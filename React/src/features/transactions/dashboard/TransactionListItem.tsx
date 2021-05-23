import { Icon, Item, Label, Segment } from "semantic-ui-react";
import { Button } from "@material-ui/core";
import { Link } from "react-router-dom";
import React from "react";
import { Transaction } from "../../../app/models/transaction";
import { useStore } from "../../../app/stores/store";
import { format } from 'date-fns';
import { useStyles } from "../../../assets/pages";

interface Props {
    transaction: Transaction
}

export default function TransactionListItem({transaction}: Props) {
    const classes = useStyles();
    const {userStore: {user}} = useStore();

    return (
        <Segment.Group>
            <Segment>
                {transaction.isCanceled &&
                <Label attached='top' color='red' content='Cancelled' style={{textAlign: 'center'}}/>
                }
                <Item.Group>
                    <Item>
                        <Item.Image size='tiny' circular src={user?.image || 'assets/user.png'}/>
                        <Item.Content>
                            <Item.Header as={Link} to={`/transactions/${transaction.id}`}>
                                {transaction.money}
                            </Item.Header>
                            <Item.Description>Created by <Link
                                to={`/userProfile/${user?.username}`}>{user?.displayName}</Link></Item.Description>
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name='calendar'/>{format(transaction.dateTransaction!, 'dd MMM yyyy')}
                </span>
            </Segment>
            <Segment clearing>
                <span>
                    <Icon name='calculator'/>{transaction.transactionStatus ? (<span>Income</span>) : (
                    <span>Outcome</span>)}
                </span>
                <Button component={Link} to={`/transactions/${transaction.id}`}
                      className={classes.backgroundButtonColorTeal}
                        variant="contained"
                        style={{
                             marginTop:'10px',
                             display: "flex"
                        }}
                >View</Button>
            </Segment>
        </Segment.Group>
    )
}