import React from 'react';
import { styled } from '@mui/material/styles';
import MuiAppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import { Button } from '@mui/material';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import LogoutIcon from '@mui/icons-material/Logout';
import { fireToast } from './fireToast';

const drawerWidth = 240;


const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== 'isSidebarOpen', 
})(({ theme, isSidebarOpen }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen, 
  }),
  ...(isSidebarOpen && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`, 
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

const Navbar = ({ isSidebarOpen, handleDrawerToggle }) => {
  const { logout } = useAuth();
  const navigate = useNavigate();
  
  const handleLogout = () => {
    fireToast("Logged out successfully", "success");
    logout();
    navigate('/');
  };

  return (
    <AppBar position="fixed" isSidebarOpen={isSidebarOpen}> 
      <Toolbar>
        <IconButton
          color="inherit"
          aria-label="open drawer"
          onClick={handleDrawerToggle} 
          edge="start"
          sx={{ marginRight: 5, ...(isSidebarOpen && { display: 'none' }) }} 
        >
          <MenuIcon /> 
        </IconButton>
        
        <Typography variant="h6" noWrap>
          Roima Intelligence
        </Typography>

        <Button
          variant="text"
          sx={{
            backgroundColor: '#3f51b5',
            color: '#ffffff', 
            '&:hover': {
              backgroundColor: '#303f9f',
            },
            padding: '8px 16px',
            marginLeft: 'auto', 
          }}
          onClick={handleLogout} 
          startIcon={<LogoutIcon />}
        >
          Logout
        </Button>
      </Toolbar>
    </AppBar>
  );
}

export default Navbar; 