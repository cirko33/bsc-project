import jwtDecode from "jwt-decode";
import api from "../api/api";
import { throwWarning, toFormData } from "../helpers/helpers";
import { toast } from "react-toastify";

export const getToken = () => {
  return localStorage.getItem("token");
};

export const register = async (data) => {
  try {
    const formData = toFormData(data);
    await api.post("/user/register", formData, {
      headers: { "Content-Type": "multipart/form-data" },
    });
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const login = async (data) => {
  try {
    const res = await api.post("/user/login", data);
    localStorage.setItem("token", res.data);
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const logout = () => {
  localStorage.removeItem("token");
};

export const getUser = async () => {
  try {
    const res = await api.get("/user");
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const putUser = async (data) => {
  try {
    const formData = toFormData(data);
    const res = await api.put("/user", formData, {
      headers: { "Content-Type": "multipart/form-data" },
    });
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const userInRole = (role) => {
  try {
    const token = getToken();
    if (!token) return null;
    const tokenDecoded = jwtDecode(token);
    return (
      tokenDecoded[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ] === role
    );
  } catch (e) {
    console.log(e);
  }
};

export const loggedIn = () => {
  return getToken() !== null;
};

export const getImageLink = (imageName) => {
  if (imageName) return process.env.REACT_APP_LINK + "/user/image/" + imageName;
  return "default.jpg";
};

export const getUsers = async () => {
  try {
    const res = await api.get("/user/all");
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const deleteBlock = async (data) => {
  try {
    const res = await api.delete("/user/block/" + data);
    toast.success("Blocked successfully");
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};
