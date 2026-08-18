import { useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import { useNotification } from "../../../app/NotificationProvider";
import { getCountries } from "../api/authApi";

export default function RegisterRoot(){
    const [countries, setCountries] = useState([]);
    const [loading ,setLoading] = useState(false)
    const {showNotification} = useNotification();

    async function fetchCountries(){
        try{
            setLoading(true);
            const data = await getCountries();
            setCountries(data);
        }catch(err){
            showNotification(
                err.message
                ,"danger"
            )
        }finally{
            setLoading(false);
        }
    }

    useEffect(() => {
        fetchCountries();
    }, [])

    return(
        <div>
            <Outlet context={
                {
                    countries,
                    loading
                }
            }/>
        </div>
    )
}