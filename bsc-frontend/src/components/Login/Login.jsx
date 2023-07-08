import { LoginOutlined } from "@mui/icons-material";
import { Button, Card, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { login } from "../../services/userService";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const [data, setData] = useState({
    username: "",
    password: "",
  });
  const navigate = useNavigate();
  const handleChange = (e) => {
    setData({ ...data, [e.target.name]: e.target.value });
  };

  const submit = async (e) => {
    e.preventDefault();
    for (let key in data) {
      if (data[key] === "") {
        alert("Please fill all fields");
        return;
      }
    }

    await login(data);
    navigate("/dashboard");
  };

  return (
    <Card component="form" sx={{ padding: "20px", margin: "20px", maxWidth: "20rem", bgcolor: "#2f3e6f" }}>
      <Typography variant="h4" sx={{ marginBottom: "30px", textAlign: "center" }}>
        Login
      </Typography>

      <TextField
        required
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.username}
        name="username"
        type="text"
        label="Username"
        onChange={handleChange}
      />
      <TextField
        required
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.password}
        name="password"
        type="password"
        label="Password"
        onChange={handleChange}
      />
      <Button
        variant="contained"
        sx={{
          marginTop: "10px",
          width: "100%",
          color: "#fff",
          backgroundColor: "#2f3e6f",
          ":hover": { backgroundColor: "#2f3e6f" },
        }}
        type="submit"
        onClick={submit}
      >
        <LoginOutlined />
      </Button>
    </Card>
  );
};

export default Login;
