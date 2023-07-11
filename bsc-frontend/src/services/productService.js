import { toFormData } from "axios";
import api from "../api/api";
import { throwWarning } from "../helpers/helpers";

export const getSellerProducts = async () => {
  try {
    const res = await api.get("/product/seller");
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const getProducts = async () => {
  try {
    const res = await api.get("/product");
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const getProduct = async (data) => {
  try {
    const res = await api.get("/product/" + data);
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const postProduct = async (data) => {
  try {
    const formData = toFormData(data);
    const res = await api.post("/product", formData, { headers: { "Content-Type": "multipart/form-data" } });
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const putProduct = async (data) => {
  try {
    const formData = toFormData(data);
    const res = await api.put("/product", formData, { headers: { "Content-Type": "multipart/form-data" } });
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const deleteProduct = async (data) => {
  try {
    await api.delete("/product/" + data);
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const postProductKey = async (data) => {
  try {
    const res = await api.post("/product/key", data);
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const deleteProductKey = async (data) => {
  try {
    await api.delete("/product/key/" + data);
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};
