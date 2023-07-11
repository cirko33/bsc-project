import {
  Button,
  Card,
  CardActionArea,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
  Typography,
} from "@mui/material";

import { useState } from "react";
import { postProduct } from "../../../services/productService";
import { toast } from "react-toastify";

const ProductAddForm = ({ updateProducts }) => {
  const [data, setData] = useState({
    name: "",
    description: "",
    discount: "",
    price: "",
    imageFile: "",
  });
  const [open, setOpen] = useState(false);

  const handleClose = () => setOpen(false);
  const handleSave = (e) => {
    e.preventDefault();
    if (!data.name || !data.price || !data.description || (!data.discount && data.discount !== 0)) {
      toast.error("All fields are required");
      return;
    }

    if ((!data.discount && data.discount !== 0) || data.discount < 0) {
      toast.error("Discount must be integer");
      return;
    }

    if (!data.price || data.price < 0 || !parseFloat(data.price)) {
      toast.error("Price is floater and must be over 0.");
      return;
    }

    postProduct(data).then(updateProducts);
    setOpen(false);
  };
  const handleChange = (e) => {
    setData({
      ...data,
      [e.target.id]: e.target.value,
    });
  };

  const handleChangeNumber = (e) => {
    let value = "";
    if (e.target.value) value = e.target.value > 0 ? e.target.value : 0;
    if (e.target.id === "discount") value = e.target.value < 100 ? e.target.value : 100;

    setData({
      ...data,
      [e.target.id]: value,
    });
  };
  return (
    <>
      <Dialog open={open} onClose={handleClose} sx={{ color: "white", backgroundColor: "#0c1215" }}>
        <DialogTitle>Add product</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            id="name"
            label="Name"
            type="text"
            fullWidth
            variant="standard"
            value={data.name}
            onChange={handleChange}
            required
          />
          <TextField
            autoFocus
            margin="dense"
            id="price"
            label="Price"
            type="number"
            fullWidth
            variant="standard"
            value={data.price}
            onChange={handleChangeNumber}
            required
          />
          <TextField
            autoFocus
            margin="dense"
            id="discount"
            label="Discount"
            type="number"
            fullWidth
            variant="standard"
            value={data.discount}
            onChange={handleChangeNumber}
            required
          />
          <TextField
            autoFocus
            margin="dense"
            id="description"
            label="Description"
            type="text"
            fullWidth
            variant="standard"
            value={data.description}
            onChange={handleChange}
            required
          />
          <img
            title="Image"
            alt="Add"
            src={data.imageFile ? URL.createObjectURL(data.imageFile) : "default.jpg"}
            className="form-image"
          />
          <div>
            <input
              id="imageFile"
              label="Image"
              type="file"
              accept="image/jpg"
              onChange={(e) => {
                setData({ ...data, imageFile: e.target.files[0] });
              }}
            />
          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSave}>Save</Button>
        </DialogActions>
      </Dialog>
      <Card className="custom-card" sx={{ display: "flex", alignItems: "center", justifyItems: "center" }}>
        <CardActionArea sx={{ display: "flex", height: "100%" }} onClick={() => setOpen(true)}>
          <Typography sx={{ fontSize: 120 }}>+</Typography>
        </CardActionArea>
      </Card>
    </>
  );
};

export default ProductAddForm;
