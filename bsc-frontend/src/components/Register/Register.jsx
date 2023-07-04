import { AppRegistrationOutlined } from "@mui/icons-material";
import { Button, Card, MenuItem, Select, TextField, Typography } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";
import { useState } from "react";

const Register = () => {
  const [data, setData] = useState({
    username: "",
    password: "",
    email: "",
    fullName: "",
    birthday: "",
    address: "",
    type: "",
    imageFile: "",
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
          setData({ ...data, birthday: dayjs(e.$d).format('YYYY-MM-DD') });
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
      <TextField
        required
        sx={{ marginBottom: "10px", width: "100%" }}
        type="file"
        onChange={(e) => {
          e.target.files[0] && setData({ ...data, imageFile: e.target.files[0] });
        }}
      />
      <Select
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.type}
        name="type"
        label="Type"
        onChange={handleChange}
      >
        <MenuItem value="1" selected>
          Seller
        </MenuItem>
        <MenuItem value="2">Buyer</MenuItem>
      </Select>
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
        <AppRegistrationOutlined />
      </Button>
    </Card>
  );
};

export default Register;
