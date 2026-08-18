import ListCard from "../../../components/common/ListCard"
import { useOutletContext } from "react-router-dom";

export default function TotalPayments(){
    const {loans , loading} = useOutletContext();

    function calculateTotal(){
        const total = loans.reduce((sum, loan) => {
            return sum + Math.min(loan.remaintoPay, loan.monthlyRepay);
        }, 0);
        
        return total;
    }

    return (
    <div className="container w-75">
    <h4 className="d-flex justify-content-between align-items-center mb-3">
    <span className="text-primary">
      Montly Payments
    </span>
    <span className="badge bg-primary rounded-pill">
      {loans.length}
    </span>
  </h4>
  <ul className="list-group mb-3">
    <ListCard 
        title={<b className="fs-2">Total (USD)</b>}
        element={<b className="fs-2">{"$" + calculateTotal()}</b>}/>

    {loans.map(loan => {
        return(
            <ListCard 
                key={loan.id}
                title={loan.accountDTO.accountNumber}
                description={loan.createdAt}
                element={"$" + (loan.monthlyRepay >= loan.remaintoPay) ? loan.monthlyRepay : loan.remaintoPay}/>
        )
    })}
  </ul>
</div>
    )
}