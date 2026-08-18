import { MdOutlineMailOutline } from "react-icons/md";
import { CiPhone } from "react-icons/ci";
import { IoLogoLinkedin } from "react-icons/io5";
import { FaGithub , FaInstagram } from "react-icons/fa";
import { NavLink } from "react-router-dom";

export default function Footer(){
    return (
        <footer className="py-4 border-top">
            <div className="container">
                <div className="row">
                    <div className="col mb-3">
                        </div>
                        <div className="col mb-3">
                            <h5>
                            Socials
                            </h5>
                            <ul className="nav flex-column">
                            <li className="nav-item mb-2">
                                <a href="https://www.linkedin.com/in/yahya-jad-9227a1248/" className="nav-link p-0 text-body-secondary">
                                    <IoLogoLinkedin className="me-1" size={22}/> LinkedIn
                                </a>
                            </li>
                            <li className="nav-item mb-2">
                                <a href="#" className="nav-link p-0 text-body-secondary">
                                    <FaInstagram className="me-1" size={22}/> Instagram
                                </a>
                            </li>
                            <li className="nav-item mb-2">
                                <a href="https://github.com/yahyajad04" className="nav-link p-0 text-body-secondary">
                                    <FaGithub className="me-1" size={22}/> Github
                                </a>
                            </li>
                            </ul>
                        </div>
                        <div className="col mb-3">
                            <h5>
                            Personal Info
                            </h5>
                            <ul className="nav flex-column">
                            <li className="nav-item mb-2">
                                <a href="mailto:yahyajad04@yahoo.com" className="nav-link p-0 text-body-secondary">
                                    <MdOutlineMailOutline className="me-1" size={22}/> yahyajad04@yahoo.com
                                </a>
                            </li>
                            <li className="nav-item mb-2">
                                <a href="tel:0798087126" className="nav-link p-0 text-body-secondary">
                                    <CiPhone className="me-1" size={22}/> 0798087126
                                </a>
                            </li>
                            </ul>
                        </div>
                        <div className="col mb-3">
                            <h5>
                            Other Links
                            </h5>
                            <ul className="nav flex-column">
                            <li className="nav-item mb-2">
                                <NavLink to="/about" className="nav-link p-0 text-body-secondary">
                                About Us
                                </NavLink>
                            </li>
                            <li className="nav-item mb-2">
                                <NavLink to="/faq" className="nav-link p-0 text-body-secondary">
                                faQ
                                </NavLink>
                            </li>
                            </ul>
                    </div>
                </div>
            </div>
        </footer>
    )
}