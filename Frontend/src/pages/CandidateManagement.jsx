import React, { useEffect, useState } from "react";
import TableComponent from "../components/TableComponent";
import CustomDialogForm from "../components/CustomDialogForm";
import ConfirmationDialog from "../components/ConfirmationDialog";
import { Button, Box, Typography, CircularProgress, TextField, IconButton, Switch, Input, Alert } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useNavigate } from "react-router-dom";
import { useCandidateContext } from "../contexts/CandidateContext";
import DocumentScannerIcon from '@mui/icons-material/DocumentScanner';
import Documents from "./Documents";
import candidateService from "../services/candidateService";
import AuthHeader from "../helper/AuthHeader";
import { fireToast } from "../components/fireToast";
import { useAuth } from "../contexts/AuthContext";
import UploadIcon from '@mui/icons-material/Upload';
import DownloadIcon from '@mui/icons-material/Download';
import PostAddIcon from '@mui/icons-material/PostAdd';

const CandidateManagement = () => {

    const { searchTerm, isLoading, isError, candidates, filteredCandidates, getAllCandidates, addCandidate, searchCandidate, editCandidate, removeCandidate } = useCandidateContext();
    const { logout } = useAuth();
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isFormOpen1, setIsFormOpen1] = useState(false);
    const [isFormOpen2, setIsFormOpen2] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [selectedCandidate, setSelectedCandidate] = useState(null);
    const [formData, setFormData] = useState({});
    const navigate = useNavigate();
    const [file, setFile] = useState(null);
    const [documents, setDocuments] = useState([]);
    const [file1, setFile1] = useState(null);
    const [isUploading, setIsUploading] = useState(false);
    const { bulkCandidateUpload } = candidateService;
    const headers = AuthHeader();
    let headers1 = headers;
    headers1['Content-Type'] = "multipart/form-data";

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

        if (formData.YearsOfExperience < 0) {
            fireToast("year of experience can not be nagetive", "error");
            return;
        }
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

    const handleDocument = (candidate) => {
        setSelectedCandidate(candidate);

        setDocuments(candidate?.Documents?.map((document) => {
            document.type = document.FkDocumentType.Name;
            document.status = document.FkStatus.Name;

            return document;
        }))

        setIsFormOpen1(true);
    }

    const handleAddBulk = () => {
        setIsFormOpen2(true);
    }

    const handleFileChange1 = (e) => {
        setFile1(e.target.files[0]);
        setUploadSuccess(false);
    };

    const handleUpload = async () => {
        if (!file1) {
            fireToast("Please select a file first.", "error");
            return;
        }

        const formData = new FormData();
        formData.append("document", file1);

        setIsUploading(true);

        try {
            await bulkCandidateUpload(formData, headers1);
            fireToast("file successfully uploaded", "success")
            setFile1(null);
            setIsFormOpen2(false);
            await getAllCandidates(navigate);
        } catch (error) {
            if (error?.response?.status === 401 || error?.response?.status === 403) {
                logout();
                navigate("/");
                fireToast("Unauthorized access", "error");
            }
            fireToast(error?.response?.data, "error");
        } finally {
            setIsUploading(false);
        }
    };

    const handleDownload = () => {
        const filePath = `/Sample_Data.xlsx`;

        const link = document.createElement("a");
        link.href = filePath;
        link.setAttribute("download", "Sample_Data.xlsx");
        document.body.appendChild(link);
        link.click();
        link.remove();
    };


    const columns = ["ID", "FullName", "Email", "Phone", "Skills(Years of experience)", "YearsOfExperience", "CreatedAt"];
    const datacolumns = ["PkCandidateId", "FullName", "Email", "Phone", "skills", "YearsOfExperience", "CreatedAt"];


    const formFields = [
        { label: "FullName", name: "FullName", type: "text", required: true },
        { label: "Email", name: "Email", type: "email", required: true },
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
        },
        {
            label: "Documents",
            color: "secondary",
            handler: handleDocument,
            icon: <DocumentScannerIcon />
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
                    label="Search Candidate"
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

                <Button
                    variant="contained"
                    color="secondary"
                    onClick={handleAddBulk}
                    startIcon={<PostAddIcon/>}
                >
                    Bulk Candidate Upload
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

            <CustomDialogForm
                open={isFormOpen1}
                onClose={setIsFormOpen1}
                formData={null}
                setFormData={null}
                onSubmit={null}
                title={"Candidate Document"}
                submitButtonText={""}
                fields={[]}
            >
                <Documents
                    documents={documents}
                />
            </CustomDialogForm>

            <CustomDialogForm
                open={isFormOpen2}
                onClose={setIsFormOpen2}
                formData={null}
                setFormData={null}
                onSubmit={null}
                title={"Bulk Candidate Upload"}
                submitButtonText={""}
                fields={[]}
            >

                <Box
                    display="flex"
                    flexDirection="column"
                    alignItems="center"
                    justifyContent="center"
                    sx={{ mt: 4, border: "2px", borderColor: "black" }}
                >
                    
                    <Input
                        type="file"
                        accept=".xlsx, .xls"
                        onChange={handleFileChange1}
                        sx={{ mb: 2 }}
                    />

                    <div style={{ display: 'flex', flexDirection: 'row', gap: '1rem' }}>

                        <Button
                            variant="contained"
                            color="primary"
                            onClick={handleUpload}
                            disabled={isUploading}
                            startIcon={<UploadIcon />}
                        >
                            {isUploading ? (
                                <CircularProgress size={24} sx={{ color: "white" }} />
                            ) : (
                                "Upload"
                            )}
                        </Button>

                        <Button
                            variant="contained"
                            color="primary"
                            onClick={handleDownload}
                            startIcon={<DownloadIcon />}
                        >
                            Download Sample File
                        </Button>
                    </div>

                </Box>
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