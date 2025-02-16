import { Typography } from "@mui/material";
import { useJobPositionContext } from "../contexts/JobPositionContext";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";
import TableComponent from "../components/TableComponent";

const CandidateApplications = () => {

    const { appliedJobs, jobPositions, getAllJob } = useJobPositionContext();
    const {candidate} = useAuth();
    const [applications, setApplications] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        if (jobPositions?.length === 0) {
            getAllJob(navigate)
        } else {
            let appliedJobs1 = jobPositions?.filter((job) => {

                let temp = 0;
                job?.JobApplications?.map((application) => {
                    if (application?.FkCandidateId === candidate.PkCandidateId) {
                        temp = 1;
                    }
                })

                if (temp === 1) {
                    return true;
                }
            })

            setApplications(
                appliedJobs1?.map((job) => {
                    job.applicationId = "";
                    job.applicationStatus = "";

                    job.JobApplications?.map((application) =>{

                        if(application.FkCandidateId === candidate.PkCandidateId) {
                            job.applicationId = application.PkJobApplicationId;
                            job.applicationStatus = application.FkStatus.Name;
                        }
                    })
                    return job;
                })
            )
        }
    }, [jobPositions])

    console.log(appliedJobs);
    
    useEffect(() => {

        
    }, [])

    console.log(applications);
    const columns = ["Job ID", "Title", "Description", "requiredSkill", "preferredSkill", "Job Status", "Application ID", "Application Status"];
    const datacolumns = ["PkJobPositionId", "Title", "Description", "requiredSkill", "preferredSkill", "status", "applicationId", "applicationStatus"];


    return (
        <>
            <Typography variant="h4" align="center" gutterBottom>
                Job Applications
            </Typography>

            <TableComponent
                columns={columns}
                data={applications ? applications : []}
                datacolumns={datacolumns}
                // actions={actions}
            />
        </>
    )
}

export default CandidateApplications;