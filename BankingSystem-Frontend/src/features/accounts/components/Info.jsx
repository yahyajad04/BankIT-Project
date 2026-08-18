import DisabledInput,{DisabledInput_4col, DisabledInput_col_sm_6, DisabledInput_textArea} from "../../../components/common/DisabledInput";

export default function Info(props){
    const account = props.account;
    return (
        <main>
        <div className="container my-5">
            <h4 className="mb-3">Profile Info</h4>
                <div className="row g-3">
                    <DisabledInput_col_sm_6 label="Name" htmlFor="Name" type="text" id="name" placeholder="ex.Yahya" value={account.profile.appUserDTO.name}/>
                    <DisabledInput_col_sm_6 label="UserName" htmlFor="UserName" type="text" id="userName" placeholder="ex.Yahya21" value={account.profile.appUserDTO.userName}/>

                    <DisabledInput label="Email" htmlFor="email" type="email" id="email" placeholder="you@example.com" value={account.profile.appUserDTO.email}/>
                    <hr className="mt-4 mb-4" />

                    <h4 className="mb-3">Account Info</h4>

                    {account.accountName && <div className="row g-3">
                        <DisabledInput label="Account Name" htmlFor="AccountName" type="text" id="accountName" placeholder="ex.Main" value={account.accountName} />
                    </div>}

                    {account.accountDescription && <div className="row g-3">
                        <DisabledInput_textArea title="Account Description" htmlFor="AccountDescription" type="text" id="accountDescription" placeholder="ex.This is for savings" value={account.accountDescription} />
                    </div>}
                    <div className="row g-3">
                        <DisabledInput_4col label="Account Number" htmlFor="account_number" type="text" id="accountNumber" placeholder="1234.." value={account.accountNumber} />
                        <DisabledInput_4col label="Account Type" htmlFor="AccountType" type="text" id="accountType" placeholder="Islamic.."  value={account.accountType.name} />
                        <DisabledInput_4col label="Balance" type="text" id="balance" placeholder="1234.." value={account.balance} />
                    </div>

                    <div className="col-12">
                        <label htmlFor="status" className="form-label">
                            <b>Status: </b> 
                            <span className="badge text-bg-success fs-6">{account.accountStatus}</span>
                        </label>
                        
                    </div>
                    <DisabledInput label="Creation Date" htmlFor="Date" type="date" id="date" placeholder="" value={account.createdAt.split("T")[0]}/>
                </div>

                <hr className="my-4" />
        </div>
    </main>
    )
}