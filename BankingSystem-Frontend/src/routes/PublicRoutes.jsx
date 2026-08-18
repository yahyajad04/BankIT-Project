import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../features/auth/AuthContext";

export default function PublicRoute() {
    const { token } = useAuth();

    return token ? <Navigate to="/accounts" replace /> : <Outlet />;
}