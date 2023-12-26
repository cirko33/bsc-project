import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { dateTimeToString } from "../../../helpers/helpers";

const OrderTable = ({ orders, updateOrders }) => {
  return (
    <TableContainer component={Paper}>
      <Typography variant="h5" align="center" sx={{ my: 2, color: "gray" }}>
        Orders
      </Typography>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Order time</TableCell>
            <TableCell>Price</TableCell>
            <TableCell>Buyer</TableCell>
            <TableCell>Product</TableCell>
            <TableCell>State</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {orders &&
            orders.length > 0 &&
            orders.map((order, index) => (
              <TableRow key={index}>
                <TableCell>{dateTimeToString(order.orderTime)}</TableCell>
                <TableCell>{order.price.toFixed(2)}$</TableCell>
                <TableCell>{order.buyer.email}</TableCell>
                <TableCell>{order.product.name}</TableCell>
                <TableCell>{order.state === 0 ? "Waiting" : order.state === 1 ? "Confirmed" : "Cancelled"}</TableCell>
              </TableRow>
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default OrderTable;
