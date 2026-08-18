import { useNavigate , NavLink } from "react-router-dom";
import { useAuth } from "../../features/auth/AuthContext";

export default function UserDropdown(props){
  const {token, setToken} = useAuth();
  const navigate = useNavigate();

  function handleLogout(){
    try{
      if(token){
        setToken(null);
        navigate("/login");
      }
    }catch(err){
      console.log(err);
    }
  }
    return (
        <div className="dropdown">
    <a href="#" className="d-flex align-items-center text-white text-decoration-none dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
      <img src="https://github.com/mdo.png" alt="" width="32" height="32" className="rounded-circle me-2" />
      <strong>
        mdo
      </strong>
    </a>
    <ul className="dropdown-menu dropdown-menu-dark text-small shadow">
      <li>
        <a className="dropdown-item" href="#">
          Settings
        </a>
      </li>
      <li>
        <NavLink className="dropdown-item" to="/profile">
          Profile
        </NavLink>
      </li>
      <li>
        <hr className="dropdown-divider" />
      </li>
      <li>
        <button className="dropdown-item" onClick={handleLogout}>
          Sign out
        </button>
      </li>
    </ul>
  </div>
    )
}