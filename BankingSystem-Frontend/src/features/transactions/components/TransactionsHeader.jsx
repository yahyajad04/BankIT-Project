import { NavLink , useLocation} from "react-router-dom"
import { useState } from "react";

export default function TransactionsHeader(props){
    const location = useLocation();
    const path = location.pathname;
    const account = props.account;
    const role = null;

    return (
    <header className="p-3 mb-3 border-bottom">
    <div className="container">
        <div className="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
            <ul className="nav w-100 col-12 col-lg-auto me-lg-auto mb-2 mb-md-0">
                <li>
                    <NavLink to={`/transactions/${account.id}`} type="button" className="nav-link px-2 link-body-emphasis">Transactions </NavLink>
                </li>
                                
                <div className="vr mx-3"></div>

                <li>
                    <NavLink to="/somewhere" type="button" className="nav-link px-2 link-body-emphasis">Balances </NavLink>
                </li>
                
                <div className="vr mx-3"></div>
                
                <li>
                    <NavLink to="/no" className="nav-link px-2 link-body-emphasis">Available Types</NavLink>
                </li>
            </ul>
        </div>
    </div>
    </header>
    )
}

