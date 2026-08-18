import { BaseURL } from "../../../constants/URL";

export default async function getProfile(token){
    const response = await fetch(`${BaseURL}/api/CustomerProfile/GetProfile`, {
        method: "GET",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        }
    });

    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;

}

export async function editProfile(token, profileDTO){
        const response = await fetch(`${BaseURL}/api/CustomerProfile/Edit`, {
        method: "PUT",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body: JSON.stringify(profileDTO)
    });

    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}