import {
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
import { tableColumns } from "../../../helpers/renderHelpers";
import { deleteBlock } from "../../../services/userService";

const UsersTable = ({ users, updateUsers }) => {
  return (
    <TableContainer component={Paper}>
      <Typography variant="h5" align="center" sx={{ my: 2, color: "gray" }}>
        Users
      </Typography>
      <Table>
        <TableHead>
          <TableRow>
            {users &&
              users.length > 0 &&
              Object.keys(users[0]).map((key, index) => <TableCell key={index}>{key}</TableCell>)}
          </TableRow>
        </TableHead>
        <TableBody>
          {users &&
            users.length > 0 &&
            users.map((user, index) => (
              <TableRow key={index}>
                {Object.keys(user).map((key, index) => (
                  <TableCell key={index}>{tableColumns(key, user)}</TableCell>
                ))}
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
