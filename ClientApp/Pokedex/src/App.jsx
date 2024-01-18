import React, { useEffect } from "react";
import {
    BrowserRouter as Router,
    Routes,
    Route,
    useNavigate,
} from "react-router-dom";

import Dashboard from "./pages/Dashboard";
import AddPokemon from "./pages/AddPokemon";
import Trainer from "./pages/Trainer";
import Region from "./pages/Region";
import AddTrainer from "./pages/AddTrainer"; 
import Gym from "./pages/Gym";

function RedirectToDashboard() {
    const navigate = useNavigate();

    useEffect(() => {
        navigate("/dashboard");
    }, [navigate]);

    return null;
}

function App() {

    return (
        <Router>
            <div className="App">
                <Routes>
                    <Route path="/dashboard" element={<Dashboard />} />
                    <Route path="/addpokemon" element={<AddPokemon />} />
                    <Route path="/trainer" element={<Trainer />} />
                    <Route path="/region" element={<Region />} />
                    <Route path="/addtrainer" element={<AddTrainer />} />
                    <Route path="/gym" element={<Gym />} />
                    <Route path="*" element={<RedirectToDashboard />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
