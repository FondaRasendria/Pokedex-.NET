import React, { useState, useEffect } from "react";
import { useLocation, Link } from "react-router-dom";
import AppLayout from "../layouts/AppLayout";
import Abilities from "../layouts/Abilities";

function Dashboard() {
    const [pokemons, setPokemons] = useState([]);

    useEffect(() => {
        fetch("http://localhost:5152/api/pokemon")
            .then(res => res.json())
            .then(data => setPokemons(data))
            .catch(err => console.log(err))
    }, []);

    const fetchTypeById = async (typeId) => {
        try {
            const response = await fetch("http://localhost:5152/api/Type/" + typeId + "/getById");
            if (response.ok) {
                const typeData = await response.json();
                return typeData;
            }
            throw new Error('Failed to fetch Type');
        }
        catch (error) {
            console.error(error);
            return [];
        }
    }

    useEffect(() => {
        const fetchTypesForPokemons = async () => {
            const updatedPokemon = await Promise.all(
                pokemons.map(async (pokemon) => {
                    try {
                        const response = await fetch("http://localhost:5152/api/PokemonType/" + pokemon.id +"/getByPokemonId");
                        if (response.ok) {
                            const pokemonTypes = await response.json();

                            const typesData = await Promise.all(
                                pokemonTypes.map(async (pokeType) => {
                                    const typeData = await fetchTypeById(pokeType.typeId);
                                    return typeData;
                                })
                            );

                            return { ...pokemon, types: typesData };
                        }
                        else {
                            throw new Error("Failed To Fetch Pokemon Types");
                        }
                    }
                    catch (error) {
                        console.error(error);
                        return { ...pokemon, types: [] };
                    }
                })
            );
            setPokemons(updatedPokemon);
        };
        if (pokemons.length > 0) {
            fetchTypesForPokemons();
        }
    }, [pokemons]);

    const padWithZeroes = (num) => {
        const padded = String(num).padStart(4, '0');
        return padded;
    };

    return (
        <AppLayout>
            <ul style={{ float: "left", marginRight: "-100%", width: "85.49%", marginLeft: "7.2525%" }}>
                {pokemons.map((list, index) => (
                    <li style={{ transform: "matrix(1, 0, 0, 1, 0, 0)", width: "23.4375%", display: "block", float: "left", margin: "0.78125% 50px" }} key={index} >
                        <div className="card">
                            <a href="" style={{ background: "#DDDDDD" }}>
                                <img src={ list.image} className="card-img-top" alt="..."></img>
                            </a>
                            <div className="card-body">
                                <div style={{ paddingLeft: "7.2525%" }}>
                                    <p style={{ fontFamily: "sans-serif", color: "#919191", fontSize: "80%", paddingTop: "2px", fontWeight: "bold" }}>
                                        <span>#</span>
                                        {padWithZeroes(list.id)}
                                    </p>
                                    <h5>{list.name}</h5>
                                    <div className="row">
                                        {list.types && list.types.map((type, index) => (
                                            <Abilities key={index}>{type.name}</Abilities>
                                        ))}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                ))}
            </ul>
        </AppLayout>
    );
}

export default Dashboard;