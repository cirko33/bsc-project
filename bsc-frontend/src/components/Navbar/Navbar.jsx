import { useNavigate } from "react-router-dom";
import { useContext, useState } from "react";
import MenuIcon from "@mui/icons-material/Menu";
import {
  AppBar,
  Box,
  Toolbar,
  IconButton,
  Typography,
  Menu,
  Container,
  Avatar,
  Tooltip,
  MenuItem,
} from "@mui/material";
import { getImageLink, loggedIn, logout } from "../../services/userService";
import UserContext from "../../store/user-context";

const pages = ["Login", "Register"];
const loggedPages = ["Home"];
const settings = ["Profile", "Home", "Logout"];

const Navbar = () => {
  const navigate = useNavigate();
  const { user } = useContext(UserContext);
  const [anchorElNav, setAnchorElNav] = useState(null);
  const [anchorElUser, setAnchorElUser] = useState(null);

  const handleOpenNavMenu = (event) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseNavMenu1 = (page) => {
    setAnchorElNav(null);
    navigate(`/${page.toLowerCase()}`);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const handleCloseUserMenu1 = (setting) => {
    setAnchorElUser(null);
    if (setting.toLowerCase() === "logout") {
      logout();
      navigate("/");
      return;
    }
    navigate(`/${setting.toLowerCase()}`);
  };

  return (
    <AppBar position="static" sx={{ bgcolor: "#2c3e50" }}>
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                fill: "dark",
                display: { xs: "block", md: "none" },
              }}
            >
              {loggedIn()
                ? loggedPages.map((page) => (
                    <MenuItem
                      key={page}
                      onClick={() => handleCloseNavMenu1(page)}
                    >
                      <Typography textAlign="center">{page}</Typography>
                    </MenuItem>
                  ))
                : pages.map((page) => (
                    <MenuItem
                      key={page}
                      onClick={() => handleCloseNavMenu1(page)}
                    >
                      <Typography textAlign="center">{page}</Typography>
                    </MenuItem>
                  ))}
            </Menu>
          </Box>

          <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
            {loggedIn()
              ? loggedPages.map((page) => (
                  <MenuItem
                    key={page}
                    onClick={() => handleCloseNavMenu1(page)}
                  >
                    <Typography textAlign="center">{page}</Typography>
                  </MenuItem>
                ))
              : pages.map((page) => (
                  <MenuItem
                    key={page}
                    onClick={() => handleCloseNavMenu1(page)}
                  >
                    <Typography textAlign="center">{page}</Typography>
                  </MenuItem>
                ))}
          </Box>

          {loggedIn() && (
            <Box sx={{ flexGrow: 0 }}>
              <Tooltip title="Open settings">
                <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                  <Avatar src={user && getImageLink(user.image)} />
                </IconButton>
              </Tooltip>
              <Menu
                sx={{ mt: "45px" }}
                id="menu-appbar"
                anchorEl={anchorElUser}
                anchorOrigin={{
                  vertical: "top",
                  horizontal: "right",
                }}
                keepMounted
                transformOrigin={{
                  vertical: "top",
                  horizontal: "right",
                }}
                open={Boolean(anchorElUser)}
                onClose={handleCloseUserMenu}
              >
                {settings.map((setting) => (
                  <MenuItem
                    key={setting}
                    onClick={() => handleCloseUserMenu1(setting)}
                  >
                    <Typography textAlign="center">{setting}</Typography>
                  </MenuItem>
                ))}
              </Menu>
            </Box>
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default Navbar;
