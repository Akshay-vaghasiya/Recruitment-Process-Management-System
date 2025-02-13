import TableComponent from "../components/TableComponent";
import EditIcon from "@mui/icons-material/Edit";
import DocumentScannerIcon from '@mui/icons-material/DocumentScanner';
import { useEffect, useState } from "react";
import CustomDialogForm from "../components/CustomDialogForm";
import candidateService from "../services/candidateService";
import AuthHeader from "../helper/AuthHeader";
import { useCandidateContext } from "../contexts/CandidateContext";
import { useNavigate } from "react-router-dom";
import { fireToast } from "../components/fireToast";
import { useAuth } from "../contexts/AuthContext";


const Documents = ({ documents }) => {


    const [formData, setFormData] = useState({
        FkStatusId: ""
    });
    const [isFormOpen, setIsFormOpen] = useState(false);
    const { updateDocumentStatus } = candidateService;
    const headers = AuthHeader();
    const {getAllCandidates} = useCandidateContext();
    const {logout} = useAuth();
    const navigate = useNavigate();

    const handleEdit = (document) => {
        setFormData(document);
        setIsFormOpen(true);
    }


    const handleView = (document1) => {
        var anchor = document.createElement('a');
        anchor.href = document1?.DocumentUrl;
        anchor.target = '_blank';
        document.body.appendChild(anchor);
        anchor.click();
    }

    const handleSubmit = async () => {

        try {
            const data = await updateDocumentStatus(formData?.PkDocumentId, formData?.FkStatusId, headers);
            if(data !== null) {

                documents = documents?.map((document) => {
                    if(document.PkDocumentId === formData?.PkDocumentId) {
                        if(formData?.FkStatusId === 2) {
                            document.status = "PENDING"
                        } 
                        else if (formData?.FkStatusId === 3) {
                            document.status = "VERIFIED"
                        }
                        else if (formData?.FkStatusId === 4) {
                            document.status = "REJECTED"
                        }
                        
                        document.FkStatusId = formData?.FkStatusId;
                        return document;
                    }

                })
                await getAllCandidates(navigate);
                fireToast("successfully status updated", "success");
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

    const columns = ["ID", "Document type", "Status"];
    const datacolumns = ["PkDocumentId", "type", "status"];

    const actions = [
        {
            label: "Update Status",
            color: "primary",
            handler: handleEdit,
            icon: <EditIcon />,
        },
        {
            label: "view document",
            color: "secondary",
            handler: handleView,
            icon: <DocumentScannerIcon />,
        }
    ];

    const formFields = [
        {
            label: "Status", name: "FkStatusId", type: "select", options: [
                { label: "PENDING", value: 2 },
                { label: "VERIFIED", value: 3 },
                { label: "REJECTED", value: 4 },
            ],
            isMultiple: false, required: true
        }
    ]


    return (
        <>
            <TableComponent
                columns={columns}
                data={documents}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={"Update Document status"}
                submitButtonText={"Update"}
                fields={formFields}
            >
            </CustomDialogForm>
        </>
    )
}

export default Documents;