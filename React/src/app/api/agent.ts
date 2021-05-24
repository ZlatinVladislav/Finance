import axios, { AxiosError, AxiosResponse } from "axios";
import { Transaction, TransactionFormValues } from "../models/transaction";
import { toast } from "react-toastify";
import { history } from "../../index";
import { store } from "../stores/store";
import { User, UserFormValues } from "../models/user";
import { TransactionType, TransactionTypeFormValues } from "../models/transactionType";
import { Photo, UserProfile } from "../models/profile";
import { PaginatedResult } from "../models/pagination";
import { Bank, BankFormValues } from "../models/bank";
import { UserDescription } from "../models/userDescription";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = 'https://localhost:44303/api';

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token) config.headers.authorization = `Bearer ${token}`
    return config;
})

axios.interceptors.response.use(async response => {
    await sleep(1000);
    const pagination = response.headers['pagination'];
    if (pagination) {
        response.data = new PaginatedResult(response.data, JSON.parse(pagination));
        return response as AxiosResponse<PaginatedResult<any>>
    }
    return response
}, (error: AxiosError) => {
    const {data, status, config} = error.response!;
    switch (status) {
        case 400:
            if (typeof data === 'string') {
                toast.error(data);
            }
            if (config.method === 'get' && data.errors.hasOwnProperty('id')) {
                history.push('/not-found');
            }
            if (data.errors) {
                const modalStateError = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateError.push(data.errors[key]);
                    }
                }
                throw modalStateError.flat();
            }
            break;
        case 401:
            toast.error('Unauthorized')
            break;
        case 403:
            toast.error('Forbidden')
            break;
        case 404:
            history.push('/not-found')
            break;
        case 500:
            store.commonStore.setServerError(data);
            history.push('/server-error')
            break;
    }
    return Promise.reject(error);
})

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const request = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body?: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Transacions = {
    list: (params: URLSearchParams) => axios.get<PaginatedResult<Transaction[]>>('/Transaction', {params}).then(responseBody),
    details: (id: string) => request.get<Transaction>(`/Transaction/${id}`),
    create: (transaction: TransactionFormValues) => request.post<void>('/Transaction', transaction),
    update: (transaction: TransactionFormValues) => request.put<void>(`/Transaction/${transaction.id}`, transaction),
    delete: (id: string) => request.del<void>(`/Transaction/${id}`),
    cancell: (id: string) => request.post<void>(`/Transaction/${id}/cancel`)
}

const TransactionTypes = {
    list: (params: URLSearchParams) => axios.get<PaginatedResult<TransactionType[]>>('/TransactionType', {params}).then(responseBody),
    listAll: () => axios.get<TransactionType[]>('/TransactionType/all'),
    details: (id: string) => request.get<TransactionType>(`/TransactionType/${id}`),
    create: (transactionType: TransactionTypeFormValues) => request.post<void>('/TransactionType', transactionType),
    update: (transactionType: TransactionTypeFormValues) => request.put<void>(`/TransactionType/${transactionType.id}`, transactionType),
    delete: (id: string) => request.del<void>(`/TransactionType/${id}`),
}

const Banks = {
    list: () => axios.get<Bank[]>('/Bank'),
    assignBank:(bankId:string,transactionId:string)=>request.post<Bank>(`/Bank/${bankId}/transaction/${transactionId}`),
    details: (id: string) => request.get<Bank>(`/Bank/${id}`),
    // create: (transactionType: TransactionTypeFormValues) => request.post<void>('/TransactionType', transactionType),
    // update: (transactionType: TransactionTypeFormValues) => request.put<void>(`/TransactionType/${transactionType.id}`, transactionType),
    // delete: (id: string) => request.del<void>(`/TransactionType/${id}`),
}

const Account = {
    current: () => request.get<User>('/account'),
    login: (user: UserFormValues) => request.post<User>('/account/login', user),
    register: (user: UserFormValues) => request.post<User>('/account/register', user)
}

const UserProfiles = {
    get: (userName: string) => request.get<UserProfile>(`/userProfile/${userName}`),
    post: (description: UserDescription) => request.post<void>(`/userProfile`,description),
    put: (description: UserDescription) => request.put<void>(`/userProfile`,description),
    uploadPhoto: (file: Blob) => {
        let formData = new FormData();
        formData.append('File', file);
        return axios.post<Photo>('photo', formData, {
            headers: {'Content-type': 'multipart/form-data'}
        })
    },
    setMainPhoto: (id: string) => request.post(`/photo/${id}/setMain`, {}),
    deletePhoto: (id: string) => request.del(`/photo/${id}`)
}

const agent = {
    Transacions, Account, TransactionTypes, UserProfiles, Banks
}

export default agent;