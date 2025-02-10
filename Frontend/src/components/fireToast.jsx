import { Bounce, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

export const fireToast = (message, type) => {
    
    toast(message, {
        type: type, 
        position: "top-right", 
        autoClose: 3000, 
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true, 
        progress: undefined, 
        theme: "light", 
        transition: Bounce,
    });
};