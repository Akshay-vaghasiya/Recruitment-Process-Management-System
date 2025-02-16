import { useEffect, useState } from "react";
import { useJobPositionContext } from "../contexts/JobPositionContext";
import { useNavigate } from "react-router-dom";
import TableComponent from "../components/TableComponent";
import EditIcon from "@mui/icons-material/Edit";
import CustomDialogForm from "../components/CustomDialogForm";
import jobApplicationService from "../services/jobApplicationService";
import AuthHeader from "../helper/AuthHeader";
import { fireToast } from "../components/fireToast";
import { useAuth } from "../contexts/AuthContext";
import PreviewIcon from '@mui/icons-material/Preview';
import resumeReviewService from "../services/resumeReviewService";
import InterpreterModeIcon from '@mui/icons-material/InterpreterMode';
import Interview from "./Interview";
import DeleteIcon from "@mui/icons-material/Delete";
import ConfirmationDialog from "../components/ConfirmationDialog";

const JobApplications = ({ jobPositionId }) => {

    const { jobPositions, getAllJob } = useJobPositionContext();
    const [applications, setApplications] = useState();
    const navigate = useNavigate();
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isFormOpen1, setIsFormOpen1] = useState(false);
    const [isFormOpen2, setIsFormOpen2] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [formData, setFormData] = useState({
        FkStatusId: ""
    });
    const [formData1, setFormData1] = useState({
        Comments: ""
    });
    const [isUpdate, setIsUpdate] = useState(false);
    const { updateApplicationStatus, deleteJobAppliction } = jobApplicationService;
    const headers = AuthHeader();
    const { logout } = useAuth();
    const { updateReview, addReview } = resumeReviewService;
    const user = JSON.parse(localStorage.getItem('user'));

    useEffect(() => {

        if (!jobPositions.length) {
            getAllJob(navigate);
        } else {
            let temp = jobPositions?.filter((job1) => {
                if (job1.PkJobPositionId === parseInt(jobPositionId)) {
                    return true;
                }
            })

            let application1 = temp[0]?.JobApplications?.map((jobApp) => {

                jobApp.candidateName = jobApp?.FkCandidate?.FullName;
                jobApp.status = jobApp?.FkStatus?.Name;

                return jobApp;
            })

            setApplications(application1);
        }
    }, [jobPositions])

    const handleEdit = (application) => {
        setFormData(application);
        setIsFormOpen(true);
    }

    const handleDelete = (application) => {
        setFormData(application);
        setIsConfirmOpen(true);
    }

    const handleSubmit = async () => {
        try {
            const response = await updateApplicationStatus(formData.PkJobApplicationId, formData.FkStatusId, headers);
            if (response != null) {
                await getAllJob(navigate);
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

    const handleResumeScreen = (application) => {

        const review1 = application?.FkCandidate?.ResumeReviews?.filter((review) => {
            if (review?.FkJobPositionId === parseInt(jobPositionId)) {
                return true;
            }
        });

        if (review1.length > 0) {
            setIsUpdate(true);
            application.ResumeReviewId = review1[0].PkResumeReviewId;
            setFormData(application);
        }


        setFormData1((prev) => ({
            ...prev, Comments: review1[0]?.Comments
        }));
        setIsFormOpen1(true);
    }

    const handleInterview = (application) => {
        setFormData(application);
        setIsFormOpen2(true);
    }

    const handleSubmit1 = async () => {

        try {
            const obj = {
                FkCandidateId: formData.FkCandidateId,
                FkJobPositionId: formData.FkJobPositionId,
                Comments: formData1.Comments
            }

            if (isUpdate) {
                const response = await updateReview(user?.PkUserId, formData.ResumeReviewId, obj, headers);
                if (response !== null) {
                    await getAllJob(navigate);
                    fireToast("successfully review updated", "success");
                    setIsFormOpen1(false);
                }
            } else {
                const response = await addReview(user?.PkUserId, obj, headers);
                if (response !== null) {
                    await getAllJob(navigate);
                    fireToast("successfully review added", "success");
                    setIsFormOpen1(false);
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
            const response = await deleteJobAppliction(formData.PkJobApplicationId, headers);
            if (response !== null) {
                await getAllJob(navigate);
                fireToast("successfully application deleted", "success");
                setIsConfirmOpen(false);
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

    const columns = ["ID", "Candidate name", "Status", "CreatedAt"];
    const datacolumns = ["PkJobApplicationId", "candidateName", "status", "CreatedAt"];

    const actions = [
        {
            label: "Update Status",
            color: "primary",
            handler: handleEdit,
            icon: <EditIcon />,
        },
        {
            label: "Resume Screen",
            color: "secondary",
            handler: handleResumeScreen,
            icon: <PreviewIcon />,
        },
        {
            label: "Interviews",
            color: "secondary",
            handler: handleInterview,
            icon: <InterpreterModeIcon />,
        },
        {
            label: "Delete",
            color: "error",
            handler: handleDelete,
            icon: <DeleteIcon />,
        }
    ];

    const formFields = [
        {
            label: "Status", name: "FkStatusId", type: "select", options: [
                { label: "APPLIED", value: 2 },
                { label: "SCREENED", value: 3 },
                { label: "SHORTLISTED", value: 4 },
                { label: "INTERVIEW SCHEDULED", value: 5 },
                { label: "INTERVIEWED", value: 6 },
                { label: "HIRED", value: 7 },
                { label: "REJECTED", value: 8 },
            ],
            isMultiple: false, required: true
        }
    ]

    const formFields1 = [
        { label: "Comments", name: "Comments", type: "textarea", required: true },
    ]

    return (
        <>
            <TableComponent
                columns={columns}
                data={applications?.length > 0 ? applications : []}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={"Update application status"}
                submitButtonText={"Update"}
                fields={formFields}
            >
            </CustomDialogForm>

            <CustomDialogForm
                open={isFormOpen1}
                onClose={setIsFormOpen1}
                formData={formData1}
                setFormData={setFormData1}
                onSubmit={handleSubmit1}
                title={"Resume Screen"}
                submitButtonText={isUpdate ? "Update Review" : "Add Review"}
                fields={formFields1}
            >
                <a href={formData?.FkCandidate?.ResumeUrl} target="_blank">click here to show CV</a>
            </CustomDialogForm>

            <CustomDialogForm
                open={isFormOpen2}
                onClose={setIsFormOpen2}
                formData={null}
                setFormData={null}
                onSubmit={handleSubmit}
                title={"Candidate Interview"}
                submitButtonText={""}
                fields={[]}
                size="lg"
            >
                <Interview
                    application={formData}
                />
            </CustomDialogForm>

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Delete"
                message={`Are you sure you want to delete the applicaton of  ${formData?.FkCandidate?.FullName}?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />

        </>
    )

}

export default JobApplications;