import logo from './logo.svg';
import Home from './pages/Home';
import './App.css';
import Login from './pages/Login';
import Register from './pages/Register';
import {Outlet, Link } from "react-router-dom";

function App() {
  return (
    <div className="App">
      {/* <Login/> */}
      <Home/>
      {/* <Register/> */}
      <Outlet/>
    </div>
  );
}

export default App;
