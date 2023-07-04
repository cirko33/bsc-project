import { Button, Card, TextField, Typography } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import { useState } from "react";

const Profile = () => {
  const [data, setData] = useState({
    username: "",
    password: "",
    email: "",
    fullName: "",
    birthday: "",
    address: "",
  });

  const handleChange = (e) => {
    setData({ ...data, [e.target.name]: e.target.value });
  };

  const submit = (e) => {
    e.preventDefault();
    alert("A");
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
      <TextField
        required
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.email}
        name="email"
        type="email"
        label="Email"
        onChange={handleChange}
      />
      <TextField
        required
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.fullName}
        name="fullName"
        type="text"
        label="Full Name"
        onChange={handleChange}
      />
      <DatePicker
        required
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.birthday}
        type="date"
        min="1900-01-01"
        max={`${new Date().getFullYear() - 18}-01-01`}
        onChange={(e) => {
          console.log(e);
          setData({ ...data, birthday: e });
        }}
      />
      <TextField
        required
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.address}
        name="address"
        type="text"
        label="Address"
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
      ></Button>
    </Card>
  );
};

export default Profile;
