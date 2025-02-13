import { useEffect, useState } from "react";
import { useUserContext } from "../contexts/UserContext";
import { Button, Rating } from "@mui/material";
import CustomDialogForm from "../components/CustomDialogForm";
import AddIcon from "@mui/icons-material/Add";
import interviewService from "../services/interviewService";
import { useAuth } from "../contexts/AuthContext";
import { fireToast } from "../components/fireToast";
import TableComponent from "../components/TableComponent";
import AuthHeader from "../helper/AuthHeader";
import ConfirmationDialog from "../components/ConfirmationDialog";
import DeleteIcon from "@mui/icons-material/Delete";
import FeedbackIcon from '@mui/icons-material/Feedback';


const InterviewPanel = ({ interview }) => {

    const { users, getAllUsers } = useUserContext();
    const [interviewerOption, setInterviewerOption] = useState([]);
    const [formData, setFormData] = useState({});
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isFormOpen1, setIsFormOpen1] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [panel, setPanel] = useState([]);
    const { addInterviewPanel, deleteInterviewPanel, getInterviewPanelByInterview, 
        getInterviewFeedbackByUserAndInterview, addInterviewFeedback, updateInterviewFeedback } = interviewService;
    const { logout } = useAuth();
    const headers = AuthHeader();
    const [loading, setLoading] = useState(false);
    const [feedback, setFeedback] = useState({});
    const [updating, setUpdating] = useState(false);

    useEffect(() => {

        if (!users.length) {
            getAllUsers(navigate);
        } else {
            let users1 = users?.filter((user) => {

                let x = user?.UserRoles?.filter((role) => {

                    if (interview?.round === "HR") {
                        if (role.FkRole?.Name === "HR") {
                            return true;
                        }
                    } else {
                        if (role.FkRole?.Name === "INTERVIEWER") {
                            return true;
                        }
                    }

                })

                if (x.length > 0) return true
            })
            const temp = users1?.map((user) => {
                return { value: user.PkUserId, label: user.FullName }
            })
            setInterviewerOption(temp);
        }

    }, [users])

    useEffect(() => {


        const fetchPanels = async () => {
            try {
                const data = await getInterviewPanelByInterview(interview.PkInterviewId, headers);

                let temp = data?.map((panel1) => {
                    panel1.interviewerName = panel1.FkInterviewer.FullName;
                    return panel1;
                })
                setPanel(temp);

            } catch (error) {
                if (error?.response?.status === 401 || error?.response?.status === 403) {
                    logout();
                    navigate("/");
                    fireToast("Unauthorized access", "error");
                }
                fireToast(error?.response?.data, "error");
            }
        }

        fetchPanels();
    }, [loading])

    const handleAdd = () => {

        setFormData({
            FkInterviewerId: "",
        })
        setIsFormOpen(true);
    }

    const handleDelete = (panel) => {
        setFormData(panel);
        setIsConfirmOpen(true);
    }

    const handleFeedback = async (panel) => {
        try {
            const data = await getInterviewFeedbackByUserAndInterview(panel?.FkInterviewerId, panel?.FkInterviewId, headers);

            if (data != null && data != "") {
                setFeedback(data);
                setUpdating(true);
            } else {
                setFeedback({
                    FkInterviewId : panel?.FkInterviewId,
                    FkInterviewerId: panel?.FkInterviewerId,
                    Rating: 0,
                    Comments: ""
                })
                setUpdating(false);
            }
            setIsFormOpen1(true);

        } catch (error) {
            if (error?.response?.status === 401 || error?.response?.status === 403) {
                logout();
                navigate("/");
                fireToast("Unauthorized access", "error");
            }
            fireToast(error?.response?.data, "error");
        }
    }

    const handleSubmit = async () => {

        try {

            const data = await addInterviewPanel(formData.FkInterviewerId, interview?.PkInterviewId, headers);
            if (data != null) {
                fireToast("successfully interviewer added", "success");
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
            
            if( updating === true) {
                const obj = {};
                obj.Comments = feedback.Comments;
                obj.Rating = feedback.Rating;
                const data = await updateInterviewFeedback(feedback.PkInterviewFeedbackId, obj, headers);
                if (data != null) {
                    fireToast("successfully feedback updated", "success");
                    setIsFormOpen1(false);
                    setLoading(!loading);
                }
            } else {
                const data = await addInterviewFeedback(feedback, headers);
                if (data != null) {
                    fireToast("successfully feedback added", "success");
                    setIsFormOpen1(false);
                    setLoading(!loading);
                }
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

    const handleConfirmDelete = async () => {
        try {

            const data = await deleteInterviewPanel(formData.PkInterviewPanelId, headers);
            if (data != null) {
                fireToast("successfully interviewer deleted", "success");
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

    const columns = ["ID", "Interviewer Name", "Interviewer Id"];
    const datacolumns = ["PkInterviewPanelId", "interviewerName", "FkInterviewerId"];


    const formFields = [
        {
            label: "Interviewer", name: "FkInterviewerId", type: "select", options: interviewerOption
            , isMultiple: false, required: true
        },
    ];

    const formFields1 = [
        {
            label: "Comments", name: "Comments", type: "textarea", required: true
        }
    ]

    const actions = [
        {
            label: "Interview Feedback",
            color: "primary",
            handler: handleFeedback,
            icon: <FeedbackIcon />
        },
        {
            label: "Delete Interview",
            color: "error",
            handler: handleDelete,
            icon: <DeleteIcon />,
        }
    ];

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                startIcon={<AddIcon />}
                onClick={handleAdd}
                sx={{ mb: 2 }}
            >
                Add Panel Interviewer
            </Button>

            <TableComponent
                columns={columns}
                data={panel?.length > 0 ? panel : []}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={"Add Interviewer"}
                submitButtonText={"Add"}
                fields={formFields}
            >
            </CustomDialogForm>

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Delete"
                message={`Are you sure you want to delete : ${formData?.interviewerName} interviewer?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />

            <CustomDialogForm
                open={isFormOpen1}
                onClose={setIsFormOpen1}
                formData={feedback}
                setFormData={setFeedback}
                onSubmit={handleSubmit1}
                title={"Interview Feedback"}
                submitButtonText={updating ? "Update" : "Add"}
                fields={formFields1}
            >
                <Rating name="half-rating" defaultValue={0}
                precision={1}
                value={feedback.Rating}
                onChange={e => {
                    setFeedback((prev) => ({...prev, Rating : parseInt(e.target.value)}))
                }}  />
            </CustomDialogForm>
        </>
    )

}

export default InterviewPanel;