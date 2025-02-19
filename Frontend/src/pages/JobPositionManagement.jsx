import React, { useEffect, useState } from "react";
import TableComponent from "../components/TableComponent";
import CustomDialogForm from "../components/CustomDialogForm";
import ConfirmationDialog from "../components/ConfirmationDialog";
import { Button, Box, Typography, CircularProgress, TextField, IconButton, Switch, Input } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useNavigate } from "react-router-dom";
import { useJobPositionContext } from "../contexts/JobPositionContext";
import { useSkillContext } from "../contexts/SkillContext";
import { useUserContext } from "../contexts/UserContext";
import JobApplications from "./JobApplications";
import { FirstPage } from "@mui/icons-material";
import { fireToast } from "../components/fireToast";
import jobPositionService from "../services/jobPositionService";
import { useAuth } from "../contexts/AuthContext";

const JobPositionManagement = () => {

    const { searchTerm, isLoading, isError, jobPositions, filteredJobPositions, addJob, getAllJob, searchJob, editJob, removeJob } = useJobPositionContext();

    const { skills, getAllSkills } = useSkillContext();

    const { users, getAllUsers } = useUserContext();

    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isFormOpen1, setIsFormOpen1] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [selectedJobPosition, setSelectedJobPosition] = useState(null);
    const [formData, setFormData] = useState({});
    const [skillOption, setSkillOption] = useState([]);
    const [reviewerOption, setReviewerOption] = useState([]);
    const [candidateOptions, setCandidateOptions] = useState([]);
    const navigate = useNavigate();
    const {getJobStatus} = jobPositionService;
    const [jobStatusOptions, setJobStatusOptions] = useState([]);
    const {logout} = useAuth();

    const today = new Date().toISOString().split("T")[0]; 

    useEffect(() => {
        const fetchJobStatus = async () => {
            try {
                const data = await getJobStatus();
                setJobStatusOptions(
                    data?.map((status) =>{
                        return {label : status.Name, value : status.PkJobStatusId}
                    })
                )       
            } catch (error) {
                if (error?.response?.status === 401 || error?.response?.status === 403) {
                    logout();
                    navigate("/");
                    fireToast("Unauthorized access", "error");
                }
                fireToast(error?.response?.data, "error");
            }
        }

        fetchJobStatus();
    },[])

    useEffect(() => {

        if (!jobPositions.length) {
            getAllJob(navigate);
        }

    }, [jobPositions]);

    useEffect(() => {
        if (!skills.length) {
            getAllSkills(navigate);
        } else {
            const temp = skills?.map((skill) => {
                return { value: skill?.PkSkillId, label: skill?.Name }
            });
            setSkillOption(temp);
        }

    }, [skills])

    useEffect(() => {

        if (!users.length) {
            getAllUsers(navigate);
        } else {
            let users1 = users?.filter((user) => {

                let x = user?.UserRoles?.filter((role) => {
                    if (role.FkRole?.Name === "REVIEWER") {
                        return true;
                    }
                })

                if (x.length > 0) return true
            })
            const temp = users1?.map((user) => {
                return { value: user.PkUserId, label: user.FullName + "(" + user.Email + ")" }
            })
            setReviewerOption(temp);
        }

    }, [users])

    const handleAdd = () => {
        setFormData({
            Title: "",
            Description: "",
            RequireSkills: [],
            Skills: "",
            FkStatusId: "",
            ClosureReason: "",
            FkReviewerId: "",
            FkSelectedCandidateId: null
        });
        setSelectedJobPosition(null);
        setIsFormOpen(true);
    };

    const handleEdit = (job) => {

        if(job.JoiningDate == null) {
            job.JoiningDate = "";
        }
        setFormData(job);
        setCandidateOptions(
            job?.JobApplications?.map((application) => {

                let label = application.FkCandidate.FullName + "(" + application.FkCandidate.Email + ")";
                let value = application.FkCandidate.PkCandidateId;

                return { label: label, value: value };
            })
        )
        setSelectedJobPosition(job);
        setFormData((prev) => ({
            ...prev, RequireSkills: job?.requiredSkill === "" ? [] : job?.requiredSkill?.split(',')?.map((skill) => {
                let x = skillOption?.filter((skill1) => {
                    if (skill1.label === skill) {
                        return true;
                    }
                })
                if (x.length) return x[0].value;
            }),
            Skills: job?.preferredSkill === "" ? [] : job?.preferredSkill?.split(',')?.map((skill) => {
                let x = skillOption?.filter((skill1) => {
                    if (skill1.label === skill) {
                        return true;
                    }
                })
                if (x.length) return x[0].value;
            }),
        }));
        setIsFormOpen(true);
    };

    const handleDelete = (job) => {
        setSelectedJobPosition(job);
        setIsConfirmOpen(true);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const flag = formData?.RequireSkills?.some(s => formData?.Skills?.includes(s));

        if (flag) {
            fireToast("Please check require skills and skills both contain atleast one same element", "error");
            return;
        }

        if (selectedJobPosition) {
            editJob(selectedJobPosition.PkJobPositionId, formData, navigate);
        } else {
            addJob(formData, navigate);
        }
        setIsFormOpen(false);
    };

    const handleConfirmDelete = () => {
        removeJob(selectedJobPosition.PkJobPositionId, navigate);
        setIsConfirmOpen(false);
    };

    const handleSearch = (event) => {
        searchJob(event.target.value);
    };

    const handleApplication = (job) => {
        setSelectedJobPosition(job);
        setIsFormOpen1(true);
    }

    const columns = ["ID", "Title", "Description", "requiredSkill", "preferredSkill", "Status", "CreatedAt"];
    const datacolumns = ["PkJobPositionId", "Title", "Description", "requiredSkill", "preferredSkill", "status", "CreatedAt"];


    const formFields = [
        { label: "Title", name: "Title", type: "text", required: true },
        { label: "Description", name: "Description", type: "textarea", required: true },
        { label: "ClosureReason", name: "ClosureReason", type: "textarea", required: true },
        {
            label: "Status", name: "FkStatusId", type: "select", options: jobStatusOptions,
            isMultiple: false, required: true
        },
        {
            label: "RequireSkills", name: "RequireSkills", type: "select", options: skillOption,
            isMultiple: true, required: true
        },
        {
            label: "Skills", name: "Skills", type: "select", options: skillOption,
            isMultiple: true, required: true
        },
        {
            label: "ReviewerId", name: "FkReviewerId", type: "select", options: reviewerOption,
            isMultiple: false, required: true
        },
        {
            label: "SelectedCandidateId", name: "FkSelectedCandidateId", type: "select", options: candidateOptions,
            isMultiple: false, required: true, disabled : selectedJobPosition ? false : true,
        },
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
            label: "Applications",
            color: "secondary",
            handler: handleApplication,
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
                Job Position Management
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
                    Add Job Position
                </Button>
            </Box>

            <TableComponent
                columns={columns}
                data={filteredJobPositions}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={selectedJobPosition ? "Edit Job Position" : "Add Job Position"}
                submitButtonText={selectedJobPosition ? "Update" : "Create"}
                fields={formFields}
            >
               { selectedJobPosition != null ? <TextField
                    label="Joining Date"
                    type="date"
                    name="JoiningDate"
                    disabled={formData.FkSelectedCandidateId ? false : true}
                    value={formData.JoiningDate}
                    onChange={(e) => setFormData((prev) => ({ ...prev, JoiningDate: e.target.value }))}
                    InputLabelProps={{
                        shrink: true,
                    }}
                    inputProps={{
                        min: today,
                    }}
                    sx={{ mt: 2 }}
                    fullWidth
                /> : ""}
            </CustomDialogForm>

            <CustomDialogForm
                open={isFormOpen1}
                onClose={setIsFormOpen1}
                formData={null}
                setFormData={null}
                onSubmit={null}
                title={"Job Applications"}
                submitButtonText={""}
                fields={[]}
                size="lg"
            >
                <JobApplications
                    jobPositionId={selectedJobPosition?.PkJobPositionId}
                />
            </CustomDialogForm>

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Delete"
                message={`Are you sure you want to delete the Job: ${selectedJobPosition?.Title}?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />
        </Box>
    );
};
export default JobPositionManagement;