
export default function Input(props){
    return(
        <div className="col-12">
            <label htmlFor={props.htmlFor ?? "text"} className="form-label">{props.label} <span className="text-body-secondary"></span></label>
            <input 
                name={props.name ?? "Unkown"}
                onChange={props.handleChange} 
                type={props.type ?? "text"} 
                className={"form-control " + props.class}
                id={props.id ?? ""} 
                placeholder={props.placeholder ?? ""} 
                value={props.value}/>
            <div className="invalid-feedback">{props.feedback ?? "Something is wrong"}</div>
        </div>
    );
}

export function Input_4col(props){
    return (
        <div className="col-sm-6 col-4 ">
            <label htmlFor={props.htmlFor ?? ""} className="form-label">{props.label}<span className="text-body-secondary"></span></label>
            <input 
            type={props.type ?? "text"} 
            className={"form-control " + props.class}
            id={props.id ?? ""} 
            placeholder={props.placeholder ?? ""} 
            value={props.value}/>
        </div>
    );
}

export function Input_col_sm_6(props){
    return (
        <div className="col-sm-6">
            <label htmlFor="Name" className="form-label">{props.label}</label>
            <input 
                name={props.name ?? "Unkown"} 
                type={props.type ?? "text"} 
                className={"form-control " + props.class} 
                id={props.id ?? ""} 
                placeholder={props.placeholder ?? ""} 
                value={props.value} 
                onChange={props.handleChange} />
            <div className="invalid-feedback">{props.feedback ?? "Something is wrong"}</div>
        </div>
    );
}

export function Input_textArea(props){
    return (
        <div className={"input-group " + props.class}>
            <span className="input-group-text">{props.title}</span>
            <textarea
                className="form-control"
                aria-label="With textarea"
                onChange={props.handleChange}
                name={props.name ?? "Unknown"}
                value={props.value}
                id={props.id ?? ""}
            />
        </div>
    )
}