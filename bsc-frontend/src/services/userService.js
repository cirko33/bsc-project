import jwtDecode from "jwt-decode";
import api from "../api/api";
import {toFormData} from "../helpers/helpers";

export const getToken = () => {
  return localStorage.getItem("token");
};

export const register = async (data) => {
  try {
    const formData = toFormData(data);
    await api.post("/user/register", formData);
  } catch (e) {
    e.response.data.messages.forEach((element) => {
      alert(element);
    });
    return Promise.reject(e);
  }
};

export const login = async (data) => {
  try {
    const res = await api.post("/user/login", data);
    localStorage.setItem("token", res.data);
  } catch (e) {
    e.response.data.messages.forEach((element) => {
      alert(element);
    });
    return Promise.reject(e);
  }
};

export const logout = () => {
  localStorage.clear();
};

export const getUser = async () => {
  try {
    const res = await api.get("/user");
    return res.data;
  } catch (e) {
    e.response.data.messages.forEach((element) => {
      alert(element);
    });
    return Promise.reject(e);
  }
};

export const setUser = async (data) => {
  try {
    const formData = toFormData(data);
    const res = await api.put("/user", formData);
    return res.data;
  } catch (e) {
    e.response.data.messages.forEach((element) => {
      alert(element);
    });
    return Promise.reject(e);
  }
};

export const userInRole = (role) => {
  try {
    const token = getToken();
    if (!token) return null;
    const tokenDecoded = jwtDecode(token);
    return tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === role;
  } catch (e) {
    console.log(e);
  }
};

export const loggedIn = () => {
  return getToken() !== null;
}