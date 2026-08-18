import { NavLink , useLocation} from "react-router-dom"
import { useState } from "react";

export default function LoansHeader(props){
    const location = useLocation();
    const path = location.pathname;
    const role = null;
    return (
    <header className="p-3 mb-3 border-bottom">
    <div className="container">
        <div className="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
            <ul className="nav w-100 col-12 col-lg-auto me-lg-auto mb-2 mb-md-0">
                <li>
                    <NavLink to="/loans" type="button" className="nav-link px-2 link-body-emphasis">Loans </NavLink>
                </li>
                                
                <div className="vr mx-3"></div>
                
                <li>
                    <NavLink to="/loans/monthlyPay" className="nav-link px-2 link-body-emphasis">Monthly Payment</NavLink>
                </li>

                {(path === "/loans") && <li className="ms-auto">
                    <button type="button" className="btn create-button" data-bs-toggle="modal" data-bs-target="#loan-create-id">
                        Create Loan
                    </button>
                </li>}
            </ul>
        </div>
    </div>
    </header>
    )
}

