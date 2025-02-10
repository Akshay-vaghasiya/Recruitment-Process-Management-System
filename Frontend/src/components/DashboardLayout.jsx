import React, { useState } from 'react';
import Box from '@mui/material/Box';
import CssBaseline from '@mui/material/CssBaseline';
import Navbar from './Navbar';
import Sidebar from './Sidebar';
import { Outlet } from 'react-router-dom';

const DashboardLayout = ({ children }) => {
  const [isSidebarOpen, setIsSidebarOpen] = useState(false); 
  const [selectedIndex, setSelectedIndex] = useState(0);

  const handleDrawerToggle = () => {
    setIsSidebarOpen(!isSidebarOpen);
  };

  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      
      <Navbar isSidebarOpen={isSidebarOpen} handleDrawerToggle={handleDrawerToggle} />
      
      <Sidebar 
        isOpen={isSidebarOpen} 
        onClose={handleDrawerToggle} 
        selectedIndex={selectedIndex} 
        onItemClick={(index) => setSelectedIndex(index)} 
      />
      
      <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
        <div style={{ minHeight: '64px' }} />
        <Outlet />
      </Box>
    </Box>
  );
}

export default DashboardLayout;
