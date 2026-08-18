import HomeHeader from "../components/HomeHeader"
import HomeFeatures from "../components/HomeFeatures"
import HomeComment from "../components/HomeComment"
import HomeBlogCard from "../components/HomeBlogCard"

import accountsScreen from "../../../assets/HomeScreens/Accounts-Screen1.png";
import accountsScreen2 from "../../../assets/HomeScreens/Accounts-Screen2.png";
import accountsScreen3 from "../../../assets/HomeScreens/Accounts-Screen3.png";

import transactionsScreen from "../../../assets/HomeScreens/Transactions-Screen.png";
import transactionsScreen2 from "../../../assets/HomeScreens/Transactions-Screen2.png";
import transactionsScreen3 from "../../../assets/HomeScreens/Transactions-Screen3.png";
import transactionsScreen4 from "../../../assets/HomeScreens/Transactions-Screen4.png";

import loansScreen from "../../../assets/HomeScreens/Loans-Screen.png";
import loansScreen2 from "../../../assets/HomeScreens/Loans-Screen2.png";


export default function HomePage(){
    return (
        <main className="flex-shrink-0">
            <HomeHeader />
            <HomeFeatures />
            <HomeComment />

            <section className="py-5">
                <div className="container px-5 my-5">
                    <div className="row gx-5 justify-content-center">
                        <div className="col-lg-8 col-xl-6">
                            <div className="text-center">
                                <h2 className="fw-bolder">Banking Insights</h2>
                                <p className="lead fw-normal text-muted mb-5">Discover helpful tips, features, and insights to make managing your finances easier.</p>
                            </div>
                        </div>
                    </div>

                    <div className="row gx-5">
                       <HomeBlogCard 
                            title="Manage Your Bank Accounts"
                            tag="Accounts"
                            body="Easily view your accounts, check balances, and access your account information from one convenient dashboard."
                            images={[accountsScreen, accountsScreen2, accountsScreen3]}
                            auther="Yahya Jad"
                            date="21st of March 2026"/>
                        <HomeBlogCard 
                            title="Banking Made Simple"
                            tag="Transactions"
                            body="Deposit, withdraw, and transfer funds with ease while keeping track of all your financial activity through your transaction history."
                            images={[transactionsScreen2, transactionsScreen, transactionsScreen3, transactionsScreen4]}
                            auther="Yahya Jad"
                            date="7th of July 2026"/>
                       <HomeBlogCard 
                            title="Manage your Loans"
                            tag="Loans"
                            body="Apply for loans and keep track of your loan status, monthly payments, and remaining balance all in one place."
                            images={[loansScreen, loansScreen2]}
                            auther="Yahya Jad"
                            date="13th of August 2026"/>
                    </div>
                </div>
            </section>
        </main>

    )
}