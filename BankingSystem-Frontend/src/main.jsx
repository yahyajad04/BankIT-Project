import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './app/App.jsx'
import Info from './features/accounts/components/Info.jsx';

createRoot(document.getElementById('root')).render(
    <App />
)
