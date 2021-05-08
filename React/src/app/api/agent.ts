import axios, { AxiosError, AxiosResponse } from "axios";
import { Transaction, TransactionList } from "../models/transaction";
import { toast } from "react-toastify";
import { history } from "../../index";
import { store } from "../stores/store";
import { User, UserFormValues } from "../models/user";
import { TransactionType, TransactionTypeList } from "../models/transactionType";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = 'https://localhost:44303/api';

axios.interceptors.request.use(config=>{
    const token=store.commonStore.token;
    if(token) config.headers.authorization=`Bearer ${token}`
    return config;
})

axios.interceptors.response.use(async response => {
    await sleep(1000);
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
            toast.error('unauthorized')
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
    list: () => request.get<TransactionList>('/Transaction'),
    details: (id: string) => request.get<Transaction>(`/Transaction/${id}`),
    create: (transaction: Transaction) => request.post<void>('/Transaction', transaction),
    update: (transaction: Transaction) => request.put<void>(`/Transaction/${transaction.id}`, transaction),
    delete: (id: string) => request.del<void>(`/Transaction/${id}`),
    cancell:(id:string)=>request.post<void>(`/Transaction/${id}/cancel`)
}

const TransactionTypes = {
    list: () => request.get<TransactionTypeList>('/TransactionType'),
    details: (id: string) => request.get<TransactionType>(`/TransactionType/${id}`),
    create: (transactionType: TransactionType) => request.post<void>('/TransactionType', transactionType),
    update: (transactionType: TransactionType) => request.put<void>(`/TransactionType/${transactionType.id}`, transactionType),
    delete: (id: string) => request.del<void>(`/TransactionType/${id}`),
}

const Account={
    current: () => request.get<User>('/account'),
    login:(user:UserFormValues)=>request.post<User>('/account/login',user),
    register:(user:UserFormValues)=>request.post<User>('/account/register',user)
}

const agent = {
    Transacions,Account,TransactionTypes
}

export default agent;