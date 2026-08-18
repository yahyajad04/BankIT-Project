
export default function DisabledInput(props){
    return(
        <div className="col-12">
            <label htmlFor="email" className="form-label">{props.label} <span className="text-body-secondary"></span></label>
            <input readOnly disabled type={props.type ?? "text"} className="form-control" id={props.id ?? ""} placeholder={props.placeholder ?? ""} value={props.value}/>
            <div className="invalid-feedback">{props.feedback ?? "Something is wrong"}</div>
        </div>
    );
}

export function DisabledInput_4col(props){
    return (
        <div className="col-sm-6 col-4 ">
            <label htmlFor={props.htmlFor ?? ""} className="form-label">{props.label}<span className="text-body-secondary"></span></label>
            <input readOnly disabled type={props.type ?? "text"} className="form-control" id={props.id ?? ""} placeholder={props.placeholder ?? ""} value={props.value}/>
        </div>
    );
}

export function DisabledInput_col_sm_6(props){
    return (
        <div className="col-sm-6">
            <label htmlFor="Name" className="form-label">{props.label}</label>
            <input readOnly disabled type={props.type ?? "text"} className="form-control" id={props.id ?? ""} placeholder={props.placeholder ?? ""} value={props.value} required="" />
            <div className="invalid-feedback">{props.feedback ?? "Something is wrong"}</div>
        </div>
    );
}

export function DisabledInput_textArea(props){
    return (
        <div className={"input-group " + props.class}>
            <span className="input-group-text">{props.title}</span>
            <textarea
                readOnly
                disabled
                className="form-control"
                aria-label="With textarea"
                placeholder={props.placeholder}
                onChange={props.handleChange}
                name={props.name ?? "Unknown"}
                value={props.value}
                id={props.id ?? ""}
            />
        </div>
    )
}