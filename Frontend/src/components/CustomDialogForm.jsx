import React from "react";
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
  Button,
  MenuItem,
  FormControl,
  InputLabel,
  Select,
} from "@mui/material";

const CustomDialogForm = ({
  open, 
  onClose, 
  formData,
  setFormData,
  onSubmit,
  title = "Dialog Form",
  submitButtonText = "Submit",
  fields = [],
  children,
}) => {
  
  const handleChange = (field) => (event) => {
    let value = event.target.value;
    const type = event.target.type;

    if (type === "number") {
      value = parseInt(value);
    }

    setFormData({
      ...formData,
      [field]: value,
    });
  };

  return (
    <Dialog open={open} onClose={() => onClose(false)} maxWidth="sm" fullWidth>
      <DialogTitle align="center">{title}</DialogTitle>
      <DialogContent>
        
        {fields.map(({ label, name, type, required = false, options, rows, disabled, isMultiple = false }) => {
          if (type === "select") {
            
            return (
                <FormControl
                  fullWidth
                  margin="normal"
                  required={required}
                  key={name}
                >
                  <InputLabel>{label}</InputLabel>
                  <Select
                    label={label}
                    value={formData[name] || (isMultiple ? [] : "")}
                    onChange={handleChange(name)}
                    multiple={isMultiple}
                    disabled={disabled}
                  >
                    {options.map((option, index) => (
                      <MenuItem key={index} value={option.value}>
                        {option.label}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              );
          } else if (type === "textarea") {
            return (
              <TextField
                key={name}
                label={label}
                fullWidth
                margin="normal"
                required={required}
                multiline
                rows={rows || 4}
                value={formData[name] || ""}
                onChange={handleChange(name)}
                disabled={disabled}
              />
            );
          } else {          
            return (
              <TextField
                key={name}
                label={label}
                fullWidth
                margin="normal"
                required={required}
                type={type}
                value={formData[name] || ""}
                onChange={handleChange(name)}
                disabled={disabled}
              />
            );
          }
        })}

        {children}
      </DialogContent>
      <DialogActions>
        <Button onClick={() => onClose(false)} color="secondary">
          Cancel
        </Button>
        <Button onClick={onSubmit} color="primary">
          {submitButtonText}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CustomDialogForm; 