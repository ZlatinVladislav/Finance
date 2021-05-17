import React from "react";
import { Button, Typography, Container } from "@material-ui/core"
import { Link } from "react-router-dom";
import { useStyles } from "../../assets/pages";
import SearchIcon from '@material-ui/icons/Search';


export default function NotFound() {
    const classes = useStyles();

    return (
        <Container className={classes.alignCenter}>
            <SearchIcon className={classes.searchIcon} />
            <Typography variant="h6"
                        color="inherit"
                        className={classes.alignCenter}>
                Not found recheck please and try again
            </Typography>
            <Container>
                <Button component={Link} to='/transactions' color="primary" variant="contained">
                    Return to transactions page
                </Button>
            </Container>
        </Container>
    )
}

