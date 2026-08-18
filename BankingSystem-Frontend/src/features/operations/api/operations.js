import { BaseURL } from "../../../constants/URL";

export default async function getTransactionsAll(token,query){
    const params = new URLSearchParams(query);
    const response = await fetch(`${BaseURL}/api/Transaction/GetAll?${params.toString()}`, {
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

export async function getAccountByNumber(AccountNumber,token){
    const response = await fetch(`${BaseURL}/api/Account/GetByNumber/${AccountNumber}`, {
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

export async function transfer(transferDTO, token){
    const response = await fetch(`${BaseURL}/api/Transaction/Transfer`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body : JSON.stringify(transferDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}

export async function withdraw(withdrawDTO, token){
    const response = await fetch(`${BaseURL}/api/Transaction/WithDraw`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body : JSON.stringify(withdrawDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }
    var data = await response.json();
    return data;
}

export async function deposit(depositDTO, token){
    const response = await fetch(`${BaseURL}/api/Transaction/Deposit`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body : JSON.stringify(depositDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}