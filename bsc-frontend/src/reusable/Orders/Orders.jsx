import { Button, Card, CardContent, Typography } from "@mui/material";
import { dateTimeToString } from "../../helpers/helpers";
import { useEffect, useState } from "react";
import { userInRole } from "../../services/userService";
import OrderDialog from "./OrderDialog";

const Orders = ({ orders, updateOrders }) => {
  const [open, setOpen] = useState(false);
  const [data, setData] = useState({});

  useEffect(() => {
    setInterval(() => {
      updateOrders();
    }, 60000);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      <OrderDialog open={open} setOpen={setOpen} data={data} />
      <Typography
        variant="h4"
        component="div"
        sx={{
          mb: "20px",
          color: "wheat",
          display: "flex",
          justifyContent: "center",
        }}
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
              {!userInRole("Buyer") && (
                <Typography>Buyer: {o.buyer.fullName}</Typography>
              )}
              <Typography>Product: {o.product.name}</Typography>
              {userInRole("Buyer") && o.state === 1 && (
                <Button
                  onClick={() => {
                    setData(o);
                    setOpen(true);
                  }}
                >
                  Details
                </Button>
              )}
            </CardContent>
          </Card>
        ))}
    </>
  );
};

export default Orders;
