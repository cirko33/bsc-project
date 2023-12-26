import {
  Avatar,
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import { deleteBlock, getImageLink } from "../../../services/userService";
import { dateToString } from "../../../helpers/helpers";

const UsersTable = ({ users, updateUsers }) => {
  return (
    <TableContainer component={Paper}>
      <Typography variant="h5" align="center" sx={{ my: 2, color: "gray" }}>
        Users
      </Typography>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Username</TableCell>
            <TableCell>Email</TableCell>
            <TableCell>Full name</TableCell>
            <TableCell>Birthday</TableCell>
            <TableCell>Address</TableCell>
            <TableCell>Type</TableCell>
            <TableCell>Image</TableCell>
            <TableCell>Action</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {users &&
            users.length > 0 &&
            users.map((user, index) => (
              <TableRow key={index}>
                <TableCell>{user.username}</TableCell>
                <TableCell>{user.email}</TableCell>
                <TableCell>{user.fullName}</TableCell>
                <TableCell>{dateToString(user.birthday)}</TableCell>
                <TableCell>{user.address}</TableCell>
                <TableCell>{user.type}</TableCell>
                <TableCell>
                  <Avatar alt="Profile pic" width={40} height={20} src={getImageLink(user.image)} />
                </TableCell>
                <TableCell>
                  <Button
                    variant="outlined"
                    color={user.blocked ? "success" : "error"}
                    onClick={() => deleteBlock(user.id).then(() => setTimeout(() => updateUsers(), 1000))}
                  >
                    {user.blocked ? "Unblock" : "Block"}
                  </Button>
                </TableCell>
              </TableRow>
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default UsersTable;
