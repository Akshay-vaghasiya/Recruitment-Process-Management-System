import AddIcon from "@mui/icons-material/Add";
import { Button, CircularProgress, Input, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import TableComponent from "../components/TableComponent";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import DocumentScannerIcon from '@mui/icons-material/DocumentScanner';
import candidateService from "../services/candidateService";
import AuthHeader from "../helper/AuthHeader";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";
import { fireToast } from "../components/fireToast";
import CustomDialogForm from "../components/CustomDialogForm";
import ConfirmationDialog from "../components/ConfirmationDialog";


const CandidateDocuments = () => {

    const [documents1, setDocuments1] = useState([]);
    const [document2, setDocument2] = useState({
        FkDocumentTypeId: "",
    });
    const [isUpdate, setIsUpdate] = useState(false);
    const [typeOption, setTypeOption] = useState([]);
    const { getDocumentType, addDocument, updateDocument, deleteDocument, getCandidateDocument } = candidateService;
    const headers = AuthHeader();
    const navigate = useNavigate();
    const { logout, candidate } = useAuth();
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [file, setFile] = useState(null);
    const [isLoading, setIsLoading] = useState(false);


    const fetchDocuments = async () => {
        try {
            setIsLoading(true);
            const data = await getCandidateDocument(candidate.PkCandidateId, headers);
            if (data != null) {
                setDocuments1(
                    data?.map((document) => {
                        document.status = document.FkStatus.Name;
                        document.type = document.FkDocumentType.Name;
                        return document;
                    })
                )
            }
        } catch (error) {
            if (error?.response?.status === 401 || error?.response?.status === 403) {
                logout();
                navigate("/");
                fireToast("Unauthorized access", "error");
            }
            fireToast(error?.response?.data, "error");
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        fetchDocuments();

        const fetchDocumentType = async () => {
            try {

                const data = await getDocumentType(headers);
                if (data != null) {
                    setTypeOption(
                        data?.map((type) => {
                            return { label: type.Name, value: type.PkDocumentTypeId };
                        })
                    )
                }
            } catch (error) {
                if (error?.response?.status === 401 || error?.response?.status === 403) {
                    logout();
                    navigate("/");
                    fireToast("Unauthorized access", "error");
                }
                fireToast(error?.response?.data, "error");
            }
        }

        fetchDocumentType();

    }, [])

    const handleAdd = () => {
        setIsUpdate(false);
        setDocument2({
            FkDocumentTypeId: "",
        })
        setIsFormOpen(true);
    }

    const handleEdit = (document1) => {
        setIsUpdate(true)
        setDocument2(document1);
        setIsFormOpen(true);
    }

    const handleDelete = (document1) => {
        setDocument2(document1)
        setIsConfirmOpen(true)
    }

    const handleConfirmDelete = async () => {
        try {
            await deleteDocument(document2.PkDocumentId, headers);
            fireToast("Successfully document deleted", "success");
            await fetchDocuments();
            setIsConfirmOpen(false);
        } catch (error) {
            if (error?.response?.status === 401 || error?.response?.status === 403) {
                logout();
                navigate("/");
                fireToast("Unauthorized access", "error");
            }
            fireToast(error?.response?.data, "error");
        }
    }

    const handleView = (document1) => {

        var anchor = document.createElement('a');
        anchor.href = document1?.DocumentUrl;
        anchor.target = '_blank';
        document.body.appendChild(anchor);
        anchor.click();
    }

    const handleSubmit = async () => {

        if (!file) {
            fireToast("Please select a file first.", "error");
            return;
        }

        try {
            const formData = new FormData();
            formData.append("document", file);

            let headers1 = headers;
            headers1['Content-Type'] = "multipart/form-data";

            if (isUpdate) {
                await updateDocument(document2.PkDocumentId, formData, headers1);
                fireToast("Successfully document updated", "success");
                await fetchDocuments();
                setIsFormOpen(false);
            } else {
                await addDocument(candidate.PkCandidateId, document2.FkDocumentTypeId, formData, headers1);
                fireToast("Successfully document added", "success");
                await fetchDocuments();
                setIsFormOpen(false);
            }
        } catch (error) {
            if (error?.response?.status === 401 || error?.response?.status === 403) {
                logout();
                navigate("/");
                fireToast("Unauthorized access", "error");
            }
            fireToast(error?.response?.data, "error");
        }

    }

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const columns = ["ID", "Document Type", "Status"];
    const datacolumns = ["PkDocumentId", "type", "status"];

    const actions = [
        {
            label: "view document",
            color: "secondary",
            handler: handleView,
            icon: <DocumentScannerIcon />,
        },
        {
            label: "Update Document",
            color: "primary",
            handler: handleEdit,
            icon: <EditIcon />,
        },
        {
            label: "Delete Document",
            color: "error",
            handler: handleDelete,
            icon: <DeleteIcon />,
        }
    ];

    const formFields = [
        {
            label: "Document Type", name: "FkDocumentTypeId", type: "select", options: typeOption,
            isMultiple: false, required: true, disabled: isUpdate ? true : false
        },
    ];

    if (isLoading) {
        return <CircularProgress />;
    }

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                startIcon={<AddIcon />}
                onClick={handleAdd}
                sx={{ mb: 2 }}
            >
                Add Document
            </Button>

            <TableComponent
                columns={columns}
                data={documents1?.length > 0 ? documents1 : []}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={document2}
                setFormData={setDocument2}
                onSubmit={handleSubmit}
                title={isUpdate ? "Edit Document" : "Add Document"}
                submitButtonText={isUpdate ? "Update" : "Add"}
                fields={formFields}
            >
                <Typography variant="h6" gutterBottom>
                    Upload Document
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
                message={`Are you sure you want to delete ${document?.type} document ?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />
        </>
    )
}

export default CandidateDocuments;