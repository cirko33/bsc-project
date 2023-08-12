import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { tableColumns } from "../../../helpers/renderHelpers";

const OrderTable = ({ orders, updateOrders }) => {
  return (
    <TableContainer component={Paper}>
      <Typography variant="h5" align="center" sx={{ my: 2, color: "gray" }}>
        Orders
      </Typography>
      <Table>
        <TableHead>
          <TableRow>
            {orders &&
              orders.length > 0 &&
              Object.keys(orders[0]).map((key, index) => <TableCell key={index}>{key}</TableCell>)}
          </TableRow>
        </TableHead>
        <TableBody>
          {orders &&
            orders.length > 0 &&
            orders.map((order, index) => (
              <TableRow key={index}>
                {Object.keys(order).map((key, index) => (
                  <TableCell key={index}>{tableColumns(key, order)}</TableCell>
                ))}
              </TableRow>
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default OrderTable;
