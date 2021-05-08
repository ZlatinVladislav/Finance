import { observer } from 'mobx-react-lite';
import React, { SyntheticEvent, useState } from 'react'
import { Button, Header, Image, Item, Label, Segment } from 'semantic-ui-react'
import { Transaction } from "../../../app/models/transaction";
import { Link } from "react-router-dom";
import { format } from "date-fns";
import { useStore } from "../../../app/stores/store";

const activityImageStyle = {
    filter: 'brightness(30%)'
};

const activityImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    left: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
};

interface Props {
    transaction: Transaction
}

export default observer(function ActivityDetailedHeader({transaction}: Props) {
    const {transactionStore: {cancelTransaction, loading,deleteTransaction}, userStore: {user, logout}} = useStore();
    const [target, setTarget] = useState('');
    function handleTransactionDelete(event: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(event.currentTarget.name);
        deleteTransaction(id);
    }
    return (
        <Segment.Group>
            {transaction.isCanceled &&
            <Label style={{position: 'absolute', zIndex: '1000', left: -14, top: 20}} ribbon color='red'
                   content='Cancelled'/>
            }
            <Segment basic attached='top' style={{padding: '0'}}>
                <Image src={`/assets/categoryImages/film.jpg`} fluid style={activityImageStyle}/>
                <Segment style={activityImageTextStyle} basic>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Header
                                    size='huge'
                                    content={transaction.money}
                                    style={{color: 'white'}}
                                />
                                <p>{format(transaction.dateTransaction!, 'dd MMM yyyy')}</p>
                                <p>
                                    Created by <strong><Link
                                    to={`/profiles/${user?.displayName}`}>{user?.displayName}</Link></strong>
                                </p>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Segment clearing attached='bottom'>
                    <Item.Content>
                        <Button disabled={transaction.isCanceled} as={Link} to={`/manageTransaction/${transaction.id}`} color='orange' floated='left'>
                            Edit Transaction
                        </Button>
                        <Button
                            color={transaction.isCanceled ? 'green' : 'red'}
                            floated='left'
                            basic
                            content={transaction.isCanceled ? 'Re-activate transaction' : 'Cancel Transaction'}
                            onClick={cancelTransaction}
                            loading={loading}
                            name={transaction.id}
                           />
                        <Button disabled={transaction.isCanceled}
                                as={Link} to={`/transactions`}
                                onClick={(e)=>handleTransactionDelete(e,transaction.id)}
                                color='red'
                                name={transaction.id}
                                loading={loading && target===transaction.id}
                                floated='right'>Delete Transaction</Button>
                    </Item.Content>

            </Segment>
        </Segment.Group>
    )
})