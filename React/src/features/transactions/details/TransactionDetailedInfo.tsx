import { observer } from 'mobx-react-lite';
import React from 'react'
import { Grid, Icon, Segment } from 'semantic-ui-react'
import { Transaction } from "../../../app/models/transaction";
import { format } from "date-fns";

interface Props {
    transaction: Transaction
}

export default observer(function TransactionDetailedInfo({transaction}: Props) {
    return (
        <Segment.Group>
            <Segment attached='top'>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='info'/>
                    </Grid.Column>
                    <Grid.Column width={15}>
                        {transaction.transactionStatus ? (<p>Income</p>) : (<p>Outcome</p>)}
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='content' size='large' color='teal'/>
                    </Grid.Column>
                    <Grid.Column width={15}>
            <span>
                   {transaction.transactionType?.length!==0  ? (<p>{transaction.transactionType}</p>) : (
                       <p>Transaction type was not defined</p>)}
            </span>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='money' size='large' color='teal'/>
                    </Grid.Column>
                    <Grid.Column width={15}>
            <span>
                   {transaction.bankDto?.length ? (
                       <span>
                           {transaction.bankDto.map((bank) => (
                               <p key={bank.id}>  {bank.name} </p>
                           ))}
                      </span>
                   ) : (<p>Banks were not defined</p>)}
            </span>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='calendar' size='large' color='teal'/>
                    </Grid.Column>
                    <Grid.Column width={15}>
            <span>
              {format(transaction.dateTransaction!, 'dd MMM yyyy')}
            </span>
                    </Grid.Column>
                </Grid>
            </Segment>
        </Segment.Group>
    )
})