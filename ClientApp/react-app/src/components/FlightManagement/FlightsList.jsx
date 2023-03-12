import React, { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button'
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

export default props => {
    const navigate = useNavigate();
    const [flights, setFlights] = useState([]);

    const fetchFlights = async () => {
            const response = await fetch('https://localhost:5000/api/flights');
            const json = await response.json();
            setFlights(json);
        };

    useEffect(() => {
        fetchFlights();
    }, []);


    const editFlight = (e) => {
        navigate('/flight/' + e);
    }

    const deleteFlight = (e) => {
        confirmAlert({
            title: 'Confirm delete',
            message: 'Are you sure you want to delete this flight?',
            buttons: [
                {
                    label: 'Yes',
                    onClick: () => {
                        fetch('https://localhost:5000/api/flights/' + e,
                            {
                                method: 'DELETE'
                            })
                            .then(() => fetchFlights())
                            .catch(error => alert(error));
                    }
                },
                {
                    label: 'No'
                }
            ]
        });
    }

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell align="left">Flight Number</TableCell>
                        <TableCell align="left">Departure Airport</TableCell>
                        <TableCell align="left">Destination Airport</TableCell>
                        <TableCell align="left">Departure Time</TableCell>
                        <TableCell align="left">Arrival Time</TableCell>
                        <TableCell align="left">Price</TableCell>
                        <TableCell align="left">Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {flights.map((row) => (
                        <TableRow
                            key={row.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell align="left">{row.flightNumber}</TableCell>
                            <TableCell align="left">{row.departureAirport}</TableCell>
                            <TableCell align="left">{row.destinationAirport}</TableCell>
                            <TableCell align="left">{new Date(row.departureTime).toLocaleString()}</TableCell>
                            <TableCell align="left">{new Date(row.arrivalTime).toLocaleString()}</TableCell>
                            <TableCell align="left">{row.price.toFixed(2)}</TableCell>
                            <TableCell align="left">
                                <Button onClick={() => editFlight(row.id)}>Edit</Button>
                                <Button onClick={() => deleteFlight(row.id)}>Delete</Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}