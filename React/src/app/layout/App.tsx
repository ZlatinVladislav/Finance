import React, { Fragment, useEffect } from "react";
import { Container } from "@material-ui/core";
import NavBar from "./NavBar";
import TransactionDashboard from "../../features/transactions/dashboard/TransactionDashboard";
import { observer } from "mobx-react-lite";
import HomePage from "../../features/home/HomePage";
import { Route, Switch, useLocation } from "react-router-dom";
import TransactionForm from "../../features/transactions/form/TransactionForm";
import TransactionDetails from "../../features/transactions/details/TransactionDetails";
import TestErrors from "../../features/errors/TestError";
import { ToastContainer } from "react-toastify";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import { useStore } from "../stores/store";
import LoadingComponent from "./LoadingComponent";
import ModalContainer from "../common/modals/modalContainer";
import TransactionTypeDashboard from "../../features/transactiontypes/dashboard/TransactionTypeDashboard";
import TransactionTypeForm from "../../features/transactiontypes/form/TransactionTypeForm";
import UserProfilePage from "../../features/profiles/UserProfilePage";
import PrivateRoute from "./PrivateRoute";
import "./styles.css";
import BankDashboard from "../../features/banks/dashboard/BankDashboard";
import TransactionBankForm from "../../features/transactions/form/TransactionBankForm";

function App() {
    const location = useLocation();
    const {commonStore,userStore}=useStore();

    useEffect(()=>{
        if(commonStore.token){
            userStore.getUser().finally(()=>commonStore.setAppLoaded());
        }else{
            commonStore.setAppLoaded();
        }
    },[commonStore,userStore])

    if(!commonStore.appLoaded) return <LoadingComponent content='Loading app...'/>

    return (
        <Fragment>
            <ToastContainer position='top-right' hideProgressBar/>
            <ModalContainer/>
            <Route exact path='/' component={HomePage}/>
            <Route
                path={'/(.+)'}
                render={() => (
                    <>
                        <NavBar/>
                        <Container className="default-margin">
                            <Switch>
                                <PrivateRoute exact path='/transactions' component={TransactionDashboard}/>
                                <PrivateRoute exact path='/transactionTypes' component={TransactionTypeDashboard}/>
                                <PrivateRoute exact path='/banks' component={BankDashboard}/>
                                <PrivateRoute key={location.key} path={['/manageBankTransaction/:id']}
                                              component={TransactionBankForm}/>
                                <PrivateRoute key={location.key} path={['/createTransactionType', '/manageTransactionType/:id']}
                                       component={TransactionTypeForm}/>
                                <PrivateRoute key={location.key} path={['/createTransaction', '/manageTransaction/:id']}
                                       component={TransactionForm}/>
                                <PrivateRoute path='/transactions/:id' component={TransactionDetails}/>
                                <PrivateRoute path='/userProfile/:userName' component={UserProfilePage}/>
                                <Route path='/error' component={TestErrors}/>
                                <Route path='/server-error' component={ServerError}/>
                                <Route component={NotFound}/>
                            </Switch>
                        </Container>
                    </>
                )}
            />
        </Fragment>
    );
}

export default observer(App);
