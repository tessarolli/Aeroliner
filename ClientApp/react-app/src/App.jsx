import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Home from './routes/FlightManagement/Home'
import Edit from './routes/FlightManagement/EditFlight'

function App() {
    return (
        <div className="App">

            <h2>Flight Management</h2>

            <BrowserRouter>
                <Routes>
                    <Route
                        path="/"
                        element={<Home />} />
                    <Route
                        path="/add"
                        element={<Edit />} />
                    <Route
                        path="/flight/:id"
                        element={<Edit />} /> </Routes>
            </BrowserRouter>
        </div>
    );
}

export default App;
