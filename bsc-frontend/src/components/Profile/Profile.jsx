import { Button, Card, TextField, Typography } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";
import { useContext, useEffect, useState } from "react";
import { getImageLink, putUser } from "../../services/userService";
import { toast } from "react-toastify";
import UserContext from "../../store/user-context";

const Profile = () => {
  const { user, updateUser } = useContext(UserContext);
  const [data, setData] = useState({
    username: "",
    password: "",
    email: "",
    fullName: "",
    birthday: "",
    address: "",
  });
  useEffect(() => {
    setData({ ...data, ...user, birthday: dayjs(user.birthday) });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleChange = (e) => {
    setData({ ...data, [e.target.name]: e.target.value });
  };

  const submit = async (e) => {
    e.preventDefault();
    await putUser(data);
    updateUser(data);
    toast.success("Profile updated successfully");
  };

  return (
    <Card component="form" sx={{ padding: "20px", margin: "20px", maxWidth: "24rem", bgcolor: "#2f3e6f" }}>
      <Typography variant="h4" sx={{ marginBottom: "30px", textAlign: "center" }}>
        Profile
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
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.password}
        name="password"
        type="password"
        label="Password"
        onChange={handleChange}
      />
      <TextField
        sx={{ marginBottom: "10px", width: "100%" }}
        value={data.newPassword}
        name="newPassword"
        type="password"
        label="New Password"
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
      <img
        src={
          data.imageFile ? URL.createObjectURL(data.imageFile) : data.image ? getImageLink(data.image) : "default.jpg"
        }
        className="img-profile"
      />
      <TextField
        required
        sx={{ marginBottom: "10px", width: "100%" }}
        type="file"
        onChange={(e) => {
          e.target.files[0] && setData({ ...data, imageFile: e.target.files[0] });
        }}
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
        Update profile
      </Button>
    </Card>
  );
};

export default Profile;
