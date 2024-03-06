import { Outlet } from "react-router";
import Navbar from "./Components/Navbar/Navbar";
import "./App.css";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";

function App() {
  return (
    <>
      <Navbar />
      <Outlet />
      <ToastContainer/>
    </>
  );
}

export default App;
