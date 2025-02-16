import React, { useEffect, useState } from "react";
import TableComponent from "../components/TableComponent";
import ConfirmationDialog from "../components/ConfirmationDialog";
import { Box, Typography, CircularProgress} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useJobPositionContext } from "../contexts/JobPositionContext";
import { useAuth } from "../contexts/AuthContext";
import jobApplicationService from "../services/jobApplicationService";
import { fireToast } from "../components/fireToast";
import AuthHeader from "../helper/AuthHeader";

const JobPositionCandidate = () => {

    const { isLoading, isError, jobPositions, getAllJob } = useJobPositionContext();
    const navigate = useNavigate();
    const [selectedJob, setSelectedJob] = useState();
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const { candidate, logout } = useAuth();
    const { createJobApplication } = jobApplicationService;
    const headers = AuthHeader();
    const [openJobPositions, setOpenJobPositions] = useState([]);

    useEffect(() => {
        if (jobPositions?.length === 0) {
            getAllJob(navigate)
        } else {

            let openPositions = jobPositions?.filter((job) => {
                if (job.FkStatus.Name === "OPEN") {
                    let temp = 0;
                    job?.JobApplications?.map((application) => {
                        if (application?.FkCandidateId === candidate.PkCandidateId) {
                            temp = 1;
                        }
                    })

                    if (temp === 0) {
                        return true;
                    }
                }
            })

            setOpenJobPositions(openPositions);

        }
    }, [jobPositions])

    const handleApply = (job) => {
        setSelectedJob(job);
        setIsConfirmOpen(true);
    }

    const handleApplyJob = async () => {
        try {
            const data = await createJobApplication(candidate?.PkCandidateId, selectedJob?.PkJobPositionId, headers);
            if (data != null) {
                fireToast("successfully applied", "success");
                setIsConfirmOpen(false);
                await getAllJob(navigate);
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


    const columns = ["ID", "Title", "Description", "requiredSkill", "preferredSkill"];
    const datacolumns = ["PkJobPositionId", "Title", "Description", "requiredSkill", "preferredSkill"];

    const actions = [
        {
            label: "Apply",
            color: "primary",
            handler: handleApply,
        }
    ];



    if (isLoading) {
        return <CircularProgress />;
    }

    if (isError) {
        return (
            <Typography color="error">
                Error loading Jobs. Please try again later.
            </Typography>
        );
    }

    return (
        <Box>
            <Typography variant="h4" align="center" gutterBottom>
                Job Positions
            </Typography>

            <TableComponent
                columns={columns}
                data={openJobPositions ? openJobPositions : []}
                datacolumns={datacolumns}
                actions={actions}
            />

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Apply For Job"
                message={`Are you sure you want to apply the Job: ${selectedJob?.Title}?`}
                onConfirm={handleApplyJob}
                onCancel={setIsConfirmOpen}
            />
        </Box>
    );
};
export default JobPositionCandidate;