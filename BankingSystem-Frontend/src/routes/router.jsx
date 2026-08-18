import { createBrowserRouter, createRoutesFromElements, Route } from "react-router-dom";
import RootLayout from "../app/RootLayout";
import { RegisterUser } from "../features/auth/pages/register";
import { Login } from "../features/auth/pages/Login";
import Accounts from "../features/accounts/pages/Accounts";
import AccountTypes from "../features/accounts/pages/AccountTypes";
import AccountBalances from "../features/accounts/pages/AccountsBalances";
import Index from "../components/previews/Index";
import ProtectedRoute from "./ProtectedRoutes";
import PublicRoute from "./PublicRoutes";
import AccountsRoot from "../features/accounts/components/AccountsRoot";
import TransactionsRoot from "../features/transactions/components/TransactionsRoot";
import TransactionsIndex from "../features/transactions/pages/TransactionIndex";
import Transactions from "../features/transactions/pages/Transactions";
import OperationsRoot from "../features/operations/components/OperationsRoot";
import OperationsIndex from "../features/operations/pages/Index";
import LoanRoot from "../features/loan/components/LoanRoot";
import Loans from "../features/loan/pages/Loans";
import TotalPayments from "../features/loan/pages/TotalPayments";
import Error from "../components/previews/Error";
import HomePage from "../features/Main/pages/Home";
import AboutPage from "../features/Main/pages/About";
import Faq from "../features/Main/pages/Faq";
import ProfilesRoot from "../features/customerProfiles/components/ProfilesRoot";
import Profile from "../features/customerProfiles/pages/profile";
import RegisterRoot from "../features/auth/components/RegisterRoot";

  export const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path="/" element={<RootLayout />} errorElement={<Error />}>
        <Route index element={<Index />} />
        <Route path="home" element={<HomePage />}/>
        <Route path="about" element={<AboutPage />}/>
        <Route path="faq" element={<Faq />}/>

        <Route element={<ProtectedRoute />}>
          <Route path="accounts" element={<AccountsRoot />}> 
            <Route index element={<Accounts />} />
            <Route path="balance" element={<AccountBalances />} />
            <Route path="types" element={<AccountTypes />} />
          </Route>   

          <Route path="transactions" element={<TransactionsRoot />}>
            <Route index element={<TransactionsIndex />} />
            <Route path=":accountId" element={<Transactions />} />
          </Route>  

          <Route path="operations" element={<OperationsRoot />}>
            <Route index element={<OperationsIndex />}/>
          </Route>

          <Route path="loans" element={<LoanRoot />}>
            <Route index element={<Loans />} />
            <Route path="monthlyPay" element={<TotalPayments />} />
          </Route>

          <Route path="profile" element={<ProfilesRoot />}>
            <Route index element={<Profile />} />
          </Route>

        </Route>

        <Route element={<PublicRoute />}>

          <Route path="login" element={<Login />} />
          <Route  path="register" element={<RegisterRoot />} >
            <Route index element={<RegisterUser />} />   
          </Route>
          
        </Route>
      </Route>
    )
  );