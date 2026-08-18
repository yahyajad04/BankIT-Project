export default function AccountsCard(props) {
    const account = props.account;
  return (
    <div className="card card-shadow">
      <div className="card-header">Account Number: <b>{account.accountNumber}</b></div>
      <div className="card-body">
        <h5 className="card-title">{account.accountName ?? account.profile.appUserDTO.email}</h5>
        <p className="card-text">
          Yout Balance Is: <b>{account.balance}</b>
        </p>
        <button type="button" className="btn btn-dark" data-bs-toggle="modal" data-bs-target={`#account-${account.id}`}>
            Account Info
        </button>
      </div>
    </div>
  );
}
