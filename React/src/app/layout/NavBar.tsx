import React from "react";
import {  Button, MenuItem, AppBar, Toolbar, Avatar } from "@material-ui/core";
import { useStore } from "../stores/store";
import { Link, NavLink } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { useStyles } from "../../assets/pages";
import { IconButton } from "@material-ui/core";
import { Menu } from "@material-ui/core";
import MoreVertIcon from '@material-ui/icons/MoreVert';

export default observer(function NavBar() {
    const classes = useStyles();
    const {userStore: {user, logout}} = useStore();
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <AppBar position="static" className={classes.navBar}>
            <Toolbar>
                <MenuItem component={NavLink} to='/'>
                    <img src="/assets/logo.png" alt='logo' style={{height: '38px'}}/>
                    Finance
                </MenuItem>
                <MenuItem component={NavLink} to='/transactions'>Transactions</MenuItem>
                <MenuItem component={NavLink} to='/transactionTypes'>Transaction Types</MenuItem>
                <MenuItem component={NavLink} to='/banks'>Banks</MenuItem>
                <MenuItem component={NavLink} to='/error'>Errors</MenuItem>
                <MenuItem>
                    <Button component={NavLink} to='/createTransaction'
                            style={{backgroundColor: 'rgba(76,255,0,0.61)'}}>Create Transaction</Button>
                </MenuItem>
                <MenuItem style={{alignItems:'right'}}>
                    <Avatar src={user?.image || '/assets/user.png'}
                            style={{float: 'inline-end', width: '35px', height: 'auto'}}/>
                    <IconButton
                        aria-label="more"
                        aria-controls="long-menu"
                        aria-haspopup="true"
                        onClick={handleClick}
                    >
                        <MoreVertIcon/>
                    </IconButton>
                    <Menu
                        id="long-menu"
                        anchorEl={anchorEl}
                        keepMounted
                        open={open}
                        onClick={handleClose}
                        onClose={handleClose}
                        PaperProps={{
                            style: {
                                width: '20ch',
                            },
                        }}
                    >
                        <MenuItem component={Link} to={`/userProfile/${user?.username}`}>My Profile</MenuItem>
                        <MenuItem onClick={logout}>Logout</MenuItem>
                    </Menu>
                </MenuItem>
            </Toolbar>
        </AppBar>
    );
})
