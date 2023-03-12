import { React, useState, useEffect } from 'react';
import { TextField, Button } from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom'
import './EditFlight.css'

import dayjs from 'dayjs';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DateTimeField } from '@mui/x-date-pickers/DateTimeField';


export default props => {
    const navigate = useNavigate();
    const { id } = useParams();

    const [flight, setFlight] = useState({
        flightNumber: '',
        departureAirport: '',
        destinationAirport: '',
        departureTime: dayjs(),
        arrivalTime: dayjs(),
        price: ''
    });


    const fetchFlight = async () => {
        const response = await fetch('https://localhost:5000/api/flights/' + id);
        const json = await response.json();

        if (json) {
            setFlight(json);
        }
    };

    useEffect(() => {
        if (id)
            fetchFlight();
    }, []);

    const handleSubmit = (event) => {
        event.preventDefault();
        if (id) {
            fetch('https://localhost:5000/api/flights/' + id,
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(flight)
                })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    navigate('/');
                })
                .catch(error => {
                    console.error('There was a problem with the fetch operation:', error);
                });
        }
        else {
            fetch('https://localhost:5000/api/flights/',
                {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(flight)
                })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    navigate('/');
                })
                .catch(error => {
                    console.error('There was a problem with the fetch operation:', error);
                });
        }
    };

    const handleCancel = (event) => {
        event.preventDefault();

        navigate('/');
    };

    return (
        <div>
            <h2>{id ? 'Edit' : 'Add'} Flight</h2>

            <form onSubmit={handleSubmit} className="Form">
                <TextField
                    className="FormElement"
                    label="Flight Number"
                    margin="normal"
                    variant="filled"
                    value={flight.flightNumber}
                    onChange={(e) => setFlight({ ...flight, flightNumber: e.target.value })}
                />
                <TextField
                    className="FormElement"
                    label="Departure Airport"
                    margin="normal"
                    variant="filled"
                    value={flight.departureAirport}
                    onChange={(e) => setFlight({ ...flight, departureAirport: e.target.value })}
                />
                <TextField
                    className="FormElement"
                    label="Destination Airport"
                    margin="normal"
                    variant="filled"
                    value={flight.destinationAirport}
                    onChange={(e) => setFlight({ ...flight, destinationAirport: e.target.value })}
                />
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <DateTimeField
                        label="Departure Time"
                        margin="normal"
                        value={dayjs(flight.departureTime)}
                        onChange={(newValue) => setFlight({ ...flight, departureTime: newValue })}
                        format="L HH:mm"
                    />
                    <DateTimeField
                        label="Arrival Time"
                        margin="normal"
                        value={dayjs(flight.arrivalTime)}
                        onChange={(newValue) => setFlight({ ...flight, arrivalTime: newValue })}
                        format="L HH:mm"
                    />
                </LocalizationProvider>
                <TextField
                    className="FormElement"
                    label="Price"
                    type="number"
                    margin="normal"
                    variant="filled"
                    value={flight.price}
                    onChange={(e) => setFlight({ ...flight, price: e.target.value })}
                />
                <Button
                    className="FormElement"
                    type="submit"
                    margin="normal"
                    variant="contained"
                    color="primary">
                    Save
                </Button>
                <Button
                    className="FormElement"
                    margin="normal"
                    onClick={handleCancel}
                    variant="contained"
                    color="secondary">
                    Cancel
                </Button>
            </form>
        </div>
    );
};
