export default function AboutMember(props){
    return (
    <div className="col mb-5 mb-5 mb-xl-0">
        <div className="text-center">
            <img className="img-fluid rounded-circle mb-4 px-4" src={props.image} alt="..."/>
            <h5 className="fw-bolder">{props.name}</h5>
            <div className="fst-italic text-muted">{props.position}</div>
        </div>
    </div>
    )
}