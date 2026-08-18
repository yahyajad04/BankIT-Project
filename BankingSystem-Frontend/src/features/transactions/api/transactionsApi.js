import { BaseURL } from "../../../constants/URL"

export default async function getTransactions(token, accountId, query){
    const params = new URLSearchParams(query);
    const response = await fetch(`${BaseURL}/api/Transaction/GetAll/${accountId}?${params.toString()}`, {
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

export async function getTransaction(token, id){
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

export async function transfer(token, transferDTO){
    const response = await fetch(`${BaseURL}/api/Transaction/Transfer`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body: JSON.stringify(transferDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}

export async function deposit(token, depositDTO){
    const response = await fetch(`${BaseURL}/api/Transaction/Deposit`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body: JSON.stringify(depositDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}

export async function withdraw(token, withdrawDTO){
    const response = await fetch(`${BaseURL}/api/Transaction/Withdraw`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body: JSON.stringify(withdrawDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}