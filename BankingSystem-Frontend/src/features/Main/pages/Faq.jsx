import ListCard from "../../../components/common/ListCard"

export default function Faq(){
    return (
        <div className="container my-3">
            <h1 className="main-color">FAQ</h1>
            <div className="row">
                <ul className="list-group mb-3">
                    <ListCard
                        id="FAQ-1"
                        title="How do I create a new bank account?"
                        element="You can create a new bank account by navigating to the Accounts section and selecting the option to create an account. Follow the required steps and provide the necessary information to complete the process."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-2"
                        title="How can I check my account balance?"
                        element="Your current account balance can be viewed from the Accounts section of your dashboard. Select the account you want to view to see its balance and other account details."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-3"
                        title="How can I deposit money into my account?"
                        element="You can deposit money by selecting the Deposit option for the account you want to fund. Enter the required amount and confirm the transaction to complete the deposit."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-4"
                        title="How can I withdraw money from my account?"
                        element="To withdraw money, select the Withdraw option for the desired account, enter the amount you wish to withdraw, and confirm the transaction. The amount will be deducted from your available balance."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-5"
                        title="How can I transfer money to another account?"
                        element="You can transfer money by selecting the Transfer option and providing the destination account and the amount you want to send. Review the transaction details and confirm the transfer."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-6"
                        title="Where can I view my transaction history?"
                        element="Your transaction history is available in the Transactions section. There you can review your deposits, withdrawals, transfers, and other financial activity associated with your account."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-7"
                        title="How can I apply for a loan?"
                        element="You can apply for a loan through the Loans section. Select the option to apply for a loan, provide the required information, and submit your application for review."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-8"
                        title="How can I check the status of my loan?"
                        element="You can check your loan status from the Loans section. The system will display the current status of your application along with relevant loan information."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-9"
                        title="How can I track my loan payments?"
                        element="Your loan details include information about your monthly repayment amount, remaining balance, and other payment details, allowing you to keep track of your loan progress."
                        className="mb-2"
                    />

                    <ListCard
                        id="FAQ-10"
                        title="Is my banking information secure?"
                        element="We take the security of your banking information seriously. Access to your account is protected through authentication and authorization mechanisms designed to ensure that only permitted users can access financial information and perform transactions."
                        className="mb-2"
                    />
                </ul>
            </div>
        </div>
    )
}