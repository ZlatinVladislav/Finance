import React from "react";
import { useField } from "formik";
import { Form, Radio } from "semantic-ui-react";
import { Alert } from '@material-ui/lab';

interface Props {
    placeholder: string;
    name: string;
    type?: string;
    label?: string;
}

export default function MyRadioInput(props: Props) {
    const [field, meta, helpers] = useField(props.name);
    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <label>{props.label}</label>
            <Radio checked={field.value} name={props.name} type={'checkbox'} toggle
                   onChange={(event, data) => helpers.setValue(data.checked)}/>
            {meta.touched && meta.error ? (
                <Alert severity="error">{meta.error}</Alert>
            ) : null}
        </Form.Field>
    )
}