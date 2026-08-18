import React, { useState } from "react";
import { registerUser } from "../api/authApi";
import { useNavigate, useOutletContext } from "react-router-dom";
import { useNotification } from "../../../app/NotificationProvider";
import Input , {Input_4col , Input_col_sm_6} from "../../../components/common/Input";
import Spinner from "../../../components/common/Spinner";
import SelectCountry from "./SelectCountry";

export default function RegisterForm(){
      const [name, setName] = useState("");
      const [username, setUsername] = useState("");
      const [email, setEmail] = useState("");
      const [pass, setPass] = useState("");
      const [country, setCountry] = useState("");
      const [city, setCity] = useState("");
      const [nationalId, setNationalId] = useState("");

      const navigate = useNavigate();
      const {showNotification} = useNotification();
      const {countries, loading} = useOutletContext();

      function handleChange(event) {
        var { value, name } = event.target;
    
        if (name === "email") setEmail(value);
        else if(name === "password") setPass(value);
        else if(name === "name") setName(value);
        else if(name === "country") setCountry(value);
        else if (name === "city") setCity(value);
        else if (name === "nationalId") setNationalId(value);
        else setUsername(value);
      }
    
      async function handleClick(event) {
        event.preventDefault();
        const registerDTO = {
            Name : name,
            Username : username,
            Email: email,
            Password: pass,
            NationalId: nationalId,
            Country: country,
            City: city 
        }
        
        try{
            var data = await registerUser(registerDTO);
            showNotification(
                "Registered Account Successfully",
                "success"
            )
            navigate("/login");
        }catch(err){
        showNotification(
            err.message,
            "danger"
        )
        }

      }
        return (
        loading ? <Spinner /> :<main className="form-signin w-50 m-auto">
            <form>
                <h1 className="h3 mb-3 fw-bold fs-1 text-center">Sign Up</h1>
                <div className="row">
                    <div className="col-sm-6">
                        <div className="form-floating mb-1 mx-auto">
                            <input
                                onChange={handleChange}
                                value={name}
                                type="name"
                                className="form-control"
                                id="floatingInput"
                                name="name"
                                placeholder="John"
                            />
                            <label htmlFor="floatingInput">Name</label>
                        </div>
                    </div>

                    <div className="col-sm-6">
                        <div className="form-floating mb-1 mx-auto">
                            <input
                                onChange={handleChange}
                                value={username}
                                type="username"
                                className="form-control"
                                id="floatingInput"
                                name="username"
                                placeholder="ex.John123"
                            />
                            <label htmlFor="floatingInput">Username</label>
                        </div>
                    </div>
                </div>
                
                <div className="form-floating mb-1 mx-auto">
                <input
                    onChange={handleChange}
                    value={email}
                    type="email"
                    className="form-control"
                    id="floatingInput"
                    name="email"
                    placeholder="name@example.com"
                />
                <label htmlFor="floatingInput">Email address</label>
                </div>
                <div className="form-floating mx-auto mb-1">
                <input
                    onChange={handleChange}
                    value={pass}
                    type="password"
                    className="form-control"
                    id="floatingPassword"
                    name="password"
                    placeholder="Password"
                />
                <label htmlFor="floatingPassword">Password</label>
                </div>

                <div className="form-floating mx-auto mb-1">
                    <input
                        onChange={handleChange}
                        value={nationalId}
                        type="text"
                        className="form-control"
                        id="floatingNationalId"
                        name="nationalId"
                        placeholder="National Id"
                    />
                    <label htmlFor="floatingPassword">National Id</label>
                </div>

                <div className="row">
                    <div className="col-sm-6">
                       <div className="form-floating mb-1">
                            <SelectCountry
                                name="country"
                                values={countries}
                                value={country}
                                handleChange={handleChange}
                                title="Select Country"
                            />
                            <label>Country</label>
                        </div>
                    </div>
                    <div className="col-sm-6">
                        <div className="form-floating mx-auto mb-1">
                            <input
                                onChange={handleChange}
                                value={city}
                                type="text"
                                className="form-control"
                                id="floatingCity"
                                name="city"
                                placeholder="City"
                            />
                            <label htmlFor="floatingPassword">City</label>
                        </div>
                    </div>
                </div>

                <div className="mb-4"></div>

                <button
                onClick={handleClick}
                className="btn sign-button btn-dark d-block mx-auto"
                type="submit"
                >
                Sign up
                </button>
            </form>
        </main>
        )
}