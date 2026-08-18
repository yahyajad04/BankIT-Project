export default function TextCardHome(props){
    return (
        <div className="col mb-5 h-100">
            <div className="feature bg-primary bg-gradient text-white rounded-3 mb-3"><i className="bi bi-collection"></i></div>
            <h2 className="h5">{props.title}</h2>
            <p className="mb-0">{props.body}</p>
        </div>
    )
}