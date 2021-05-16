import React from 'react'
import { Button, Container, Header, Image, Item, Label, List, Menu, Segment } from 'semantic-ui-react'
import { Link, NavLink } from 'react-router-dom'
import { observer } from 'mobx-react-lite'

export default observer(function ActivityDetailedSidebar() {
    return (
        <>
            <Button as={NavLink} to='/createTransactionType'
                    name='CreateTransactionTypes'
                    content='Create Type'
                    color='green'
            />
        </>
    )
})