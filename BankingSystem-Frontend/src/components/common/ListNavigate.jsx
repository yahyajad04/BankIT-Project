import { NavLink } from "react-router-dom";

export default function ListNavigate(props){
    return (
        <li className="nav-item">
            <NavLink to={props.path} className={`${props.class ?? ""}`}>
                {props.icon ?? ""}
                {props.title}  
            </NavLink>
        </li>
    )
}