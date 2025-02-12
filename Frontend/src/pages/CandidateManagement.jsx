import React, { useEffect, useState } from "react";
import TableComponent from "../components/TableComponent";
import CustomDialogForm from "../components/CustomDialogForm";
import ConfirmationDialog from "../components/ConfirmationDialog";
import { Button, Box, Typography, CircularProgress, TextField, IconButton, Switch, Input } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useNavigate } from "react-router-dom";
import { useCandidateContext } from "../contexts/CandidateContext";

const CandidateManagement = () => {

    const { searchTerm, isLoading, isError, candidates, filteredCandidates, getAllCandidates, addCandidate, searchCandidate, editCandidate, removeCandidate } = useCandidateContext();

    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [selectedCandidate, setSelectedCandidate] = useState(null);
    const [formData, setFormData] = useState({});
    const navigate = useNavigate();
    const [file, setFile] = useState(null);

    useEffect(() => {
            getAllCandidates(navigate);        
    }, []);


    const handleAdd = () => {
        setFormData({
            FullName: "",
            Email: "",
            Phone: "",
            Password: "",
            YearsOfExperience: 0
        });
        setSelectedCandidate(null);
        setIsFormOpen(true);
    };

    const handleEdit = (Candidate) => {
        Candidate.Password = "";
        setFormData(Candidate);
        setSelectedCandidate(Candidate);
        setIsFormOpen(true);
    };

    const handleDelete = (Candidate) => {
        setSelectedCandidate(Candidate);
        setIsConfirmOpen(true);
    };

    const handleSubmit = () => {
        if (selectedCandidate) {
            editCandidate(selectedCandidate.PkCandidateId, formData, file, navigate);
        } else {
            addCandidate(formData, file, navigate);
        }
        setIsFormOpen(false);
    };

    const handleConfirmDelete = () => {
        removeCandidate(selectedCandidate.PkCandidateId, navigate);
        setIsConfirmOpen(false);
    };

    const handleSearch = (event) => {
        searchCandidate(event.target.value);
    };

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const columns = ["ID", "FullName", "Email", "Phone", "ResumeUrl", "YearsOfExperience", "CreatedAt"];
    const datacolumns = ["PkCandidateId","FullName", "Email", "Phone", "ResumeUrl", "YearsOfExperience", "CreatedAt"];


    const formFields = [
        { label: "FullName", name: "FullName", type: "text", required: true },
        { label: "Email", name: "Email", type: "text", required: true },
        { label: "Phone", name: "Phone", type: "text", required: true },
        { label: "Password", name: "Password", type: "text", required: true },
        { label: "Year Of Experience", name: "YearsOfExperience", type: "number", required: true },
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
                Error loading Candidates. Please try again later.
            </Typography>
        );
    }

    return (
        <Box>
            <Typography variant="h4" align="center" gutterBottom>
                Candidate Management
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
                    Add Candidate
                </Button>
            </Box>

            <TableComponent
                columns={columns}
                data={filteredCandidates}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={selectedCandidate ? "Edit Candidate" : "Add Candidate"}
                submitButtonText={selectedCandidate ? "Update" : "Create"}
                fields={formFields}
            >
                <Typography variant="h6" gutterBottom>
                    Upload Resume
                </Typography>
                <Input
                    type="file"
                    accept=".docx, .pdf"
                    onChange={handleFileChange}
                    sx={{ mb: 2 }}
                />

            </CustomDialogForm>

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Delete"
                message={`Are you sure you want to delete the Candidate: ${selectedCandidate?.FullName}?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />
        </Box>
    );
};
export default CandidateManagement;