import React from 'react'
import FlightsList from '../../components/FlightManagement/FlightsList'
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';

export default props => {
    const navigate = useNavigate();

    const addFlight = (e) => {
        navigate('/add');
    }

    return (
        <div>
            <h3>Flight List</h3>
            <Button onClick={addFlight}>Add Flight</Button>

            <FlightsList />
        </div>
    );
}
