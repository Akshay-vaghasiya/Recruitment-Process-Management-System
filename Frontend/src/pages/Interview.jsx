import { Button, colors } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useEffect, useState } from "react";
import CustomDialogForm from "../components/CustomDialogForm";
import interviewService from "../services/interviewService";
import { useAuth } from "../contexts/AuthContext";
import AuthHeader from "../helper/AuthHeader";
import TableComponent from "../components/TableComponent";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { fireToast } from "../components/fireToast";
import ConfirmationDialog from "../components/ConfirmationDialog";
import PeopleIcon from '@mui/icons-material/People';
import InterviewPanel from "./InterviewPanel";

const Interview = ({ application }) => {

    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isFormOpen1, setIsFormOpen1] = useState(false);
    const [isFormOpen2, setIsFormOpen2] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [formData, setFormData] = useState({});
    const [interviews, setInterviews] = useState([]);
    const {logout} = useAuth();
    const headers = AuthHeader();
    const [loading, setLoading] = useState(false);
    const { getInterviewByCandidateAndPosition, addInterview, deleteInterview, updateInterviewStatus } = interviewService;

    useEffect(() => {

        const fetchinterview = async () => {

            try {

                let data = await getInterviewByCandidateAndPosition(application?.FkCandidateId, application?.FkJobPositionId, headers);
                
                if(data != null) {

                    data = data?.map((interview) => {
                        interview.status = interview.FkStatus.Name;
                        interview.round = interview.FkInterviewRound.Name;

                        return interview;
                    })

                    setInterviews(data);
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

        fetchinterview();

    }, [loading])

    const handleAdd = () => {

        setFormData({
            FkCandidateId: application?.FkCandidateId,
            FkJobPositionId: application?.FkJobPositionId,
            FkInterviewRoundId: "",
            RoundNumber: 1,
            FkStatusId: ""
        })
        setIsFormOpen(true);
    }


    const handleSubmit = async () => {

        try {

            const data = await addInterview(formData, headers);
            if(data != null) {
                fireToast("successfully interview added", "success");
                setIsFormOpen(false);
                setLoading(!loading);
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

    const handleSubmit1 = async () => {
        try {

            const data = await updateInterviewStatus(formData.PkInterviewId, formData.FkStatusId, headers);
            if(data != null) {
                fireToast("successfully interview status updated", "success");
                setIsFormOpen1(false);
                setLoading(!loading);
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


    const handleEdit = (interview) => {
        setFormData(interview);
        setIsFormOpen1(true);
    }

    const handleDelete = (interview) => {
        setFormData(interview);
        setIsConfirmOpen(true);
    }

    const handleInterviewPanel = (interview) => {
        setLoading(!loading);
        setFormData(interview);
        setIsFormOpen2(true);
    }

    const handleConfirmDelete = async () => {

        try {

            const data = await deleteInterview(formData.PkInterviewId, headers);
            if(data != null) {
                fireToast("successfully interview deleted", "success");
                setIsConfirmOpen(false);
                setLoading(!loading);
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

    const columns = ["ID", "Round Name", "Round no.", "Status", "CreatedAt"];
    const datacolumns = ["PkInterviewId", "round", "RoundNumber", "status", "CreatedAt"];

    const actions = [
        {
            label: "Update Status",
            color: "primary",
            handler: handleEdit,
            icon: <EditIcon />,
        },
        {
            label: "Interview Panel",
            color: "secondary",
            handler: handleInterviewPanel,
            icon: <PeopleIcon />
        },
        {
            label: "Delete Interview",
            color: "error",
            handler: handleDelete,
            icon: <DeleteIcon />,
        }
    ];


    const formFields = [
        { label: "Round number", name: "RoundNumber", type: "number", required: true },
        {
            label: "Interview Round", name: "FkInterviewRoundId", type: "select", options: [
                { value: 2, label: "HR" },
                { value: 3, label: "TECHNICAL 1" },
                { value: 4, label: "TECHNICAL 2" },
                { value: 5, label: "TECHNICAL 3" },
            ], isMultiple: false, required: true
        },
        {
            label: "Interview Status", name: "FkStatusId", type: "select", options: [
                { value: 1, label: "SCHEDULED" },
                { value: 2, label: "COMPLETED" },
                { value: 4, label: "CANCELLED" },
            ], isMultiple: false, required: true
        },
    ];

    const formFields1 = [
        {
            label: "Interview Status", name: "FkStatusId", type: "select", options: [
                { value: 1, label: "SCHEDULED" },
                { value: 2, label: "COMPLETED" },
                { value: 4, label: "CANCELLED" },
            ], isMultiple: false, required: true
        },
    ]

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                startIcon={<AddIcon />}
                onClick={handleAdd}
                sx={{mb : 2}}
            >
                Add Interview
            </Button>

            <TableComponent
                columns={columns}
                data={interviews?.length > 0 ? interviews : []}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={"Add Interview"}
                submitButtonText={"Create"}
                fields={formFields}
            >
            </CustomDialogForm>

            <CustomDialogForm
                open={isFormOpen1}
                onClose={setIsFormOpen1}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit1}
                title={"Update Interview status"}
                submitButtonText={"Update"}
                fields={formFields1}
            >
            </CustomDialogForm>

            <CustomDialogForm
                open={isFormOpen2}
                onClose={setIsFormOpen2}
                formData={null}
                setFormData={null}
                onSubmit={null}
                title={"Interview Panel"}
                submitButtonText={""}
                fields={[]}
                size="md"
            >

                <InterviewPanel 
                    interview = {formData}
                />
            </CustomDialogForm>

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Delete"
                message={`Are you sure you want to delete : ${formData?.round} interview round?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />
        </>
    )
}

export default Interview;