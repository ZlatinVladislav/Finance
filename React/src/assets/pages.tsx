import { makeStyles } from "@material-ui/styles";

export const useStyles = makeStyles({
    alignCenter: {
        fontSize: "30px",
        marginBottom: "20px",
        textAlignLast: "center"
    },
    searchIcon: {
        alignSelf: "center !important",
        fontSize: "150px"
    },
    backgroundColorWhite: {
        backgroundColor: "white"
    },
    backgroundButtonColorTeal: {
        color: 'white',
        backgroundColor: "teal",
        "&:hover": {
            backgroundColor: "rgb(32, 167, 172)"
        }
    },
    colorRed: {
        color: "red"
    },
    colorTeal: {
        color: "teal"
    },
    homePage:{
        height: '100%',
        backgroundImage: "linear-gradient(135deg, rgb(24, 42, 115) 0%, rgb(33, 138, 174) 69%, rgb(32, 167, 172) 89%)",
        color:'white'
    },
    navBar: {
        backgroundImage: "linear-gradient(135deg, rgb(24, 42, 115) 0%, rgb(33, 138, 174) 69%, rgb(32, 167, 172) 89%)",
        display: 'flex', justifyContent: 'space-between'

    },
    root: {
        '& .super-app-theme--header': {
            backgroundColor: "red",
            '&:hover': {
                backgroundColor: "red",
            },
        },
    },
    overrides: {
        MuiToolbar: {
            regular: {
                '@media (min-width: 600px)': {
                    minHeight: "0px"
                }
            }
        },
    },
    positionRight: {

    },
    filter: {
        paddingLeft:"20px",
        marginTop: "36px"
    },
    alignLeft: {
        alignContent: 'left',
        justify: 'flex-end'
    },
    activityImageTextStyle: {
        position: 'absolute',
        bottom: '5%',
        left: '5%',
        width: 'auto',
        height: '50',
        color: 'white',
    },
    table: {
        minWidth: 650,
    }
});