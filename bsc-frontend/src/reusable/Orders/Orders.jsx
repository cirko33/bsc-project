import { Card, CardContent, Typography } from "@mui/material";
import { dateTimeToString } from "../../helpers/helpers";
import { useEffect } from "react";

const Orders = ({ orders, updateOrders }) => {
  useEffect(() => {
    setInterval(() => {
      updateOrders();
    }, 60000);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      <Typography
        variant="h4"
        component="div"
        sx={{ mb: "20px", color: "wheat", display: "flex", justifyContent: "center" }}
      >
        Orders
      </Typography>
      {orders &&
        orders.length > 0 &&
        orders.map((o, index) => (
          <Card key={index} sx={{ minWidth: 100, marginTop: "10px" }}>
            <CardContent>
              <Typography>Ordered: {dateTimeToString(o.orderTime)}</Typography>
              <Typography>Price: {o.price.toFixed(2)}$</Typography>
              <Typography>Buyer: {o.buyer.fullName}</Typography>
              <Typography>Product: {o.product.name}</Typography>
            </CardContent>
          </Card>
        ))}
    </>
  );
};

export default Orders;
