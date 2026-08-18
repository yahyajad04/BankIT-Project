import LoginForm from "../components/LoginForm"

export function Login(){

    return (
    <div className="container-fluid">
        <div className="row justify-content-center">
            <div className="col-12 col-md-6">
                <div className="d-flex justify-content-center align-items-center vh-100 p-3 mb-5 mt-5 rounded login-form">
                    <LoginForm />
                </div>
            </div>
        </div>
    </div>
    )
}