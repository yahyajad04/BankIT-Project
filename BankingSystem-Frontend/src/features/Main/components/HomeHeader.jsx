import { NavLink } from "react-router-dom"
import { useAuth } from "../../auth/AuthContext"

export default function HomeHeader(props){
    const {token} = useAuth();

    return (
        <header className="bg-dark py-5">
            <div className="container px-5">
                <div className="row gx-5 align-items-center justify-content-center">
                    <div className="col-lg-8 col-xl-7 col-xxl-6">
                        <div className="my-5 text-center text-xl-start">
                            <h1 className="display-5 fw-bolder text-white mb-2">Welcome to BankIT</h1>
                            <p className="lead fw-normal text-white-50 mb-4">A safe way to invest your money!</p>
                            <div className="d-grid gap-3 d-sm-flex justify-content-sm-center justify-content-xl-start">
                                {token ? 
                                <div>
                                    <NavLink className="btn create-button btn-lg px-4 me-sm-3" to="/accounts" >Go to Accounts</NavLink>
                                </div> 
                                : 
                                <div>
                                    <NavLink className="btn create-button btn-lg px-4 me-sm-3" to="/register" >Get Started</NavLink>
                                    <NavLink className="btn btn-outline-light list-card btn-lg px-4" to="/about" >Learn More</NavLink>
                                </div>
                                    }
                            </div>
                        </div>
                    </div>
                    <div className="col-xl-5 col-xxl-6 d-none d-xl-block text-center">
                        <iframe 
                            width="560" 
                            height="315" 
                            src="https://www.youtube.com/embed/oR4IK0guBiU?si=uR3CDXfuQVuQ8kJg&autoplay=1&mute=1" 
                            title="YouTube video player" 
                            frameBorder="0" 
                            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" 
                            referrerPolicy="strict-origin-when-cross-origin" 
                            allowFullScreen></iframe>
                    </div>
                </div>
            </div>
        </header>
    )
}