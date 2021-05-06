import { Button, Grid, Icon, Item, Label, Segment } from "semantic-ui-react";
import { Link } from "react-router-dom";
import React, { SyntheticEvent, useState } from "react";
import { Transaction } from "../../../app/models/transaction";
import { useStore } from "../../../app/stores/store";
import { format } from 'date-fns';

interface Props {
    transaction: Transaction
}

export default function TransactionListItem({transaction}: Props) {
    const {transactionStore, userStore: {user, logout}} = useStore();
    const {deleteTransaction, loading} = transactionStore;

    const [target, setTarget] = useState('');

    function handleTransactionDelete(event: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(event.currentTarget.name);
        deleteTransaction(id);
    }

    return (
        <Segment.Group>
            <Segment>
                {transaction.isCanceled &&
                <Label attached='top' color='red' content='Cancelled' style={{textAlign:'center'}}/>
                }
                <Item.Group>
                    <Item>
                        <Item.Image size='tiny' circular src='assets/user.png'/>
                        <Item.Content>
                            <Item.Header as={Link} to={`/transactions/${transaction.id}`}>
                                {transaction.money}
                            </Item.Header>
                            <Item.Description>Created by {user?.displayName}</Item.Description>
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
                <Button as={Link} to={`/transactions/${transaction.id}`}
                        color='teal'
                        floated='right'
                        content='View'
                />
            </Segment>
        </Segment.Group>
    )
}