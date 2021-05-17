import React from "react";
import { useStore } from "../../app/stores/store";
import { Typography, Container } from "@material-ui/core"
import { observer } from "mobx-react-lite";
import { useStyles } from "../../assets/pages";

export default observer(function ServerError() {
    const {commonStore} = useStore();
    const classes = useStyles();

    return (
        <Container>
            <Typography variant='h2'>Server Error</Typography>
            <Typography variant='h5' className={classes.colorRed}>{commonStore.error?.message}</Typography>
            {commonStore.error?.details &&
            <Container>
                <Typography variant='h4' className={classes.colorTeal}>Stack trace</Typography>
                <code style={{marginTop: '10px'}}>{commonStore.error.details}</code>
            </Container>
            }
        </Container>
    )
})