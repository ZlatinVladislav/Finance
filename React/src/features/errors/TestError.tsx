import React, { useState } from 'react';
// import { Button, Header, Segment } from "semantic-ui-react";
import axios from 'axios';
import ValidationError from "./ValidationError";
import { useStyles } from "../../assets/pages";
import { Button, Typography, Container, ButtonGroup } from "@material-ui/core"

export default function TestErrors() {
    const classes = useStyles();
    const baseUrl = 'https://localhost:5001/api/'
    const [errors, setErros] = useState(null);

    function handleNotFound() {
        axios.get(baseUrl + 'error/not-found').catch(err => console.log(err.response));
    }

    function handleBadRequest() {
        axios.get(baseUrl + 'error/bad-request').catch(err => console.log(err.response));
    }

    function handleServerError() {
        axios.get(baseUrl + 'error/server-error').catch(err => console.log(err.response));
    }

    function handleUnauthorised() {
        axios.get(baseUrl + 'error/unauthorised').catch(err => console.log(err.response));
    }

    function handleBadGuid() {
        axios.get(baseUrl + 'transaction/notaguid').catch(err => console.log(err.response));
    }

    function handleValidationError() {
        axios.post(baseUrl + 'transaction', {}).catch(err => setErros(err));
    }

    return (
        <>
            <Typography variant='h2' className={classes.alignCenter}>Test Error component</Typography>
            <Container className={classes.alignCenter}>
                <ButtonGroup color="primary" aria-label="outlined primary button group">
                    <Button onClick={handleNotFound}>Not Found</Button>
                    <Button onClick={handleBadRequest}>Bad Request</Button>
                    <Button onClick={handleValidationError}>Validation Error</Button>
                    <Button onClick={handleServerError}>Server Error</Button>
                    <Button onClick={handleUnauthorised}>Unauthorised</Button>
                    <Button onClick={handleBadGuid}>Bad Guid</Button>
                </ButtonGroup>
            </Container>
            {errors &&
            <ValidationError errors={errors}/>
            }
        </>
    )
}