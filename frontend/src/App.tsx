import { Outlet } from "react-router";
import Navbar from "./Components/Navbar/Navbar";
import "./App.css";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
import { UserProvider } from "./context/UseAuth";

function App() {
  return (
    <>
      <UserProvider>
      <Navbar />
      <Outlet />
      <ToastContainer/>
      </UserProvider>
    </>
  );
}

export default App;
