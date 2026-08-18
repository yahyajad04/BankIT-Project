import { NavLink } from "react-router-dom";
import { FaHome, FaUser, FaSignInAlt } from "react-icons/fa";
import logo from "../../assets/Bank-logo.svg";
import { useAuth } from "../../features/auth/AuthContext";
import ListNavigate from "../common/ListNavigate";
import UserDropdown from "../common/UserDropdown";

export default function Header(){
   const {token} = useAuth();

    return (
<header>
   <div className="px-3 py-2 text-bg-dark border-bottom">
      <div className="container">
         <div className="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
            <a href="/" className="d-flex align-items-center my-2 my-lg-0 me-lg-auto text-white text-decoration-none">
               <img className="logo" src={logo} alt="Bank Logo" width={60} height={50} />
               <h2><b>Bank</b><b style={{color: "rgb(169, 220, 230)"}}>IT</b></h2>
            </a>
            <ul className="nav col-12 col-lg-auto my-2 justify-content-center my-md-0 text-small">
               {token ? 
               <UserDropdown />               
               : 
               <div className="d-flex">
                  <ListNavigate 
                     path="/login"
                     class="nav-link text-white"
                     icon={<FaUser className="d-block mx-auto mb-1" size={20} />}
                     title="Login" />

                  <ListNavigate 
                     path="/register"
                     class="nav-link text-white"
                     icon={<FaSignInAlt className="d-block mx-auto mb-1" size={20} />}
                     title="Register" />
                  </div>}
            </ul>
         </div>
      </div>
   </div>
</header>
    )
}
