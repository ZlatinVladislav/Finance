import { Button, Item, Segment } from "semantic-ui-react";
import { Link } from "react-router-dom";
import React, { SyntheticEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { TransactionType } from "../../../app/models/transactionType";
import { Bank } from "../../../app/models/bank";
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@material-ui/core";
import { DataGrid, GridColDef, GridValueGetterParams } from '@material-ui/data-grid';
import { useStyles } from "../../../assets/pages";

interface Props {
    bankOption:Map<string,string>;
}

const columns: GridColDef[] = [
    { field: 'text', headerName: 'Bank name', width: 230, headerClassName: 'super-app-theme--header'},
];

export default function BankListItem({bankOption}: Props) {
    const classes = useStyles();

    return (
        <div style={{ height: 320, width: '100%' }} className={classes.backgroundColorWhite}>
            <DataGrid rows={Array.from(bankOption, ([text, value]) => ({text, value}))} getRowId={(banksOptionsArray) => banksOptionsArray.value}
                      columns={columns} pageSize={4}/>
        </div>
    )
}





















// <Segment.Group>
//     <Segment>
//         <Item.Group>
//             <Item>
//                 <Item.Content>
//                     <Item.Header as={Link} to={`/transactionsType/${bank.id}`}>
//                         {bank.name}
//                     </Item.Header>
//                 </Item.Content>
//             </Item>
//         </Item.Group>
//     </Segment>
//     <Segment clearing>
//         <Button
//             as={Link} to={`/transactionTypes`}
//             onClick={(e)=>handleTransactionTypeDelete(e,bank.id)}
//             color='red'
//             name={bank.id}
//             loading={loading && target===bank.id}
//             floated='right'
//             content='Delete Type'/>
//         <Button as={Link} to={`/manageTransactionType/${bank.id}`}
//                 color='teal'
//                 floated='right'
//                 content='Edit'
//         />
//     </Segment>
// </Segment.Group>