import { BaseURL } from "../../../constants/URL";

export async function registerUser(registerDTO){
const response = await fetch(`${BaseURL}/api/AppUser/Register`, {
  method: "POST",        
  headers: {
    "Content-Type": "application/json"
  },
  body: JSON.stringify(registerDTO)     
});

    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }
else
    return await response.statusText;

}

function registerAdmin(registerDTO){

}

function registerEmployee(registerDTO){

}

export async function login(loginDTO){
const response = await fetch(`${BaseURL}/api/AppUser/Login`, {
  method: "POST",        
  headers: {
    "Content-Type": "application/json"
  },
  body: JSON.stringify(loginDTO)     
});

    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }
else{
    var data = await response.json();
    return data;
}
}

export async function getCountries() {
  const response = await fetch(`${BaseURL}/api/AppUser/Countries`, {
    method: "GET",        
    headers: {
      "Content-Type": "application/json"
    }   
  });

    if(!response.ok){
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

  else{
      var data = await response.json();
      return data;
  }
}