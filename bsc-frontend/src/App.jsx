import "./App.css";
import Navbar from "./components/Navbar/Navbar";
import Router from "./router/Router";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { UserContextProvider } from "./store/user-context";

const darkTheme = createTheme({
  palette: {
    mode: "dark",
  },
});

const App = () => {
  return (
    <UserContextProvider>
      <ThemeProvider theme={darkTheme}>
        <ToastContainer
          position="top-center"
          autoClose={1500}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
          theme="dark"
        />
        <Navbar />
        <CssBaseline />
        <div className="container">
          <Router />
        </div>
      </ThemeProvider>
    </UserContextProvider>
  );
};

export default App;
