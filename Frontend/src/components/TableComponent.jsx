import React from "react";
import {
    Table, TableBody, TableCell, TableContainer,
    TableHead, TableRow, Paper, Button
} from "@mui/material";


const TableComponent = ({ columns, data, actions, datacolumns }) => (
    <TableContainer component={Paper}>
        <Table aria-label="custom table">
            <TableHead>
                <TableRow>
                    
                    {columns.map((col) => (
                        <TableCell key={col}><strong>{col}</strong></TableCell>
                    ))}
                    
                    {actions && <TableCell><strong>Actions</strong></TableCell>}
                </TableRow>
            </TableHead>
            <TableBody>
             
                {data.length > 0 ? (
                 
                    data?.map((row, index) => (
                        <TableRow key={index}>
                           
                            {datacolumns?.map((col) => (
                                <TableCell key={col}>{row[col]}</TableCell>
                            ))}
                           
                            {actions && (
                                <TableCell sx={{ display: 'flex', gap: '10px'}}>
                                    {actions.map((action, i) => (
                                        <Button
                                            key={i}
                                            variant="contained"
                                            color={action.color}
                                            onClick={() => action.handler(row)}
                                            style={{ marginLeft: i > 0 ? "0.5rem" : 0 }}
                                            startIcon={action?.icon} 
                                        >
                                            {action.label}
                                        </Button>
                                    ))}
                                </TableCell>
                            )}
                        </TableRow>
                    ))
                ) : (
                    <TableRow>
                        <TableCell colSpan={columns.length + 1} align="center">
                            No data found.
                        </TableCell>
                    </TableRow>
                )}
            </TableBody>
        </Table>
    </TableContainer>
);

export default TableComponent;