import React from "react";
import { useField } from "formik";
import { Form } from "semantic-ui-react";
import { Alert } from '@material-ui/lab';

interface Props {
    placeholder: string;
    name: string;
    type?: string
    label?: string;
}

export default function MyTextInput(props: Props) {
    const [field, meta] = useField(props.name);
    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <label>{props.label}</label>
            <input {...field}{...props}/>
            {meta.touched && meta.error ? (
                <Alert severity="error">{meta.error}</Alert>
            ) : null}
        </Form.Field>
    )
}