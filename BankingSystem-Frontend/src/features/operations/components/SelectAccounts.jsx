
export default function SelectAccounts(props){
    const values = props.values;
    return (
        <select 
            className="form-select" aria-label="Default select example"
            value={props.value}
            onChange={props.handleChange}
            name={props.name}>
            <option value="">{props.title}</option>
            {values.map(value => {
                return <option key={value.id} value={value.id ?? "-1"}>{value.accountNumber ?? "Unknown"}</option>
            })}
        </select>
    )
}