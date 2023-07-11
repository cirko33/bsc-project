import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import { getImageLink, userInRole } from "../../services/userService";
import { useState } from "react";
import ProductUpdateForm from "./Forms/ProductUpdateForm";
import ProductKeys from "./Forms/ProductKeys";
import ProductAddForm from "./Forms/ProductAddForm";
import { deleteProduct } from "../../services/productService";

const Products = ({ title, products, updateProducts }) => {
  const [data, setData] = useState({});
  const [open, setOpen] = useState(false);
  const [keysOpen, setKeysOpen] = useState(false);

  return (
    <div>
      <Typography variant="h4" sx={{ mb: "20px", color: "wheat", display: "flex", justifyContent: "center" }}>
        {title}
      </Typography>
      {userInRole("Seller") && (
        <>
          <ProductUpdateForm
            open={open}
            setOpen={setOpen}
            data={data}
            setData={setData}
            updateProducts={updateProducts}
          />
          <ProductKeys
            open={keysOpen}
            setOpen={setKeysOpen}
            data={data}
            setData={setData}
            updateProducts={updateProducts}
          />
          {/* <ProductAddForm updateProducts={updateProducts} /> */}
        </>
      )}
      <div className="card-container">
        {userInRole("Seller") && <ProductAddForm updateProducts={updateProducts} />}

        {products &&
          products.length > 0 &&
          products.map((p, index) => (
            <Card key={index} className="custom-card">
              <CardMedia
                component="img"
                alt="No pic"
                sx={{ maxHeight: "180px", width: "100%", objectFit: "cover" }}
                src={getImageLink(p.image)}
              />
              <CardContent>
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Seller: {p.seller.fullName}</Typography>
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Name: {p.name}</Typography>
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Price: {p.price.toFixed(2)}$</Typography>
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Discount: {p.discount}%</Typography>

                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>
                  Current price: {(p.price * (1 - p.discount / 100)).toFixed(2)}$
                </Typography>

                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Amount: {p.amount}</Typography>
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Description: {p.description}</Typography>
              </CardContent>
              <CardActions>
                {userInRole("Seller") && (
                  <>
                    <Button
                      size="small"
                      sx={{ fontWeight: "bold" }}
                      color="info"
                      onClick={() => {
                        setData(p);
                        setKeysOpen(true);
                      }}
                    >
                      Keys
                    </Button>
                    <Button
                      size="small"
                      sx={{ fontWeight: "bold" }}
                      color="success"
                      onClick={() => {
                        setData({ ...p, imageFile: "" });
                        setOpen(true);
                      }}
                    >
                      Edit
                    </Button>
                    <Button
                      size="small"
                      sx={{ fontWeight: "bold" }}
                      color="error"
                      onClick={() => deleteProduct(p.id).then(updateProducts)}
                    >
                      Delete
                    </Button>
                  </>
                )}

                {/* {userInRole("Buyer") && (
                <>
                  <Button
                    variant="contained"
                    sx={{
                      minWidth: "20px",
                      minHeight: "20px",
                      maxWidth: "20px",
                      maxHeight: "20px",
                      marginRight: "10px",
                      marginLeft: "10px",
                    }}
                    onClick={() => changeValue(p.id, cart[p.id] - 1, p.amount)}
                  >
                    {"<"}
                  </Button>
                  <input
                    className={classes.numb}
                    pattern="[0-9]{0,4}"
                    placeholder="0"
                    value={cart[p.id]}
                    onChange={(e) => changeValue(p.id, e.target.value, p.amount)}
                  />
                  <Button
                    sx={{
                      minWidth: "20px",
                      minHeight: "20px",
                      maxWidth: "20px",
                      maxHeight: "20px",
                      marginRight: "10px",
                      marginLeft: "10px",
                    }}
                    variant="contained"
                    onClick={(e) => changeValue(p.id, cart[p.id] + 1, p.amount)}
                  >
                    {">"}
                  </Button>
                </>
              )} */}
              </CardActions>
            </Card>
          ))}
      </div>
    </div>
  );
};

export default Products;
