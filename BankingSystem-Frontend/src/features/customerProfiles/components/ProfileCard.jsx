import { NavLink } from "react-router-dom"

export default function ProfileCard(props){
    return(
    <div className="col-lg-4">
        <div className="card mb-4">
            <div className="card-body text-center">
                <img
                    src={props.image ?? "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3.webp"}
                    alt="avatar"
                    className="rounded-circle img-fluid"
                    style={{ width: "150px" }}
                />

                <h5 className="my-3">
                    {props.name ?? ""}
                </h5>

                <p className="text-muted mb-1">
                    {props.email}
                </p>

                <p className="text-muted mb-4">
                    {props.country ? props.country + (props.city ? "," + props.city : "") : props.city ? props.city : ""}
                </p>

                <div className="d-flex justify-content-center mb-2">
                    <NavLink
                        to="/home"
                        type="button"
                        className="btn create-button"
                    >
                        Go to HomePage
                    </NavLink>

                </div>
            </div>
        </div>
    </div>
    )
}