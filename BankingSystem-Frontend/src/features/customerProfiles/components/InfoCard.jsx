import { useState } from "react"
import Input from "../../../components/common/Input"
import DisabledInput from "../../../components/common/DisabledInput";
import { useNotification } from "../../../app/NotificationProvider";
import { useAuth } from "../../auth/AuthContext";
import { editProfile } from "../api/customerProfilesApi";
import { useOutletContext } from "react-router-dom";

export default function InfoCard(props){
    const [userName, setUserName] = useState(props.userName ?? "");
    const [email, setEmail] = useState(props.email ?? "");
    const [address, setAddress] = useState(props.address ?? "");
    const [postal, setPostal] = useState(props.postalCode ?? "");

    const {showNotification} = useNotification();
    const {token} = useAuth();
    const {setProfile} = useOutletContext();

    function handleChange(event){
        const {name, value} = event.target;

        if(name === "UserName")
            setUserName(value)
        else if(name === "Email")
            setEmail(value);
        else if(name === "Address")
            setAddress(value);
        else
            setPostal(value);

    }

    async function handleClick(){
        const profileDTO = {
            UserName: userName,
            Email: email,
            Address: address,
            PostalCode: postal
        }

        try{
            const data = await editProfile(token, profileDTO);
            setProfile(data);
            showNotification(
                "Update Successful",
                "success"
            )
        }catch(err){
            showNotification(
                err.message,
                "danger"
                )
        }

    }

    return (
    <div className="col-lg-8">
        <div className="card mb-4">
            <div className="card-body">

                <div className="row">
                    <div className="col-sm-12">
                        <DisabledInput 
                            placeholder="enter your National Id" 
                            label="National Id"
                            value={props.nationalId}
                            id="natioanl-id-Profile"
                            name="NationalId"/>
                    </div>
                </div>

                <hr />

                <div className="row">
                    <div className="col-sm-12">
                        <Input 
                            placeholder="enter your UserName" 
                            label="UserName"
                            value={userName}
                            id="userName-Profile"
                            name="UserName"
                            handleChange={handleChange}/>
                    </div>
                </div>

                <hr />

                <div className="row">
                    <div className="col-sm-12">
                        <Input 
                            placeholder="enter your email" 
                            label="Email"
                            value={email}
                            id="Email-Profile"
                            name="Email"
                            handleChange={handleChange}/>
                    </div>
                </div>

                <hr />

                <div className="row">
                    <div className="col-sm-12">
                        <Input 
                            placeholder="enter your address" 
                            label="Address"
                            value={address}
                            id="address-Profile"
                            name="Address"
                            handleChange={handleChange}/>
                    </div>
                </div>

                <hr />

                <div className="row">
                    <div className="col-sm-12">
                        <Input 
                            placeholder="Postal Code" 
                            label="Postal Code"
                            value={postal}
                            id="postalCode-Profile"
                            name="PostalCode"
                            handleChange={handleChange}/>
                    </div>
                </div>

                <hr />

                <div className="row">
                    <div className="col-sm-3">
                        <button className="btn create-button" onClick={handleClick}>
                            Save
                        </button>
                    </div>
                </div>

            </div>
        </div>
    </div>
    )
}