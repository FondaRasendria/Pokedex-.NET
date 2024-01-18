import React, { useState, useEffect } from "react";
import { useLocation, Link } from "react-router-dom";
import AppLayout from "../layouts/AppLayout";

function Region() {
    const [regions, setRegions] = useState([]);

    useEffect(() => {
        fetch("http://localhost:5152/api/region")
            .then(res => res.json())
            .then(data => setRegions(data))
            .catch(err => console.log(err))
    }, []);

    return (
        <AppLayout>
            <div id="carouselExampleDark" className="carousel carousel-dark slide">
                <div className="carousel-indicators">
                    {regions.map((item, index) => (
                        <button
                            key={index}
                            type="button"
                            data-bs-target="#carouselExampleDark"
                            data-bs-slide-to={index}
                            className={index === 0 ? "active" : ""}
                            aria-current={index === 0 ? "true" : "false"}
                            aria-label={`Slide ${index + 1}`}
                        ></button>
                    ))}
                </div>
                <div className="carousel-inner">
                    {regions.map((item, index) => (
                        <div key={index} className={`carousel-item ${index === 0 ? "active" : ""}`} data-bs-interval="10000">
                            <img src={item.image} className="d-block w-100" alt="..." />
                            <div className="carousel-caption d-none d-md-block">
                                <h5>{item.name}</h5>
                            </div>
                        </div>
                    ))}
                    
                </div>
                <button className="carousel-control-prev" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="prev">
                    <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span className="visually-hidden">Previous</span>
                </button>
                <button className="carousel-control-next" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="next">
                    <span className="carousel-control-next-icon" aria-hidden="true"></span>
                    <span className="visually-hidden">Next</span>
                </button>
            </div>
        </AppLayout>
    );
}

export default Region;