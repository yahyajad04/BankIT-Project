import { useState } from "react";
import { Input_col_sm_6 , Input_textArea } from "../../../components/common/Input";
import { createAccountType } from "../api/accountTypesApi";
import { useAuth } from "../../auth/AuthContext";

export default function CreateType(props){
    const {token} = useAuth();
    const [desc, setDesc] = useState("");
    const [typeName, setTypeName] = useState("");

    function handleChange(event){
        const {value,name} = event.target;

        if(name === "typeName")
            setTypeName(value)
        else
            setDesc(value);
    }

    async function handleClick(event){
        event.preventDefault();
        const createTypeDTO = {
            Name: typeName,
            Description: desc
        }
        try{
            const data = await createAccountType(token, createTypeDTO);
            console.log(data);
            await props.refresh();
        }catch(err){
            console.log(err);
        }
    }

    return (
         <div>
            <form method="POST">
                <Input_col_sm_6 
                htmlFor="balance" 
                placeHolder="Enter the Type Name" 
                type="text" label="Type" 
                id="Type-id"
                handleChange={handleChange}
                value={typeName}
                name="typeName" />

                <Input_textArea
                title="description"
                handleChange={handleChange}
                name="description"
                value={desc}
                id="description-id" />
                
                <button onClick={handleClick} type="submit" className="btn btn-dark mt-3" data-bs-dismiss="modal">Create Type</button>
            </form>
        </div>
    )
}