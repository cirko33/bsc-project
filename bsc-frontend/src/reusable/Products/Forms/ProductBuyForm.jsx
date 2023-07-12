import { Button, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material";
import { createPayPalPayment } from "../../../services/paymentService";

const ProductBuyForm = ({ open, setOpen, data }) => {
  const handleClose = () => setOpen(false);

  return (
    <Dialog open={open} onClose={handleClose} sx={{ color: "white", background: "#0c1215" }}>
      <DialogTitle>Buy product</DialogTitle>
      <DialogContent>
        <Button
          onClick={() => createPayPalPayment(data.id)}
          sx={{ background: "yellow", minHeight: "20px" }}
          variant="contained"
        >
          <img src="paypal.png" alt="paypal" style={{ width: "100px" }} />
        </Button>
        <br />
        <br />
        <Button
          onClick={() => createPayPalPayment(data.id)}
          color="info"
          variant="contained"
          sx={{ minHeight: "45px" }}
        >
          <img src="metamask.png" alt="metamask" style={{ width: "100px" }} />
        </Button>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
};

export default ProductBuyForm;
