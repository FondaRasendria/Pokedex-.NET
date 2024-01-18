import { useLocation, useNavigate, Link } from "react-router-dom";

function AppLayout({ children }) {
    const location = useLocation();
    const navigate = useNavigate();

    return (
        <div>
            <nav className="navbar navbar-expand-sm bg-body-tertiary">
                <div className="container-fluid">
                    <a className="navbar-brand" href="/dashboard">
                        <img src="/pokeball.svg" alt="Logo" width="30" height="24" className="d-inline-block align-text-top"></img>
                        Pokedex
                    </a>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav">
                            <li className="nav-item">
                                <a className="nav-link" aria-current="page" href="/dashboard">Home</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="/trainer">Trainer</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="/gym">Gym</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="/region">Region</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="/addpokemon">Add Pokemon</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <div className="wrapper">
                <div className="col mt-3">
                    <div className="page-header d-print-none">
                        <div className="container-xl">
                            <div className="row g-2 align-items-center">
                                <div className="col">{children}</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default AppLayout;