import { toast } from "react-toastify";

export const toFormData = (data) => {
  const formData = new FormData();
  Object.keys(data).forEach((key) => formData.append(key, data[key]));
  return formData;
};

export const throwWarning = (obj) => {
  let rep = "";
  if (obj.response.data.errors)
    Object.values(obj.response.data.errors).forEach((element) => {
      rep += element + "\n";
    });
  else rep += obj.response.data.Exception;

  toast.warning(rep);
};

export const dateToString = (date) => {
  return new Date(date).toLocaleDateString("en-GB");
};

export const dateTimeToString = (date) => {
  return new Date(date).toLocaleString("en-GB");
};
