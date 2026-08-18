export default function SelectCountry(props){
    const values = props.values;
    return (
        <select 
            className="form-select" aria-label="Default select example"
            value={props.value}
            onChange={props.handleChange}
            name={props.name}>

            <option value="">{props.title}</option>
            {values.map(value => {
                return <option key={value.id ?? value.name} value={value.name ?? value}>{value.name}</option>
            })}
        </select>
    )
}