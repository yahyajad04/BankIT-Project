import { useState } from "react";
import DisabledInput,{DisabledInput_4col, DisabledInput_col_sm_6} from "../../../components/common/DisabledInput";

export default function TransactionsInfo(props){
    const fromAccount = props.fromAccount;
    const toAccount = props.toAccount;
    const transaction = props.transaction;

    return (
        <main>
        <div className="container my-2">
                <div className="row g-3 ">
                    {fromAccount && <div className="mb-2">
                        <h4 className="mb-2">From Account Info</h4>
                        <hr className="mb-4" />
                        <DisabledInput_col_sm_6 label="Account Number" htmlFor="accountNumber" type="text" id="accountNumber" placeholder="ex.1234.." value={fromAccount.accountNumber}/>
                        <DisabledInput_col_sm_6 label="UserName" htmlFor="UserName" type="text" id="userName" placeholder="ex.Yahya21" value={fromAccount.profile.appUserDTO.userName}/>
                        <DisabledInput label="Email" htmlFor="email" type="email" id="email" placeholder="you@example.com" value={fromAccount.profile.appUserDTO.email}/>
                    </div>}

                    {toAccount && <div className="mb-2">
                        <h4 className="mb-2">To Account Info</h4>
                        <hr className="mb-4" />
                        <DisabledInput_col_sm_6 label="Account Number" htmlFor="accountNumber" type="text" id="accountNumber2" placeholder="ex.1234.," value={toAccount.accountNumber}/>
                        <DisabledInput_col_sm_6 label="UserName" htmlFor="UserName" type="text" id="userName2" placeholder="ex.Yahya21" value={toAccount.profile.appUserDTO.userName}/>

                        <DisabledInput label="Email" htmlFor="email" type="email" id="email2" placeholder="you@example.com" value={toAccount.profile.appUserDTO.email}/>
                    </div>}

                    <h4 className="mb-2">More Info</h4>
                    <hr className="mb-1" />
                    <div className="row g-3">
                        <DisabledInput_4col label="Transaction Type" htmlFor="transactionType" type="text" id="transactionType" placeholder="Active" value={transaction.type} />
                        <DisabledInput_4col label="Performed By" htmlFor="performedBy" type="text" id="performedBy" placeholder="John@example.com"  value={transaction.performedBy.email} />
                        <DisabledInput_4col label="Amount" type="text" id="amount" placeholder="1234.." value={transaction.amount} />
                    </div>
                    <div className="col-12">
                        <label htmlFor="status" className="form-label">
                            <b>Status: </b> 
                            <span className={transaction.status === "Active" ? "badge text-bg-warning fs-6" : 
                                transaction.status === "Committed" ? "badge text-bg-success fs-6" :
                                "badge text-bg-danger fs-6"
                            }>{transaction.status}</span>
                        </label>
                        
                    </div>
                    <DisabledInput label="Creation Date" htmlFor="Date" type="date" id="date" placeholder="" value={transaction.createdAt.split("T")[0]}/>
                </div>

                <hr className="my-4" />
        </div>
    </main>
    )
}