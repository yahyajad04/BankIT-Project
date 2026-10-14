import { useOutletContext } from "react-router-dom"
import PriceCard from "../../../components/common/PriceCard";
import Modal from "../../../components/common/Modal";
import CreateLoan from "../components/CreateLoan";
import Spinner from "../../../components/common/Spinner";

export default function Loans(){
    const {loans, loading, refresh} = useOutletContext();

    return (
        <div className="container mx-5">
            {loading ?  <Spinner /> : loans.length > 0 ? <div className="row mb-5">
                {loans.map(loan => {
                return (
                    <div key={loan.id} className="col-sm-12 col-md-6 col-lg-4 col-xl-4 mt-4">
                        <PriceCard
                            title={"Id - " + loan.id + " | Account - " + loan.accountDTO.accountNumber}
                            main={"Amount : $ " + loan.amount}
                            class="card-body"
                            desc1={"remain to pay: $ " + loan.remaintoPay}
                            desc2={
                                <label className="form-label">
                                    <b>Status: </b>
                                    <span className={loan.status == "Pending" ? "badge text-bg-warning fs-6" 
                                        : loan.status == "Approved" ? "badge text-bg-success fs-6" : "badge text-bg-danger fs-6"
                                    }>
                                        {loan.status}
                                    </span>
                                </label>}/>
                    </div>
                )
                })}  
                <div className="col-4">
                    <Modal 
                        id="loan-create-id"
                        element={<CreateLoan />}/>
                </div>          
            </div>
            : <div>
                <b>There is no Loans for this Profile!</b>
                <div className="col-4">
                    <Modal 
                        id="loan-create-id"
                        element={<CreateLoan />}/>
                </div>  
                </div>
            }
        </div>
    )
}