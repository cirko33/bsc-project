import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import { getImageLink } from "../../../services/userService";
import { putProduct } from "../../../services/productService";

const ProductUpdateForm = ({ open, setOpen, data, setData, updateProducts }) => {
  const handleClose = () => setOpen(false);
  const handleSave = () =>
    putProduct(data).then(() => {
      updateProducts();
      setOpen(false);
    });

  const handleChange = (e) => {
    setData({
      ...data,
      [e.target.id]: e.target.value,
    });
  };

  const handleChangeNumber = (e) => {
    let value = "";
    if (e.target.value) {
      value = e.target.value > 0 ? e.target.value : 0;
    }
    if (e.target.id === "discount") {
      value = value > 100 ? 100 : value;
    }

    setData({
      ...data,
      [e.target.id]: value,
    });
  };

  return (
    <Dialog open={open} onClose={handleClose} sx={{ color: "white", background: "#0c1215" }}>
      <DialogTitle>Edit product</DialogTitle>
      <DialogContent component="form">
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
          min="0"
          step="0.01"
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
          min="0"
          step="1"
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
          src={
            data.imageFile ? URL.createObjectURL(data.imageFile) : data.image ? getImageLink(data.image) : "default.jpg"
          }
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
  );
};

export default ProductUpdateForm;
