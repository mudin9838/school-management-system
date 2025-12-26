import { useState } from 'react'
import './App.css'
import Dashboard from './pages/Dashboard'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Login from './auth/Login'
import ProtectedRoute from './auth/ProtectedRoute'

function App() {
  return (
    
//set routes
<BrowserRouter>
<Routes>
   <Route path="/login" element={<Login />} />
   <Route path="/" element={
     <ProtectedRoute>
       <Dashboard />
     </ProtectedRoute>
   } />
</Routes>
</BrowserRouter>
  )
}

export default App
