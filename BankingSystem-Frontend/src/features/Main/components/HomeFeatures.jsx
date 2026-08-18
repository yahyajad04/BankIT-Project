import TextCardHome from "./TextCard"

export default function HomeFeatures(){
    return(
    <section className="py-5" id="features">
        <div className="container px-5 my-5">
            <div className="row gx-5">
                <div className="col-lg-4 mb-5 mb-lg-0"><h2 className="fw-bolder mb-0">Everything you need to manage your money.</h2></div>
                <div className="col-lg-8">
                    <div className="row gx-5 row-cols-1 row-cols-md-2">
                        <TextCardHome 
                        title="Bank Accounts"
                        body="Open and manage your bank accounts with ease. Keep track of your balances and account details in one place." />
                        <TextCardHome 
                        title="Easy Money Transfers"
                        body="Deposit, withdraw, and transfer money securely between accounts whenever you need to." />
                        <TextCardHome 
                        title="Transactions"
                        body="View and monitor your financial activity with a clear history of your transactions." />
                        <TextCardHome 
                        title="Loans"
                        body="Apply for loans and keep track of your loan status, monthly payments, and remaining balance." />
                    </div>
                </div>
            </div>
        </div>
    </section>
    )
}