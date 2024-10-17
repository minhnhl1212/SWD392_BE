import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter,Routes,Route } from "react-router-dom"
import './index.css';
import Login from './pages/Login';
import Home from './pages/Home';
import Register from './pages/Register';

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
   <BrowserRouter>
    <Routes>
      
      
        <Route path="/login" element={<Login />}/>
        <Route path="/home" element={<Home />}/>
        <Route path="/register" element={<Register />}/>
        
        {/* <Route path="report" element={<Report />}/> */}
        {/* <Route path="/productPage/:productId" element={<ProductPage />} /> */}
   
      
      
    </Routes>
    </BrowserRouter>
  </React.StrictMode>
);
