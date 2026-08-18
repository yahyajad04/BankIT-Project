import { BaseURL } from "../../../constants/URL"

export default async function getAccountTypes(token){
    const response = await fetch(`${BaseURL}/api/AccountType`, {
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

export async function createAccountType(token, createTypeDTO){
    const response = await fetch(`${BaseURL}/api/AccountType`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body: JSON.stringify(createTypeDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}