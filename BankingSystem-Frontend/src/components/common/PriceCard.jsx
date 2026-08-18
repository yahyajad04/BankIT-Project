

export default function PriceCard(props){
    return (
<div className="col card-shadow">
  <div className="card mb-4 rounded-3 shadow-sm">
    <div className="card-header background-dark py-3">
      <h4 className="my-0 fw-normal">
        {props.title}
      </h4>
    </div>
    <div className={props.class ? props.class :"background-main card-body"}>
      <h1 className="card-title pricing-card-title">
        {props.main}
      </h1>
      <ul className="list-unstyled fs-4 mt-3 mb-4">
        <li>
          {props.desc1}
        </li>
        <li>
          {props.desc2}
        </li>
        <li>
          {props.desc3}
        </li>
        <li>
          {props.desc4}
        </li>
      </ul>
    </div>
  </div>
</div>
    )
}