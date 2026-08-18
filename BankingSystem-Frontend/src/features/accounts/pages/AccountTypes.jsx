import { useState , useEffect } from "react";
import Card from "../../../components/common/Card"
import { useAuth } from "../../auth/AuthContext";
import getAccountTypes from "../api/accountTypesApi";
import Modal from "../../../components/common/Modal";
import CreateType from "../components/CreateTypes";
import { MdTypeSpecimen } from "react-icons/md";

export default function AccountTypes(){
    const {token} = useAuth();
    const [types, setTypes] = useState([]);

     useEffect(() => {
        fetchAccountTypes();
    }, []);

    async function fetchAccountTypes() {
        try {
            const data = await getAccountTypes(token);
            setTypes(data);
        } catch (err) {
            console.error(err);
        }
    }     

    return (
    <div className="container">
        <div className="row mb-4">
            <div className="col-12">
                <MdTypeSpecimen className="mx-2" size={42}/>
                <b className="fs-3 main-color">Available Account Types</b>
            </div>
        </div>

        <div className="row">
            <div className="col-12">
                <ul className="list-group mb-3">
                    {types.map(type => {
                        return (
                        <li  key={type.id} className="list-group-item border-0 p-0 mb-3">
                            <Card title={type.name} body={type.description} />
                        </li>
                        )
                    })}
                </ul>
            </div>
            <div className="col-4">
                <Modal id="accounts-type-create-id" title="Create Type" element={<CreateType refresh={fetchAccountTypes} />}/>
            </div>  
        </div>
    </div>
    )
}