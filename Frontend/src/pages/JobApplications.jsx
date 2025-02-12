import { useEffect, useState } from "react";
import { useJobPositionContext } from "../contexts/JobPositionContext";
import { useNavigate, useParams } from "react-router-dom";
import TableComponent from "../components/TableComponent";
import EditIcon from "@mui/icons-material/Edit";
import CustomDialogForm from "../components/CustomDialogForm";
import jobApplicationService from "../services/jobApplicationService";
import AuthHeader from "../helper/AuthHeader";
import { fireToast } from "../components/fireToast";
import { useAuth } from "../contexts/AuthContext";

const JobApplications = () => {

    const { jobPositions, getAllJob } = useJobPositionContext();
    const [applications, setApplications] = useState();
    const { jobPositionId } = useParams()
    const navigate = useNavigate();
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [formData, setFormData] = useState({
        FkStatusId: ""
    });
    const { updateApplicationStatus } = jobApplicationService;
    const headers = AuthHeader();
    const { logout } = useAuth();


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

    const handleSubmit = async () => {

        console.log(formData);
        
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
            fireToast(error?.response?.status, "error");
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

        </>
    )

}

export default JobApplications;