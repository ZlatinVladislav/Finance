import { observer } from 'mobx-react-lite';
import React from 'react'
import { Grid, Icon, Segment } from 'semantic-ui-react'
import { Transaction } from "../../../app/models/transaction";
import { format } from "date-fns";

interface Props {
    transaction: Transaction
}

export default observer(function ActivityDetailedInfo({transaction}: Props) {
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