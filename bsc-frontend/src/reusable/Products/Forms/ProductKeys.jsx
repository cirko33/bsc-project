import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
} from "@mui/material";
import { useState } from "react";
import { deleteProductKey, postProductKey } from "../../../services/productService";

const ProductKeys = ({ open, setOpen, data, setData, updateProducts }) => {
  const handleClose = () => setOpen(false);
  const [newKey, setNewKey] = useState("");
  const refresh = () => {
    setNewKey("");
    updateProducts();
  };

  return (
    <Dialog open={open} onClose={handleClose} sx={{ color: "white", background: "#0c1215" }}>
      <DialogTitle>Keys</DialogTitle>
      <DialogContent>
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Key</TableCell>
                <TableCell>Action</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              <TableRow>
                <TableCell>
                  <TextField
                    autoFocus
                    margin="dense"
                    id="key"
                    label="Key"
                    type="text"
                    fullWidth
                    variant="standard"
                    value={newKey}
                    onChange={(e) => setNewKey(e.target.value)}
                    required
                  />
                </TableCell>
                <TableCell>
                  <Button
                    color="info"
                    variant="outlined"
                    onClick={() =>
                      postProductKey({ key: newKey, productId: data.id }).then((res) => {
                        refresh();
                        setData({ ...data, keys: [...data.keys, { key: newKey, id: res }] });
                      })
                    }
                  >
                    Add
                  </Button>
                </TableCell>
              </TableRow>
              {data &&
                data.keys &&
                data.keys.map((item, index) => (
                  <TableRow key={index}>
                    <TableCell>{item.key}</TableCell>
                    <TableCell>
                      <Button
                        color="error"
                        variant="outlined"
                        onClick={() =>
                          deleteProductKey(item.id).then(() => {
                            refresh();
                            setData({ ...data, keys: data.keys.filter((i) => i.id !== item.id) });
                          })
                        }
                      >
                        Delete
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
            </TableBody>
          </Table>
        </TableContainer>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Finish</Button>
      </DialogActions>
    </Dialog>
  );
};

export default ProductKeys;
