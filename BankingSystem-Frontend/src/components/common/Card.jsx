export default function Card(props) {
  return (
    <div className="card card-shadow">
      <div className="card-header">{props.header}</div>
      <div className="card-body">
        <h5 className="card-title">{props.title}</h5>
        <p className="card-text">
          {props.body}
        </p>
        {props.button && <button type="button" className="btn btn-dark" data-bs-toggle="modal" data-bs-target={`#c-${props.id}`}>
            {props.button}
        </button>}
      </div>
    </div>
  );
}
