import React, { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import AppLayout from "../layouts/AppLayout";
import axios from "axios";

function AddPokemon() {
    const [id, setId] = useState(0);
    const [pokemonId, setPokemonId] = useState(0);
    const [typeId, setTypeId] = useState(0);
    const [tempType, setTempType] = useState(0);
    const [name, setName] = useState("");
    const [image, setImage] = useState("");
    const [type, setType] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

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

    useEffect(() => {
        fetchResource("http://localhost:5152/api/type", setType)
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true);
        try {
            let response = await axios.post(
                "http://localhost:5152/api/pokemon",
                {
                    id,
                    name,
                    image,
                }
            );
            try {
                let response2 = await axios.post(
                    "http://localhost:5152/api/pokemontype",
                    {
                        id,
                        pokemonId,
                        typeId,
                    }
                )
                try {
                    let response3 = await axios.post(
                        "http://localhost:5152/api/pokemontype",
                        {
                            id,
                            pokemonId,
                            typeId: tempType,
                        }
                    )
                }
                catch (error) {
                    console.error(error);
                }
                console.log(response.data);
                console.log(response2.data);
                setPokemonId(0);
                setTypeId(0);
                navigate("/dashboard");
            }
            catch (error) {
                console.error(error);
            }
            setId(0);
            setName("");
            setImage("");
        }
        catch (error) {
            console.error(error);
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
                            <label htmlFor="pokemonIndex" className="form-label">Pokemon Index</label>
                            <input type="number" className="form-control" id="pokemonIndex" value={id} onChange={(e) => { setId(e.target.valueAsNumber); setPokemonId(e.target.value); }} />
                        </div>
                        <div className="mb-3">
                            <label htmlFor="pokemonName" className="form-label">Pokemon Name</label>
                            <input type="text" className="form-control" id="pokemonName" value={name} onChange={(e) => setName(e.target.value)} />
                        </div>
                        <label htmlFor="pokemonType" className="form-label">Pokemon Type</label>
                        <select className="form-select" value={typeId} aria-label="Default select example" onChange={(e) => setTypeId(parseInt(e.target.value))}>
                            <option selected value="0">Select Pokemon Type</option>
                            {type.map((item) => (
                                <option key={item.id} value={item.id}>
                                    {item.name}
                                </option>
                            ))}
                        </select>
                        <select className="form-select" value={tempType} aria-label="Default select example" style={{ marginTop: "10px" }} onChange={(e) => setTempType(parseInt(e.target.value))}>
                            <option selected value="0">Select Pokemon Type</option>
                            {type.map((item) => (
                                <option key={item.id} value={item.id}>
                                    {item.name}
                                </option>
                            ))}
                        </select>
                        <div style={{ marginTop: "16px" }}>
                            <button type="submit" className="btn btn-primary">Submit</button>
                        </div>
                    </form>
                </div>
            </div>
        </AppLayout>
    );
}

export default AddPokemon;