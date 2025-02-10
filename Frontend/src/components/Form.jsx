import React from "react";
import { Box, Button, TextField, Typography } from "@mui/material";

const Form = ({ title, buttonLabel, fields, onSubmit, children }) => {
  return (
    <Box component="form" onSubmit={onSubmit} sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
      
      <Typography variant="h4" component="h1" gutterBottom align="center">
        {title}
      </Typography>
      
      {fields.map((field, index) => (
        <TextField
          key={index}
          margin="normal"
          fullWidth
          label={field.label}
          type={field.type || "text"}
          name={field.name}
          value={field.value}
          onChange={field.onChange}
          required={field.required !== false}
          autoComplete="true"
        />
      ))}

      {children}

      <Button type="submit" fullWidth variant="contained" color="primary" sx={{ mt: 3, height: 56 }}>
        {buttonLabel}
      </Button>
    </Box>
  );
};

export default Form;