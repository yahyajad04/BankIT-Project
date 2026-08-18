import { NavLink , useLocation} from "react-router-dom"
import { useState } from "react";

export default function AccountsHeader(props){
    const location = useLocation();
    const path = location.pathname;
    const role = null;
    return (
    <header className="p-3 mb-3 border-bottom">
    <div className="container">
        <div className="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
            <ul className="nav w-100 col-12 col-lg-auto me-lg-auto mb-2 mb-md-0">
                <li>
                    <NavLink to="/accounts" type="button" className="nav-link px-2 link-body-emphasis">Accounts </NavLink>
                </li>
                                
                <div className="vr mx-3"></div>

                <li>
                    <NavLink to="/accounts/balance" type="button" className="nav-link px-2 link-body-emphasis">Balances </NavLink>
                </li>
                
                <div className="vr mx-3"></div>
                
                <li>
                    <NavLink to="/accounts/types" className="nav-link px-2 link-body-emphasis">Available Types</NavLink>
                </li>

                {(path === "/accounts") && <li className="ms-auto">
                    <button type="button" className="btn create-button" data-bs-toggle="modal" data-bs-target="#create-account-id">
                        Create Account
                    </button>
                </li>}
                 {(path === "/accounts/types" && role === "Admin") && <li className="ms-auto">
                    <button type="button" className="btn create-button" data-bs-toggle="modal" data-bs-target="#accounts-type-create-id">
                        Create New Type
                    </button>
                </li>}
            </ul>
        </div>
    </div>
    </header>
    )
}

