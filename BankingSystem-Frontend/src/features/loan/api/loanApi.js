import { BaseURL } from "../../../constants/URL";

export default async function getLoans(token){
    const response = await fetch(`${BaseURL}/api/Loan/GetAll`, {
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

export async function getLoanTypes(){
    const response = await fetch(`${BaseURL}/api/Loan/GetLoanTypes`, {
        method: "GET",        
        headers: {
            "Content-Type": "application/json"
        }
    });

    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    var data = await response.json();
    return data;
}

export async function createLoan(CreateLoanDTO, token){
    const response = await fetch(`${BaseURL}/api/Loan/CreateLoan`, {
        method: "POST",        
        headers: {
            "Content-Type": "application/json",
            "Authorization" : `Bearer ${token}`
        },
        body : JSON.stringify(CreateLoanDTO)
    });
    
    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }
    var data = await response.json();
    return data;
}