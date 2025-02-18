import React, { useEffect } from 'react';
import { styled, useTheme } from '@mui/material/styles';
import MuiDrawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import { Description, PeopleAlt, PersonAdd, Quiz, School } from '@mui/icons-material';
import { Link, useLocation } from 'react-router-dom';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import PsychologyIcon from '@mui/icons-material/Psychology';
import WorkIcon from '@mui/icons-material/Work';
import { useAuth } from '../contexts/AuthContext';
import WorkHistoryIcon from '@mui/icons-material/WorkHistory';
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts';

const drawerWidth = 240;

const openedMixin = (theme) => ({
  width: drawerWidth,
  transition: theme.transitions.create('width', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.enteringScreen,
  }),
  overflowX: 'hidden',
});

const closedMixin = (theme) => ({
  transition: theme.transitions.create('width', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  overflowX: 'hidden',
  width: `calc(${theme.spacing(7)} + 1px)`,
  [theme.breakpoints.up('sm')]: {
    width: `calc(${theme.spacing(8)} + 1px)`,
  },
});

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'isOpen' })(
  ({ theme, isOpen }) => ({
    width: drawerWidth,
    flexShrink: 0,
    whiteSpace: 'nowrap',
    boxSizing: 'border-box',
    ...(isOpen && {
      ...openedMixin(theme),
      '& .MuiDrawer-paper': openedMixin(theme),
    }),
    ...(!isOpen && {
      ...closedMixin(theme),
      '& .MuiDrawer-paper': closedMixin(theme),
    }),
  }),
);


const Sidebar = ({ isOpen, onClose, selectedIndex, onItemClick }) => {
  const theme = useTheme();
  const location = useLocation();
  const { roles } = useAuth();

  let menuItems = [];

  if (roles?.includes("ADMIN")) {
    menuItems = [
      { text: 'Candidate Management', icon: <PersonAdd />, link: '/user/candidate-management' },
      { text: 'User Management', icon: <ManageAccountsIcon />, link: '/user/user-management' },
      { text: 'Skill Management', icon: <PsychologyIcon />, link: '/user/skill-management' },
      { text: 'Job Position Management', icon: <WorkIcon />, link: '/user/job-management' },
    ];
  } else if (roles?.includes("CANDIDATE")) {
    menuItems = [
      { text: 'Job Position', icon: <WorkIcon />, link: '/candidate/job-positions' },
      { text: 'Job Applications', icon: <WorkHistoryIcon />, link: '/candidate/job-applications' },
      { text: 'Profile', icon: <AccountBoxIcon />, link: '/candidate/profile' },
    ]
  } else if (roles?.includes("RECRUITER")) {
    menuItems = [
      { text: 'Candidate Management', icon: <PersonAdd />, link: '/recruiter/candidate-management' },
      { text: 'Job Position Management', icon: <WorkIcon />, link: '/recruiter/job-management' },
    ];
  } else if (roles?.includes("HR")) {
    menuItems = [
      { text: 'Candidate Management', icon: <PersonAdd />, link: '/hr/candidate-management' },
      { text: 'Job Position Management', icon: <WorkIcon />, link: '/hr/job-management' },
    ];
  } else if (roles?.includes("REVIEWER")) {
    menuItems = [
      { text: 'Job Position Management', icon: <WorkIcon />, link: '/reviewer/job-management' },
    ];
  } else if (roles?.includes("INTERVIEWER")) {
    menuItems = [
      { text: 'Job Position Management', icon: <WorkIcon />, link: '/interviewer/job-management' },
    ];
  }

  useEffect(() => {
    const index = menuItems.findIndex((item) => item.link === location.pathname);
    if (index !== -1) {
      onItemClick(index);
    }
  }, [location]);

  return (
    <Drawer variant="permanent" isOpen={isOpen}>
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'flex-end', padding: '10px 8px' }}>
        <IconButton onClick={onClose}>
          {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
        </IconButton>
      </div>
      <Divider />

      <List>



        {menuItems.map((item, index) => (
          <ListItem
            key={item.text}
            disablePadding
            sx={{ display: 'block' }}
            component={Link}
            to={item.link}
          >
            <ListItemButton
              selected={selectedIndex === index}
              onClick={() => onItemClick(index)}
              sx={{
                minHeight: 48,
                px: 2.5,
                color: 'black',
                ...(selectedIndex === index && {
                  backgroundColor: theme.palette.action.selected,
                }),
                justifyContent: isOpen ? 'initial' : 'center',
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  justifyContent: 'center',
                  color: 'black',
                  mr: isOpen ? 3 : 'auto',
                }}
              >
                {item.icon}
              </ListItemIcon>
              <ListItemText
                primary={item.text}
                sx={{ color: 'black', opacity: isOpen ? 1 : 0 }}
              />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Drawer>
  );
}

export default Sidebar;
