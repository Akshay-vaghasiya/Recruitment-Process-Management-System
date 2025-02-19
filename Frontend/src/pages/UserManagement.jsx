import React, { useEffect, useState } from "react";
import TableComponent from "../components/TableComponent";
import CustomDialogForm from "../components/CustomDialogForm";
import ConfirmationDialog from "../components/ConfirmationDialog";
import { Button, Box, Typography, CircularProgress, TextField, IconButton, Switch } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useNavigate } from "react-router-dom";
import { useUserContext } from "../contexts/UserContext";
import authService from "../services/authService";
import AuthHeader from "../helper/AuthHeader";
import { fireToast } from "../components/fireToast";
import { useAuth } from "../contexts/AuthContext";

const UserManagement = () => {

    const { searchTerm, isLoading, isError, users, filteredUsers, getAllUsers, addUser, searchUser, editUser, removeUser } = useUserContext();
    const {logout} = useAuth();
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [selectedUser, setSelectedUser] = useState(null);
    const [formData, setFormData] = useState({});
    const navigate = useNavigate();
    const { getAllRoles } = authService;
    const headers = AuthHeader();
    const [roles, setRoles] = useState([]);

    const today = new Date().toISOString().split("T")[0];

    useEffect(() => {

        if (!users.length) {
            getAllUsers(navigate);
        }

    }, [users]);

    useEffect(() => {
        const fetchRoles = async () => {
            try {
                const data = await getAllRoles(headers);
                setRoles(
                    data?.map((role) => {
                        return { label: role.Name, value: role.Name }
                    })
                )
            } catch (error) {
                if (error?.response?.status === 401 || error?.response?.status === 403) {
                    logout();
                    navigate("/");
                    fireToast("Unauthorized access", "error");
                }
                fireToast(error?.response?.data, "error");
            }
        }

        fetchRoles();
    }, [])

    const handleAdd = () => {
        setFormData({
            FullName: "",
            Email: "",
            Phone: "",
            Password: "",
            JoiningDate: today,
            LeavingDate: null,
            Roles: []
        });
        setSelectedUser(null);
        setIsFormOpen(true);
    };

    const handleEdit = (user) => {
        user.Password = "";

        if (user.LeavingDate == null) {
            user.LeavingDate = "";
        }
        setFormData(user);
        setSelectedUser(user);
        setFormData((prev) => ({
            ...prev, Roles: user?.UserRoles?.map((role) => {
                return role.FkRole.Name;
            })
        }));
        setIsFormOpen(true);
    };

    const handleDelete = (user) => {
        setSelectedUser(user);
        setIsConfirmOpen(true);
    };

    const handleSubmit = () => {
        if (selectedUser) {
            editUser(selectedUser.PkUserId, formData, navigate);
            getAllUsers(navigate);
        } else {
            addUser(formData, navigate);
        }
        setIsFormOpen(false);
    };

    const handleConfirmDelete = () => {
        removeUser(selectedUser.PkUserId, navigate);
        setIsConfirmOpen(false);
    };

    const handleSearch = (event) => {
        searchUser(event.target.value);
    };


    const columns = ["ID", "FullName", "Email", "Phone", "JoiningDate", "CreatedAt", "Roles"];
    const datacolumns = ["PkUserId", "FullName", "Email", "Phone", "JoiningDate", "CreatedAt", "roles"];


    const formFields = [
        { label: "FullName", name: "FullName", type: "text", required: true },
        { label: "Email", name: "Email", type: "email", required: true },
        { label: "Phone", name: "Phone", type: "text", required: true },
        { label: "Password", name: "Password", type: "password", required: true },
        {
            label: "Roles", name: "Roles", type: "select", options: roles,
            isMultiple: true, required: true
        },
    ];

    const actions = [
        {
            label: "Edit",
            color: "primary",
            handler: handleEdit,
            icon: <EditIcon />,
        },
        {
            label: "Delete",
            color: "error",
            handler: handleDelete,
            icon: <DeleteIcon />,
        }
    ];

    if (isLoading) {
        return <CircularProgress />;
    }

    if (isError) {
        return (
            <Typography color="error">
                Error loading Users. Please try again later.
            </Typography>
        );
    }

    return (
        <Box>
            <Typography variant="h4" align="center" gutterBottom>
                User Management
            </Typography>

            <Box sx={{ display: "flex", justifyContent: "flex-start", marginBottom: "1rem", gap: "1.5rem", marginTop: "1.5rem" }}>
                <TextField
                    label="Search Exams"
                    variant="outlined"
                    value={searchTerm}
                    onChange={handleSearch}
                    fullWidth
                    sx={{ maxWidth: "40%" }}
                />
                <Button
                    variant="contained"
                    color="primary"
                    startIcon={<AddIcon />}
                    onClick={handleAdd}
                >
                    Add User
                </Button>
            </Box>

            <TableComponent
                columns={columns}
                data={filteredUsers}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={selectedUser ? "Edit User" : "Add User"}
                submitButtonText={selectedUser ? "Update" : "Create"}
                fields={formFields}
            >
                <TextField
                    label="Joining Date"
                    type="date"
                    name="JoiningDate"
                    value={formData.JoiningDate}
                    onChange={(e) => setFormData((prev) => ({ ...prev, JoiningDate: e.target.value }))}
                    InputLabelProps={{
                        shrink: true,
                    }}
                    sx={{ mt: 2 }}
                    fullWidth
                />

                <TextField
                    label="Leaving Date"
                    type="date"
                    name="LeavingDate"
                    value={formData.LeavingDate}
                    onChange={(e) => setFormData((prev) => ({ ...prev, LeavingDate: e.target.value }))}
                    InputLabelProps={{
                        shrink: true,
                    }}
                    sx={{ mt: 2 }}
                    fullWidth
                />
            </CustomDialogForm>

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Delete"
                message={`Are you sure you want to delete the user: ${selectedUser?.FullName}?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />
        </Box>
    );
};
export default UserManagement;