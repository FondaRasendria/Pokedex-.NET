import React, { useState, useEffect } from "react";
import { useLocation, Link } from "react-router-dom";
import AppLayout from "../layouts/AppLayout";

function Trainer() {
    const [trainers, setTrainers] = useState([]);
    const [region, setRegion] = useState([]);
    const [gym, setGym] = useState([]);

    useEffect(() => {
        fetch("http://localhost:5152/api/trainer")
            .then(res => res.json())
            .then(data => setTrainers(data))
            .catch(err => console.log(err));
    }, []);

    const fetchPokemonById = async (pokemonId) => {
        try {
            const response = await fetch("http://localhost:5152/api/Pokemon/" + pokemonId + "/getById");
            if (response.ok) {
                const pokemonData = await response.json();
                return pokemonData;
            }
            throw new Error('Failed to fetch Pokemon');
        }
        catch (error) {
            console.error(error);
            return [];
        }
    }
    const fetchRegionById = async (regionId) => {
        try {
            const response = await fetch("http://localhost:5152/api/Region/" + regionId + "/getById");
            if (response.ok) {
                const regionData = await response.json();
                return regionData;
                
            }
            throw new Error('Failed to fetch Region');
        }
        catch (error) {
            console.error(error);
            return [];
        }
    }
    const fetchGymById = async (gymId) => {
        try {
            const response = await fetch("http://localhost:5152/api/Gym/" + gymId + "/getById");
            if (response.ok) {
                const regionData = await response.json();
                return regionData;
            }
            throw new Error('Failed to fetch Region');
        }
        catch (error) {
            console.error(error);
            return [];
        }
    }

    useEffect(() => {
        const fetchPokemonsForTrainers = async () => {
            const updatedTrainer = await Promise.all(
                trainers.map(async (trainer) => {
                    try {
                        const response = await fetch("http://localhost:5152/api/PokemonTrainer/" + trainer.id + "/getByTrainerId");
                        if (response.ok) {
                            const pokemonTrainers = await response.json();

                            const trainersData = await Promise.all(
                                pokemonTrainers.map(async (pokeTrainer) => {
                                    const trainerData = await fetchPokemonById(pokeTrainer.pokemonId);
                                    return trainerData;
                                })
                            );

                            return { ...trainer, pokemons: trainersData };
                        }
                        else {
                            throw new Error("Failed To Fetch Pokemon Trainer");
                        }
                    }
                    catch (error) {
                        console.error(error);
                        return { ...trainer, pokemons: [] };
                    }
                })
            );
            setTrainers(updatedTrainer);
        };
        if (trainers.length > 0) {
            fetchPokemonsForTrainers();
        }
    }, [trainers]);

    useEffect(() => {
        const fetchDataForTrainers = async () => {
            const updatedTrainers = await Promise.all(
                trainers.map(async (trainer) => {
                    try {
                        const regionData = await fetchRegionById(trainer.regionId);
                        const gymData = await fetchGymById(trainer.gymId);

                        return { ...trainer, regions: regionData, gyms: gymData };
                    }
                    catch (error) {
                        console.error(error);
                        return { ...trainer, regions: [], gyms: [] };
                    }
                })
            );
            setTrainers(updatedTrainers);
        };
        fetchDataForTrainers();
    }, [trainers]);

    return (
        <AppLayout>
            <a href="/addtrainer"><h5 style={{ textAlign: "right", color: "black" }}>Add Trainer</h5></a>
            {trainers.map((item, index) => (
                <div className="card mb-3" key={index}>
                    <div className="row g-0">
                        <div className="col-md-4">
                            <img src={item.image} className="img-fluid rounded-start" style={{ margin: "16px", maxHeight: "400px" }} referrerPolicy="no-referrer" />
                        </div>
                        <div className="col-md-8">
                            <div className="card-body">
                                <h5 className="card-title">{ item.name}</h5>
                                <p className="card-text">{ item.descrption}</p>

                                {item.regions && (
                                    <h6>Region: <b>{item.regions.name}</b></h6>
                                )}
                                {item.gyms && (
                                    <h6>Gym: <b>{item.gyms.name}</b></h6>
                                )}
                                <div className="row g-0">
                                    {item.pokemons && item.pokemons.map((pokemon, index) => (
                                        <div className="card mb-3" style={{ maxWidth: "250px", margin: "8px" }} key={index}>
                                            <div className="row g-0" style={{ alignItems: "center" }}>
                                                <div className="col-md-4">
                                                    <img src={pokemon.image} className="img-fluid rounded-start" style={{ margin: "16px" }} />
                                                </div>
                                                <div className="col-md-8">
                                                    <div className="card-body">
                                                        <h5 className="card-title">{pokemon.name}</h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    ))}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            ))}
        </AppLayout>
    );
}

export default Trainer;