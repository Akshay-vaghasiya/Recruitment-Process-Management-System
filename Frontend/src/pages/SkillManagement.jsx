import React, { useEffect, useState } from "react";
import TableComponent from "../components/TableComponent";
import CustomDialogForm from "../components/CustomDialogForm";
import ConfirmationDialog from "../components/ConfirmationDialog";
import { Button, Box, Typography, CircularProgress, TextField, IconButton, Switch, Input } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useNavigate } from "react-router-dom";
import { useSkillContext } from "../contexts/SkillContext";

const SkillManagement = () => {

    const { searchTerm, isLoading, isError, skills, filteredSkills, addSkill1, getAllSkills, searchSkill, editSkill, removeSkill } = useSkillContext();

    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [selectedSkill, setSelectedSkill] = useState(null);
    const [formData, setFormData] = useState({});
    const navigate = useNavigate();

    useEffect(() => {

        if (!skills.length) {
            getAllSkills(navigate);
        }

    }, [skills]);


    const handleAdd = () => {
        setFormData({
            Name : ""
        });
        setSelectedSkill(null);
        setIsFormOpen(true);
    };

    const handleEdit = (skill) => {
        setFormData(skill);
        setSelectedSkill(skill);
        setIsFormOpen(true);
    };

    const handleDelete = (skill) => {
        setSelectedSkill(skill);
        setIsConfirmOpen(true);
    };

    const handleSubmit = () => {
        if (selectedSkill) {
            editSkill(selectedSkill.PkSkillId, formData, navigate);
        } else {
            addSkill1(formData, navigate);
        }
        setIsFormOpen(false);
    };

    const handleConfirmDelete = () => {
        removeSkill(selectedSkill.PkSkillId, navigate);
        setIsConfirmOpen(false);
    };

    const handleSearch = (event) => {
        searchSkill(event.target.value);
    };

    const columns = ["PkSkillId","Name"];
    const datacolumns = ["PkSkillId","Name"];


    const formFields = [
        { label: "Name", name: "Name", type: "text", required: true }
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
                Error loading Skills. Please try again later.
            </Typography>
        );
    }

    return (
        <Box>
            <Typography variant="h4" align="center" gutterBottom>
                Skill Management
            </Typography>

            <Box sx={{ display: "flex", justifyContent: "flex-start", marginBottom: "1rem", gap: "1.5rem", marginTop: "1.5rem" }}>
                <TextField
                    label="Search Skill"
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
                    Add Skill
                </Button>
            </Box>

            <TableComponent
                columns={columns}
                data={filteredSkills}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={selectedSkill ? "Edit Skill" : "Add Skill"}
                submitButtonText={selectedSkill ? "Update" : "Create"}
                fields={formFields}
            >
            </CustomDialogForm>

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Delete"
                message={`Are you sure you want to delete the Skill: ${selectedSkill?.Name}?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />
        </Box>
    );
};
export default SkillManagement;