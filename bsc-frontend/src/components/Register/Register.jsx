import { AppRegistrationOutlined } from "@mui/icons-material";
import { Button, Card, MenuItem, Select, TextField, Typography } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";
import { useState } from "react";
import { login, register } from "../../services/userService";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const navigate = useNavigate();
  const [data, setData] = useState({
    username: "",
    password: "",
    email: "",
    fullName: "",
    birthday: dayjs("1/1/1999"),
    address: "",
    type: "1",
    imageFile: "",
  });

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
    setData({ ...data, birthday: dayjs(data.birthday).format("DD/MM/YYYY") });

    await register(data);
    alert("Registered successfully");
    await login({ username: data.username, password: data.password });
    navigate("/");
  };

  return (
    <Card component="form" sx={{ padding: "20px", margin: "20px", maxWidth: "24rem", bgcolor: "#2f3e6f" }}>
      <Typography variant="h4" sx={{ marginBottom: "30px", textAlign: "center" }}>
        Register
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
        label="Birthday"
        value={data.birthday}
        type="date"
        inputFormat="DD/MM/YYYY"
        format="DD/MM/YYYY"
        onChange={(e) => {
          const val = dayjs(e.$d);
          if (
            !dayjs(val).isValid() ||
            dayjs(val).isAfter(dayjs().subtract(18, "years")) ||
            dayjs(val).isBefore(dayjs("1/1/1900"))
          ) {
            alert("Invalid birthday");
            return;
          }

          setData({ ...data, birthday: val });
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
      <img src={data.imageFile ? URL.createObjectURL(data.imageFile) : "./default.jpg"} className="img-profile" />
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
        <MenuItem value="1">Seller</MenuItem>
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
