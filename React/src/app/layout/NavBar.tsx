import React from "react";
import { Container, Button, MenuItem, AppBar, Toolbar, Select, Avatar, InputLabel } from "@material-ui/core";
import { useStore } from "../stores/store";
import { Link, NavLink } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { useStyles } from "../../assets/pages";

export default observer(function NavBar() {
    const classes = useStyles();
    const {userStore:{user,logout}} = useStore();

    return (
        <AppBar position="static" className={classes.navBar}>
            <Toolbar>
                <MenuItem component={NavLink} to='/'>
                    <img src="/assets/logo.png" alt='logo' style={{height: '38px'}}/>
                    Finances
                </MenuItem>
                <MenuItem component={NavLink} to='/transactions'>Transactions</MenuItem>
                <MenuItem component={NavLink} to='/transactionTypes' >Transaction Types</MenuItem>
                <MenuItem component={NavLink} to='/error'>Errors</MenuItem>
                <MenuItem>
                    <Button component={NavLink} to='/createTransaction' style={{backgroundColor: 'rgba(76,255,0,0.61)'}}>Create Transaction</Button>
                </MenuItem>
                <MenuItem className={classes.positionRight}>
                    <Avatar src={user?.image || '/assets/user.png'} style={{float: 'inline-end',width:'35px',height:'auto'}}/>
                    <InputLabel id="demo-controlled-open-select-label">{user?.displayName}</InputLabel>
                    <Select>
                        <Container>
                            <MenuItem component={Link} to={`/userProfile/${user?.username}`}>My Profile</MenuItem>
                            <MenuItem onClick={logout}>Logout</MenuItem>
                        </Container>
                    </Select>
                </MenuItem>
            </Toolbar>
        </AppBar>
    );
})
