import React from "react";
import Header from "../components/previews/Header";
import {Outlet} from "react-router-dom";
import Sidebar from "../components/previews/Sidebar";
import { NotificationProvider } from "./NotificationProvider";
import Footer from "../components/previews/Footer";

export default function RootLayout(){
    return (
        <NotificationProvider>
            <div className="d-flex flex-column min-vh-100">
                <Header />

                <main className="d-flex flex-grow-1">
                    <Sidebar />

                    <div className="d-flex flex-column flex-grow-1">
                        <div className="flex-grow-1">
                            <Outlet />
                        </div>

                        <Footer />
                    </div>
                </main>
            </div>
        </NotificationProvider>
    )
}