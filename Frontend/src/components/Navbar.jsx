import React, { useEffect, useState } from 'react';
import { styled } from '@mui/material/styles';
import MuiAppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import { Button, Badge, Popover, Box, List, ListItem, ListItemText } from '@mui/material';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import LogoutIcon from '@mui/icons-material/Logout';
import NotificationsIcon from '@mui/icons-material/Notifications';
import { fireToast } from './fireToast';
import notificationService from '../services/notificationService';
import AuthHeader from '../helper/AuthHeader';

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
  const { logout, roles, candidate, user } = useAuth();
  const navigate = useNavigate();
  const [anchorEl, setAnchorEl] = useState(null);
  const { getCandidateNotification, getNotification, updateNotification, updateCandidateNotification } = notificationService;
  const headers = AuthHeader();
  const [notifications, setNotifications] = useState([]);
  const flag = roles.includes("CANDIDATE");

  const fetchNotification = async () => {

    try {
      let data = [];
      if (flag) {
        data = await getCandidateNotification(candidate.PkCandidateId, headers);
      } else {
        data = await getNotification(user.PkUserId, headers);
      }
      data.sort((a,b) => new Date(b.CreatedAt) - new Date(a.CreatedAt))
      setNotifications(data);
    } catch (error) {
      if (error?.response?.status === 401 || error?.response?.status === 403) {
        logout();
        navigate("/");
        fireToast("Unauthorized access", "error");
      }
      fireToast(error?.response?.data, "error");
    }
  }


  useEffect(() => {
    fetchNotification();
  }, [])

  const handleLogout = () => {
    fireToast("Logged out successfully", "success");
    logout();
    navigate('/');
  };


  const handleNotificationClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleNotificationClose = () => {
    setAnchorEl(null);
  };

  const markAsRead = async (id) => {
    try {
      let apiData = {
        IsRead: true
      }
      if (flag) {
        await updateCandidateNotification(id, apiData, headers);
      } else {
        await updateNotification(id, apiData, headers);
      }

      await fetchNotification();
    } catch (error) {
      if (error?.response?.status === 401 || error?.response?.status === 403) {
        logout();
        navigate("/");
        fireToast("Unauthorized access", "error");
      }
      fireToast(error?.response?.data, "error");
    }
  };


  const unreadCount = notifications.filter((notification) => !notification.IsRead).length;

  const isPopoverOpen = Boolean(anchorEl);

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

        <IconButton
          color="inherit"
          aria-label="notifications"
          onClick={handleNotificationClick}
          sx={{ marginRight: 2, marginLeft: 'auto' }}
        >
          <Badge badgeContent={unreadCount} color="error">
            <NotificationsIcon />
          </Badge>
        </IconButton>

        <Popover
          open={isPopoverOpen}
          anchorEl={anchorEl}
          onClose={handleNotificationClose}
          anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'right',
          }}
          transformOrigin={{
            vertical: 'top',
            horizontal: 'right',
          }}
          PaperProps={{
            sx: {
              width: '300px',
              maxWidth: '90%',
              maxHeight: '275px',
              overflowY: 'auto',
            },
          }}
        >
          <List>
            {notifications.length > 0 ? (
              notifications.map((notification) => (
                <ListItem
                  key={notification.PkNotificationId}
                  onClick={() => {
                    markAsRead(notification.PkNotificationId);
                  }}
                  sx={{
                    whiteSpace: 'normal',
                    padding: '8px 16px',
                    cursor: 'pointer',
                    '&:hover': {
                      backgroundColor: '#f5f5f5',
                    },
                  }}
                >
                  <ListItemText
                    primary={notification.Message}
                    primaryTypographyProps={{
                      sx: {
                        wordWrap: 'break-word',
                        fontWeight: notification.IsRead ? 'normal' : 'bold',
                      },
                    }}
                  />
                </ListItem>
              ))
            ) : (
              <ListItem disabled>
                <ListItemText primary="No new notifications" />
              </ListItem>
            )}
          </List>
        </Popover>

        {/* Logout Button */}
        <Button
          variant="text"
          sx={{
            backgroundColor: '#3f51b5',
            color: '#ffffff',
            '&:hover': {
              backgroundColor: '#303f9f',
            },
            padding: '8px 16px',
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