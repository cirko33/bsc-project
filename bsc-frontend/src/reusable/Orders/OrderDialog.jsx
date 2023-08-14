import { useState } from "react";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Typography } from "@mui/material";
import { getImageLink } from "../../services/userService";
import { dateTimeToString } from "../../helpers/helpers";

const OrderDialog = ({ open, setOpen, data }) => {
  const handleClose = () => {
    setOpen(false);
    setTimeout(() => setKeyShow(false), 200);
  };

  const [keyShow, setKeyShow] = useState(false);

  return (
    <Dialog open={open} onClose={handleClose} sx={{ color: "white", background: "#0c1215" }}>
      <DialogTitle>Your order</DialogTitle>
      <DialogContent>
        <img
          alt="Pic"
          src={data && data.product && data.product.image ? getImageLink(data.product.image) : "default.jpg"}
          className="dialog-image"
        />
        {data && data.product && (
          <>
            <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Product: {data.product.name}</Typography>
            <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Ordered: {dateTimeToString(data.orderTime)}</Typography>
            <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Seller: {data.product.seller.fullName}</Typography>
            <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Paid: {data.price.toFixed(2)}$</Typography>
            <Typography sx={{ fontSize: 14, flexWrap: "wrap", fontWeight: "bold" }}>
              {"Key (don't show to anyone):   "}
              {keyShow ? (
                <span style={{ color: "blue" }}>{data.productKey.key}</span>
              ) : (
                <Button onClick={() => setKeyShow(true)} color="error">
                  Show
                </Button>
              )}
            </Typography>
          </>
        )}
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Close</Button>
      </DialogActions>
    </Dialog>
  );
};

export default OrderDialog;
