import "./App.css";
import Navbar from "./components/Navbar/Navbar";
import Router from "./router/Router";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";

const darkTheme = createTheme({
  palette: {
    mode: "dark",
  },
});

const App = () => {
  return (
    <ThemeProvider theme={darkTheme}>
      <Navbar />
      <CssBaseline />
      <div className="container">
        <Router />
      </div>
    </ThemeProvider>
  );
};

export default App;
