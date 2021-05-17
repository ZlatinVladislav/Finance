import React from 'react'
import { Button } from "@material-ui/core"
import { NavLink } from 'react-router-dom'
import { observer } from 'mobx-react-lite'

export default observer(function ActivityDetailedSidebar() {
    return (
        <>
            <Button variant="contained"  color='primary' component={NavLink} to='/createTransactionType'
            >Create Type</Button>
        </>
    )
})