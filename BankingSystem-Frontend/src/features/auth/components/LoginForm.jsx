import React, { useState } from "react";
import { login } from "../api/authApi";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../AuthContext";
import { useNotification } from "../../../app/NotificationProvider";

export default function LoginForm(props){
  const [email, setEmail] = useState("");
  const [pass, setPass] = useState("");
  const navigate = useNavigate();
  const {setToken} = useAuth();
  const {showNotification} = useNotification();

  function handleChange(event) {
    var { value, name } = event.target;

    if (name === "email") setEmail(value);
    else if(name === "password") setPass(value);
  }

  async function handleClick(event) {
    event.preventDefault();
    const loginDTO = {
        Email: email,
        Password: pass 
    }
    try{
        var data = await login(loginDTO);
        setToken(data.token);
        showNotification(
            "Logged In Successfully",
            "success"
        )
        navigate("/accounts");
    }catch(err){
        showNotification(
            err.message,
            "danger"
        )
        navigate("/login");
    }

  }
    return(
        <main className="form-signin w-50">
        <form>
            <h1 className="login-title text-center">Sign In</h1>
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
            <div className="form-floating mb-4 mx-auto">
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
            <button
            onClick={handleClick}
            className="btn sign-button btn-dark d-block mx-auto"
            type="submit"
            >
            Sign In
            </button>
        </form>
    </main>
    )
}