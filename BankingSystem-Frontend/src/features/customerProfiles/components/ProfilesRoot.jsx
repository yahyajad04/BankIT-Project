import { useEffect, useState } from "react"
import { useAuth } from "../../auth/AuthContext";
import { useNotification } from "../../../app/NotificationProvider";
import { Outlet } from "react-router-dom";
import getProfile from "../api/customerProfilesApi";

export default function ProfilesRoot(){
    const [profile, setProfile] = useState(null);
    const [loading, setLoaing] = useState(false);
    const {token} = useAuth();
    const {showNotification} = useNotification();

    async function getProf(){
        try{
            setLoaing(true)
            const data = await getProfile(token);
            setProfile(data)
        }catch(err){
            showNotification(
                err.message,
                "danger"
            )
        }finally{
            setLoaing(false);
        }
    }

    useEffect(() => {
        getProf();
    },[])

    return (
        <div className="container">
            <Outlet 
            context={
                {
                    profile,
                    loading,
                    getProf,
                    setProfile
                }
            }/>
        </div>
    )
}