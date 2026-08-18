export default function NativeSwitch(props){
    return(
        <div className="form-check form-switch">
            <input 
            className="form-check-input" 
            type="checkbox" 
            checked={props.checked} 
            onChange={props.handleChange} 
            name={props.name} 
            id={props.id ?? props.name} />

            <label className="form-check-label" htmlFor="checkNativeSwitch">
                {props.label}
            </label>
        </div>
    )
}