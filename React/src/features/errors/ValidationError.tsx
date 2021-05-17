import React from "react";
import { Message } from "semantic-ui-react";
import { Alert, AlertTitle } from "@material-ui/lab"

interface Props {
    errors: any;
}

export default function ValidationError({errors}: Props) {
    return (
        <Alert severity="error">
            {errors && (
                <AlertTitle>
                    {errors.map((err: any, i:any) => (
                        <Message.Item key={i}>{err}</Message.Item>
                    ))}
                </AlertTitle>
            )}
        </Alert>
    )
}