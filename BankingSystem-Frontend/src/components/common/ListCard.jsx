
export default function ListCard(props){
    return (
        <li id={props.id ?? ""} 
        className={"list-group-item d-flex justify-content-between lh-sm list-card " + props.class}
        onClick={props.onClick}
        data-bs-toggle={props.dataToggle}
        data-bs-target={props.dataTarget}>
      <div>
        <h6 className="my-0">
          {props.title}
        </h6>
        <small className="text-body-secondary">
          {props.description}
        </small>
      </div>
      <span className="text-body-secondary">
        {props.element}
      </span>
    </li>
    )
}