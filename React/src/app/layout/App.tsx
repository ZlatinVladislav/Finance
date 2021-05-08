import React, { Fragment, useEffect } from "react";
import { Container } from "semantic-ui-react";
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
import LogInForm from "../../features/users/LogInForm";
import { useStore } from "../stores/store";
import LoadingComponent from "./LoadingComponent";
import ModalContainer from "../common/modals/modalContainer";
import TransactionTypeDashboard from "../../features/transactiontypes/dashboard/TransactionTypeDashboard";
import TransactionTypeForm from "../../features/transactiontypes/form/TransactionTypeForm";

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
                        <Container style={{marginTop: "67px"}}>
                            <Switch>
                                <Route exact path='/transactions' component={TransactionDashboard}/>
                                <Route exact path='/transactionTypes' component={TransactionTypeDashboard}/>
                                <Route key={location.key} path={['/createTransactionType', '/manageTransactionType/:id']}
                                       component={TransactionTypeForm}/>
                                <Route key={location.key} path={['/createTransaction', '/manageTransaction/:id']}
                                       component={TransactionForm}/>

                                <Route path='/transactions/:id' component={TransactionDetails}/>
                                <Route path='/error' component={TestErrors}/>
                                <Route path='/server-error' component={ServerError}/>
                                <Route path='/login' component={LogInForm}/>
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
