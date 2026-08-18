

export default function SelectLoanType(props){
    const values = props.values;
    return (
        <div>
            <label htmlFor="Name" className="form-label">Loan Type</label>
            <select 
                className="form-select" aria-label="Default select example"
                value={props.value}
                onChange={props.handleChange}
                name={props.name}>

                <option value="">{props.title}</option>
                {values.map(value => {
                    return <option key={value} value={value}>{value}</option>
                })}
            </select>
        </div>

    )
}