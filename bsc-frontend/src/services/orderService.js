import api from "../api/api";
import { throwWarning } from "../helpers/helpers";

export const getOrders = async () => {
  try {
    const res = await api.get("/order");
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};

export const getSellerOrders = async () => {
  try {
    const res = await api.get("/order/seller");
    return res.data;
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};
