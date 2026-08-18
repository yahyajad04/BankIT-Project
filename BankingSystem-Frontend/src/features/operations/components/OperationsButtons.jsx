import { PiHandWithdraw } from "react-icons/pi";
import { RiLuggageDepositLine } from "react-icons/ri";
import { FaMoneyBillTransfer } from "react-icons/fa6";

export default function OperationsButtons(props){
    return (
            <div className="row">
                <div className="col-4 mb-5">
                    <button type="button" className="btn create-button btn-lg w-100" data-bs-toggle="modal" data-bs-target="#transfer-id">
                        <FaMoneyBillTransfer className="me-2" size={28}/>
                        Transfer
                    </button>
                </div>
        
                <div className="col-4 mb-5">
                    <button type="button" className="btn create-button-secondary btn-lg w-100" data-bs-toggle="modal" data-bs-target="#deposit-id">
                        <RiLuggageDepositLine className="me-2" size={28}/>
                        Deposit
                    </button>
                </div>
        
                <div className="col-4 mb-5">
                    <button type="button" className="btn create-button btn-lg w-100" data-bs-toggle="modal" data-bs-target="#withdraw-id">
                        <PiHandWithdraw className="me-2" size={28}/>
                        Withdraw
                    </button>
                </div>
            </div>
    )
}