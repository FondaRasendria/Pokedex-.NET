function Abilities({ children }) {
    let backgroundColor = '';

    switch (children) {
        case 'Normal':
            backgroundColor = '#A8A77A';
            break;
        case 'Fire':
            backgroundColor = '#EE8130';
            break;
        case 'Water':
            backgroundColor = '#6390F0';
            break;
        case 'Electric':
            backgroundColor = '#F7D02C';
            break;
        case 'Grass':
            backgroundColor = '#7AC74C';
            break;
        case 'Ice':
            backgroundColor = '#96D9D6';
            break;
        case 'Fighting':
            backgroundColor = '#C22E28';
            break;
        case 'Poison':
            backgroundColor = '#A33EA1';
            break;
        case 'Ground':
            backgroundColor = '#E2BF65';
            break;
        case 'Flying':
            backgroundColor = '#A98FF3';
            break;
        case 'Psychic':
            backgroundColor = '#F95587';
            break;
        case 'Bug':
            backgroundColor = '#A6B91A';
            break;
        case 'Rock':
            backgroundColor = '#B6A136';
            break;
        case 'Ghost':
            backgroundColor = '#735797';
            break;
        case 'Dragon':
            backgroundColor = '#6F35FC';
            break;
        case 'Dark':
            backgroundColor = '#705746';
            break;
        case 'Steel':
            backgroundColor = '#B7B7CE';
            break;
        case 'Fairy':
            backgroundColor = '#D685AD';
            break;
    }

    return (
        <div style={{ backgroundColor, maxWidth: "90px", marginRight: "8px", borderRadius: "8px" }}>
            <span style={{ color: "white" }}>{children}</span>
        </div>
    );
}

export default Abilities;