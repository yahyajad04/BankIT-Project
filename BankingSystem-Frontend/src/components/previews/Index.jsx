import React from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "../../features/auth/AuthContext";

export default function Index(){
    const {token} = useAuth();
    
    if(token)
        return <Navigate to="/accounts" replace />;
    else
        return <Navigate to="/login" replace />;
    
}