import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Typography,
} from "@mui/material";
import {
  createEthereumPayment,
  createPayPalPayment,
} from "../../../services/paymentService";
import { getImageLink } from "../../../services/userService";

const ProductBuyForm = ({ open, setOpen, data }) => {
  const handleClose = () => setOpen(false);

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      sx={{
        color: "white",
        background: "#0c1215",
      }}
    >
      {data && data.seller && (
        <DialogContent>
          <img
            title="Image"
            alt="Add"
            src={
              data.imageFile
                ? URL.createObjectURL(data.imageFile)
                : data.image
                ? getImageLink(data.image)
                : "default.jpg"
            }
            className="dialog-image"
          />
          <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>
            Seller: {data.seller.fullName}
          </Typography>
          <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>
            Name: {data.name}
          </Typography>
          <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>
            Current price: {(data.price * (1 - data.discount / 100)).toFixed(2)}
            $
          </Typography>
          <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>
            Description: {data.description}
          </Typography>
          <Typography variant="h5">Pay with:</Typography>
          <hr />
          <Button
            onClick={() => createPayPalPayment(data.id)}
            sx={{ background: "yellow", minHeight: "20px" }}
            variant="contained"
          >
            <img src="paypal.png" alt="paypal" style={{ width: "100px" }} />
          </Button>
          <Button
            onClick={() => createEthereumPayment(data.id)}
            color="info"
            variant="contained"
            sx={{ minHeight: "45px", marginLeft: "10px" }}
          >
            <img src="metamask.png" alt="metamask" style={{ width: "100px" }} />
          </Button>
        </DialogContent>
      )}

      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
};

export default ProductBuyForm;
