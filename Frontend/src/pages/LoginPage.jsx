import React, { useEffect, useState } from 'react';
import { Container, Box, Typography, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import { useAuth } from '../contexts/AuthContext';
import { Link, useNavigate } from 'react-router-dom';
import { fireToast } from '../components/fireToast';
import Form from '../components/Form';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loginAs, setLoginAs] = useState('USER');

  const { loginUser, isAuthenticated } = useAuth();

  const options = [
    { value: "USER", label: "USER" },
    { value: "CANDIDATE", label: "CANDIDATE" }
  ]

  const navigate = useNavigate();

  const handleLogin = async (event) => {
    event.preventDefault();

    if(loginAs === "USER") {
      await loginUser({ Email: email, Password: password }, navigate)
      navigate('/user/candidate-management')
    }

    if(loginAs === "CANDIDATE") {
      fireToast("not implemented", "error");
    }
  };

  useEffect(() => {
    if (isAuthenticated) {
      navigate('/user');
    }
  }, [isAuthenticated, navigate]);

  const formFields = [
    {
      label: "Email",
      name: "email",
      type: "email",
      value: email,
      onChange: (e) => setEmail(e.target.value)
    },
    {
      label: "Password",
      name: "password",
      type: "password",
      value: password,
      onChange: (e) => setPassword(e.target.value)
    },
  ];

  const handleChange = (event) => {
    let value = event.target.value;
    setLoginAs(value);
  }

  return (
    <Container maxWidth="xs">
      <Box sx={{ mt: 8, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>

        <Form
          title="Login"
          buttonLabel="Sign In"
          fields={formFields}
          onSubmit={handleLogin}
        >
          <FormControl
            fullWidth
            margin="normal"
            key="LoginAs"
            required = {true}
          >
            <InputLabel>Login As</InputLabel>
            <Select
              label="LoginAs"
              value={loginAs}
              onChange={e => handleChange(e)}
              multiple={false}
              required={true}
            >
              {options.map((option, index) => (
                <MenuItem key={index} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Form>
      </Box>
    </Container>
  );
};

export default LoginPage;