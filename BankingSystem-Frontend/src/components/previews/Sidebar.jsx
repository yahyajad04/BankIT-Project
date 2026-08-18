import { useState } from "react";
import { MdOutlineMenu , MdAccountBalance } from "react-icons/md";
import { GrTransaction } from "react-icons/gr";
import { BiMoneyWithdraw } from "react-icons/bi";
import { PiHandCoinsFill } from "react-icons/pi";
import { FaHome } from "react-icons/fa";
import { FaQuestion } from "react-icons/fa6";
import { LuInfo } from "react-icons/lu";
import { NavLink } from "react-router-dom";
import ListNavigate from "../common/ListNavigate";

export default function Sidebar(props){
    const [collapse, setCollapse] = useState(true);
    function handleColapse(event){
        setCollapse(prev => !prev);
    }

return (
    <div className={"d-flex flex-column flex-shrink-0 p-3 text-bg-dark sidebar" + (collapse ? " collapsed" : "")}>
    <div className="d-flex justify-content-between align-items-center">
        {!collapse && <span className="fs-4 text-white">Menu</span>}
        <button
        type="button"
        className="btn border-0 text-white p-1"
        style={{ background: "transparent" }}
        onClick={handleColapse}>
        <MdOutlineMenu size={collapse ? 36 : 28} />
        </button>
    </div>
  <hr />

  <ul className="nav nav-pills flex-column mb-auto">

    {!collapse && <label className="fs-6 mb-2 text-white-50">Main</label>}

    <ListNavigate path="/home" 
        class="nav-link d-flex align-items-center text-white" 
        icon={<FaHome className="me-2" size={collapse ? 28 : 22}/>}
        title= { !collapse && <span>Home</span>}/>

    <ListNavigate path="/about" 
        class="nav-link d-flex align-items-center text-white" 
        icon={<LuInfo className="me-2" size={collapse ? 28 : 22}/>}
        title= { !collapse && <span>About</span>}/>

        <ListNavigate path="/faq" 
        class="nav-link d-flex align-items-center text-white" 
        icon={<FaQuestion className="me-2" size={collapse ? 28 : 22}/>}
        title= { !collapse && <span>FAQ</span>}/>
    <hr />
    {!collapse && <label className="fs-6 mb-2  text-white-50">Services</label>}

    <ListNavigate path="/accounts" 
        class="nav-link d-flex align-items-center text-white" 
        icon={<MdAccountBalance className="me-2" size={collapse ? 28 : 22}/>}
        title= { !collapse && <span>Accounts</span>}/>
    <ListNavigate 
        path="/transactions" 
        class="nav-link d-flex align-items-center text-white" 
        icon={<GrTransaction className="me-2" size={collapse ? 28 : 22}/>}
        title= { !collapse && <span>Transactions</span>}/>
    <ListNavigate 
        path="/operations" 
        class="nav-link d-flex align-items-center text-white" 
        icon={<BiMoneyWithdraw className="me-2" size={collapse ? 28 : 22}/>}
        title= { !collapse && <span>Operations</span>}/>
    <ListNavigate 
        path="/loans" 
        class="nav-link d-flex align-items-center text-white" 
        icon={<PiHandCoinsFill className="me-2" size={collapse ? 28 : 22}/>}
        title= { !collapse && <span>Loans</span>}/>
    </ul>

  <hr />
  
</div>
)
}