import api from "../api/api";
import { throwWarning } from "../helpers/helpers";

export const createPayPalPayment = async (id) => {
  try {
    const response = await api.get(`/payment/paypal/${id}`);
    window.location.replace(response.data);
  } catch (e) {
    throwWarning(e);
    return Promise.reject(e);
  }
};
