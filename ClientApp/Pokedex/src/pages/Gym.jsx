import React, { useState, useEffect } from "react";
import { useLocation, Link } from "react-router-dom";
import AppLayout from "../layouts/AppLayout";

function Gym() {
    const [gyms, setGyms] = useState([]);

    useEffect(() => {
        fetch("http://localhost:5152/api/gym")
            .then(res => res.json())
            .then(data => setGyms(data))
            .catch(err => console.log(err))
    }, []);

    return (
        <AppLayout>
            {gyms.map((item, index) => (
                <h5 key={index}>{item.name}</h5>
            )) }
        </AppLayout>
    );
}

export default Gym;