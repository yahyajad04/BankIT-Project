import { useRouteError, Link } from "react-router-dom";

export default function Error() {
    const error = useRouteError();

    return (
        <div className="container d-flex justify-content-center align-items-center vh-100">
            <div className="text-center">
                <h1 className="display-1 fw-bold text-danger">
                    404
                </h1>

                <h2 className="mb-3">
                    Something went wrong
                </h2>

                <p className="text-muted mb-4">
                    {error?.message || "The page you are looking for does not exist."}
                </p>

                <Link 
                    to="/" 
                    className="btn btn-dark px-4"
                >
                    Go Back Home
                </Link>
            </div>
        </div>
    );
}