import { Avatar } from "@mui/material";
import { getImageLink } from "../services/userService";
import { dateTimeToString, dateToString } from "./helpers";

export const tableColumns = (key, item) => {
  switch (key) {
    case "image":
      return <Avatar alt="Profile pic" width={40} height={20} src={getImageLink(item.image)} />;
    case "birthday":
      return dateToString(item[key]);
    case "orderTime":
      return dateTimeToString(item[key]);
    case "price":
      return item[key].toFixed(2) + "$";
    case "buyer":
      return item[key].email;
    case "product":
      return item[key].name;
    case "state":
      switch (item[key]) {
        case 0:
          return "Waiting";
        case 1:
          return "Confirmed";
        case 2:
          return "Canceled";
        default:
          return "Waiting";
      }
    default:
      return item[key];
  }
};
