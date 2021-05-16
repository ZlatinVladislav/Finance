import { Button, Grid, Icon, Item, Label, Segment } from "semantic-ui-react";
import { Link } from "react-router-dom";
import React, { SyntheticEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { TransactionType } from "../../../app/models/transactionType";

interface Props {
    transactionType: TransactionType
}

export default function TransactionTypeListItem({transactionType}: Props) {
    const {transactionTypeStore: { loading,deleteTransactionType}, userStore: {user, logout}} = useStore();
    const [target, setTarget] = useState('');
    function handleTransactionTypeDelete(event: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(event.currentTarget.name);
        deleteTransactionType(id);
    }

    return (
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item>
                        <Item.Content>
                            <Item.Header as={Link} to={`/transactionsType/${transactionType.id}`}>
                                {transactionType.transactionType}
                            </Item.Header>
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment clearing>
                <Button
                    as={Link} to={`/transactionTypes`}
                    onClick={(e)=>handleTransactionTypeDelete(e,transactionType.id)}
                    color='red'
                    name={transactionType.id}
                    loading={loading && target===transactionType.id}
                    floated='right'
                    content='Delete Type'/>
                <Button as={Link} to={`/manageTransactionType/${transactionType.id}`}
                        color='teal'
                        floated='right'
                        content='Edit'
                />
            </Segment>
        </Segment.Group>
    )
}