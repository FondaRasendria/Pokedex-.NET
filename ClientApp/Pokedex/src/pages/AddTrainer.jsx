import React, { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import AppLayout from "../layouts/AppLayout";
import axios from "axios";

function AddTrainer() {
    const [id, setId] = useState(0);
    const [name, setName] = useState("");
    const [regionId, setRegionId] = useState(0);
    const [gymId, setGymId] = useState(0);
    const [image, setImage] = useState("");
    const [descrption, setDescrption] = useState("");
    const [isLoading, setIsLoading] = useState(false);

    const [pokemons, setPokemons] = useState([]);
    const [regions, setRegions] = useState([]);
    const [gyms, setGyms] = useState([]);

    const [selectedPokemons, setSelectedPokemons] = useState([]);
    const [hoveredCard, setHoveredCard] = useState(null);
    const [hoveredPokemon, setHoveredPokemon] = useState(null);

    const navigate = useNavigate();

    const fetchResource = async (url, setter) => {
        try {
            const response = await axios.get(url);
            setter(response.data);
        }
        catch (error) {
            console.error("Error Fetching Data: " + error);
        }
    }

    const handleMouseOverCard = (index) => {
        setHoveredCard(index);
    }
    const handleMouseOutCard = () => {
        setHoveredCard(null);
    }

    const handleMouseOverPokemon = (index) => {
        setHoveredPokemon(index);
    }
    const handleMouseOutPokemon = () => {
        setHoveredPokemon(null);
    }

    const handleSaveChanges = (item) => {
        if (selectedPokemons.length < 6) {
            setSelectedPokemons([...selectedPokemons, item]);
        }
        else {
            alert("Jumlah Pokemon melebihi batas");
        }
    };
    const handleDeletePokemon = (index) => {
        const updatedPokemons = [...selectedPokemons];
        updatedPokemons.splice(index, 1);
        setSelectedPokemons(updatedPokemons);
        setHoveredPokemon(null);
    };

    useEffect(() => {
        fetchResource("http://localhost:5152/api/pokemon", setPokemons);
        fetchResource("http://localhost:5152/api/region", setRegions);
        fetchResource("http://localhost:5152/api/gym", setGyms);
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true);
        try {
            let response = await axios.post(
                "http://localhost:5152/api/trainer",
                {
                    id,
                    name,
                    regionId,
                    gymId,
                    image,
                    descrption,
                }
            );
            var data = response.data;
            try {
                for (var i = 0; i < selectedPokemons.length; i++) {
                    let response2 = await axios.post(
                        "http://localhost:5152/api/pokemontrainer",
                        {
                            id,
                            pokemonId: selectedPokemons[i].id,
                            trainerId: data.id,
                        }
                    )
                }
                
                console.log(response.data);
                navigate("/trainer");
            }
            catch (error) {
                console.error(error);
            }
            setId(0);
            setName("");
            setRegionId(0);
            setGymId(0);
            setImage("");
            setDescrption("");
        }
        catch (error) {
            console.error(error.response.data);
        }
        finally {
            setIsLoading(false);
        }
    }

    return (
        <AppLayout>
            <div className="card">
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label htmlFor="trainerName" className="form-label">Name</label>
                            <input type="text" className="form-control" id="trainerName" value={name} onChange={(e) => setName(e.target.value)} />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Region</label>
                            <select className="form-select" value={regionId} aria-label="Default select example" style={{ marginTop: "10px" }} onChange={(e) => setRegionId(parseInt(e.target.value))}>
                                <option value="0">Select Region</option>
                                {regions.map((item) => (
                                    <option key={item.id} value={item.id}> 
                                        {item.name}
                                    </option>
                                ))}
                            </select>
                        </div>
                        {regionId == 0 && (
                            <div className="alert alert-danger" role="alert">
                                Region must be selected
                            </div>
                        )}
                        <div className="mb-3">
                            <label className="form-label">Gym</label>
                            <select className="form-select" value={gymId} aria-label="Default select example" style={{ marginTop: "10px" }} onChange={(e) => setGymId(parseInt(e.target.value))}>
                                {gyms.map((item) => (
                                    <option key={item.id} value={item.id}>
                                        {item.name}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Game Series</label>
                            <select className="form-select" value={image} aria-label="Default select example" style={{ marginTop: "10px" }} onChange={(e) => setImage(e.target.value)}>
                                <option value="HeartGold_SoulSilver_">Heart Gold & Soul Silver</option>
                                <option value="Black_White_">Black & White</option>
                                <option value="Black_2_White_2_">Black 2 & White 2</option>
                                <option value="XY_">X & Y</option>
                                <option value="Omega_Ruby_Alpha_Sapphire_">Omega Ruby & Alpha Sapphire</option>
                                <option value="Sun_Moon_">Sun & Moon</option>
                                <option value="Lets_Go_Pikachu_Eevee_">Lets Go Pikachu & Lets Go Eevee</option>
                                <option value="Sword_Shield_">Sword & Shield</option>
                                <option value="Brilliant_Diamond_Shining_Pearl_">Brilliant Diamond & Shining Pearl</option>
                                <option value="Legends_Arceus_">Legends Arceus</option>
                                <option value="Scarlet_Violet_">Scarlet & Violet</option>
                            </select>
                        </div>
                        <div className="mb-3">
                            <label htmlFor="trainerDesc" className="form-label">Description</label>
                            <textarea className="form-control" rows="3" id="trainerDesc" value={descrption} onChange={(e) => setDescrption(e.target.value)} />
                        </div>

                        <div className="modal fade" id="exampleModal" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div className="modal-dialog modal-lg">
                                <div className="modal-content">
                                    <div className="modal-header">
                                        <h1 className="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
                                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div className="modal-body">
                                        <div className="row g-0">
                                            {pokemons.map((item, index) => (
                                                <div className="card mb-3" style={{ width: "250px", margin: "8px", border: hoveredCard === index ? "1px solid #007bff": "none" }} key={index} data-bs-dismiss="modal" onClick={() => handleSaveChanges(item)} onMouseOver={() => handleMouseOverCard(index)} onMouseOut={handleMouseOutCard}>
                                                    <div className="row g-0">
                                                        <div className="col-md-4">
                                                            <img src={ item.image} className="img-fluid rounded-start" style={{ margin: "16px" }} />
                                                        </div>
                                                        <div className="col-md-8">
                                                            <div className="card-body">
                                                                <h5 className="card-title">{ item.name}</h5>
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

                        <div className="card mb-3" style={{ margin: "8px" }}>
                            <h6 style={{ marginRight: "8px", textAlign: "right", marginTop: "8px" }}>{selectedPokemons.length} /6</h6>
                            <div className="card-body">
                                <div className="row g-0">
                                    {selectedPokemons.map((item, index) => (
                                        <div className="card mb-3" style={{ width: "250px", margin: "8px", border: hoveredPokemon === index ? "1px solid #dc3545" : "" }} key={index} onMouseOver={() => handleMouseOverPokemon(index)} onMouseOut={handleMouseOutPokemon} onClick={() => handleDeletePokemon(index)}>
                                            <div className="row g-0">
                                                <div className="col-md-4">
                                                    <img src={item.image} className="img-fluid rounded-start" style={{ margin: "16px" }} />
                                                </div>
                                                <div className="col-md-8">
                                                    <div className="card-body">
                                                        <h5 className="card-title">{item.name}</h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    ))}
                                    {selectedPokemons.length < 6 && (
                                        <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                            Add Pokemon
                                        </button>
                                    )}
                                </div>
                            </div>
                        </div>

                        <div style={{ marginTop: "16px" }}>
                            <button type="submit" className="btn btn-primary disable" disabled={regionId == 0}>Submit</button>
                        </div>
                    </form>
                    {isLoading == true && (
                        <h5>Loading...</h5>
                    )}
                </div>
            </div>
        </AppLayout>
    );
}

export default AddTrainer;