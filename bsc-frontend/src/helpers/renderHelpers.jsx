import { Avatar } from "@mui/material";
import { getImageLink } from "../services/userService";
import { dateToString } from "./helpers";

export const tableColumns = (key, item) => {
  switch (key) {
    case "image":
      return <Avatar alt="Profile pic" width={40} height={20} src={getImageLink(item.image)} />;
    case "birthday":
      return dateToString(item[key]);
    default:
      return item[key];
  }
};
