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

const JobPositionManagement = () => {

    const { searchTerm, isLoading, isError, jobPositions, filteredJobPositions, addJob, getAllJob, searchJob, editJob, removeJob } = useJobPositionContext();

    const { skills, getAllSkills} = useSkillContext();

    const { users, getAllUsers} = useUserContext();

    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [selectedJobPosition, setSelectedJobPosition] = useState(null);
    const [formData, setFormData] = useState({});
    const [skillOption, setSkillOption] = useState([]);
    const [reviewerOption, setReviewerOption] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {

        if (!jobPositions.length) {
            getAllJob(navigate);
        }

    }, [jobPositions]);

    useEffect(() => {
        if(!skills.length) {
            getAllSkills(navigate);
        } else {
            const temp = skills?.map((skill) => {
                return { value: skill?.PkSkillId, label: skill?.Name }
            });
            setSkillOption(temp);
        }

    }, [skills])

    useEffect(() => {

        if(!users.length) {
            getAllUsers(navigate);
        } else {
            let users1 = users?.filter((user) => {
                
                let x = user?.UserRoles?.filter((role) => {
                    if(role.FkRole?.Name === "REVIEWER")
                    {
                        return true;
                    }
                })

                if(x.length > 0) return true
            })
            const temp = users1?.map((user) => {
                return { value: user.PkUserId, label: user.FullName } 
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
            FkReviewerId : "",
        });
        setSelectedJobPosition(null);
        setIsFormOpen(true);
    };

    const handleEdit = (job) => {
        setFormData(job);
        setSelectedJobPosition(job);
        setFormData((prev) => ({
            ...prev, RequireSkills : job?.requiredSkill==="" ? [] : job?.requiredSkill?.split(',')?.map((skill) => {
                let x = skillOption?.filter((skill1) => {
                    if(skill1.label === skill) {
                        return true;
                    }
                })
                if(x.length) return x[0].value;
            }),
            Skills : job?.preferredSkill==="" ? [] : job?.preferredSkill?.split(',')?.map((skill) => {
                let x = skillOption?.filter((skill1) => {
                    if(skill1.label === skill) {
                        return true;
                    }
                })
                if(x.length) return x[0].value;
            }),
        }));
        setIsFormOpen(true);
    };

    const handleDelete = (job) => {
        setSelectedJobPosition(job);
        setIsConfirmOpen(true);
    };

    const handleSubmit = () => {
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
        navigate(`/user/job-applictions/${job.PkJobPositionId}`)
    }

    const columns = ["ID", "Title", "Description", "requiredSkill", "preferredSkill", "Status", "CreatedAt"];
    const datacolumns = ["PkJobPositionId", "Title", "Description", "requiredSkill", "preferredSkill", "status", "CreatedAt"];
 

    const formFields = [
        { label: "Title", name: "Title", type: "text", required: true },
        { label: "Description", name: "Description", type: "textarea", required: true },
        { label: "ClosureReason", name: "ClosureReason", type: "textarea", required: true },
        {
            label: "Status", name: "FkStatusId", type: "select", options: [
                {label : "OPEN", value : 2},
                {label : "ON HOLD", value : 3},
                {label : "CLOSE", value : 4}
            ], 
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
            label: "FkReviewerId", name: "FkReviewerId", type: "select", options: reviewerOption,
            isMultiple: false, required: true
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