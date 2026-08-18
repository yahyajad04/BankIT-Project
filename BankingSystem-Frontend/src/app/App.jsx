import { useState } from 'react'
import { RegisterUser } from '../features/auth/pages/register'
import {router} from "../routes/router";
import { RouterProvider } from 'react-router-dom';
import { AuthProvider } from "../features/auth/AuthContext";

function App() {

  return (
  <div>
    <AuthProvider>
      <RouterProvider router = {router}/>
    </AuthProvider>
  </div>
  )
}

export default App
