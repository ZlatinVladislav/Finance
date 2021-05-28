import React from "react";
import { Link } from "react-router-dom";
import { Button, Container, Header, Image, Segment } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observable } from "mobx";
import { observer } from "mobx-react-lite";
import LogInForm from "../users/LogInForm";
import RegisterForm from "../users/RegisterForm";


export default observer(function HomePage() {
    const {userStore, modalStore} = useStore();
    return (
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header as='h1' inverted>
                    <Image size='massive' src='/assets/logo.png' alt='logo' style={{marginBottom: 12}}/>
                    Finance
                </Header>
                {userStore.isLoggedin ? (
                    <>
                        <Header as='h2' inverted content='Welcome to Finance'/>
                        <Button as={Link} to='/transactions' size='huge' inverted>
                            Open Transactions
                        </Button>
                    </>
                ) : (
                    <>
                        <Button onClick={() => modalStore.openModal(<LogInForm/>)} size='huge' inverted>
                            Log In
                        </Button>
                        <Button onClick={() => modalStore.openModal(<RegisterForm/>)} size='huge' inverted>
                            Register
                        </Button>
                    </>
                )}
            </Container>
        </Segment>
    )
})