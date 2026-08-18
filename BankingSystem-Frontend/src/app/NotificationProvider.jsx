import { createContext, useContext, useState } from "react";

const NotificationContext = createContext();

export function NotificationProvider({ children }) {
    const [notification, setNotification] = useState(null);

    function showNotification(message, type = "success") {
        setNotification({
            message,
            type
        });

        // remove notification after 3 seconds
        setTimeout(() => {
            setNotification(null);
        }, 5000);
    }

    function hideNotification() {
        setNotification(null);
    }

    return (
        <NotificationContext.Provider 
            value={{ showNotification, hideNotification }}
        >
            {children}

            {notification && (
                <div 
                   className={`alert alert-${notification.type} position-fixed start-50 translate-middle-x w-50 rounded-0 text-center`}
                    role="alert"
                    style={{border: "2px solid black", top: "40px", zIndex: 9999 }}
                >
                    {notification.message}
                </div>
            )}
        </NotificationContext.Provider>
    );
}


export function useNotification() {
    return useContext(NotificationContext);
}