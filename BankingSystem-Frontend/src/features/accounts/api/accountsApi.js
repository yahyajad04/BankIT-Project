import { BaseURL } from "../../../constants/URL"

export default async function getAccounts(token, query){
    const params = new URLSearchParams(query);
    const response = await fetch(`${BaseURL}/api/Account?${params.toString()}`, {
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

export async function getAccount(token, id){
    const response = await fetch(`${BaseURL}/api/Account/${id}`, {
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

export async function createAccount(token, createAccountDTO){
    const response = await fetch(`${BaseURL}/api/Account`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body: JSON.stringify(createAccountDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}