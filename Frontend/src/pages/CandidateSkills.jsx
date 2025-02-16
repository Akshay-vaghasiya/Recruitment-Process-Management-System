import { Button, CircularProgress } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import candidateService from "../services/candidateService";
import { useAuth } from "../contexts/AuthContext";
import AuthHeader from "../helper/AuthHeader";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { fireToast } from "../components/fireToast";
import TableComponent from "../components/TableComponent";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import CustomDialogForm from "../components/CustomDialogForm";
import { useSkillContext } from "../contexts/SkillContext";
import ConfirmationDialog from "../components/ConfirmationDialog";


const CandidateSkills = () => {


    const { getCandidateSkills, addCandidateSkill, deleteCandidateSkill } = candidateService;
    const { logout, candidate } = useAuth();
    const headers = AuthHeader();
    const [skills1, setSkills1] = useState([]);
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isUpdate, setIsUpdate] = useState(false);
    const { skills, getAllSkills } = useSkillContext();
    const [skillOption, setSkillOption] = useState([]);
    const [formData, setFormData] = useState([]);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);


    const fetchCandiateSkills = async () => {
        try {
            setIsLoading(true);
            const data = await getCandidateSkills(candidate.PkCandidateId, headers);
            if (data != null) {
                setSkills1(
                    data?.map((skill) => {
                        skill.skill = skill.FkSkill.Name;
                        return skill;
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

        if(skills.length === 0) {
            getAllSkills(navigate);
        } else {
            setSkillOption(
                skills?.map((skill) => {
                    return {label : skill.Name, value : skill.PkSkillId}
                })
            )
        }


    }, [skills])

    useEffect(() => {
        fetchCandiateSkills();
        getAllSkills(navigate);
    }, [])

    console.log(skills);



    const handleAdd = () => {
        setFormData({
            FkSkillId : "",
            YearsOfExperience : 0
        })
        setIsUpdate(false);
        setIsFormOpen(true);
    }

    const handleEdit = (skill) => {
        setFormData(skill);
        setIsUpdate(true);
        setIsFormOpen(true)
    }

    const handleDelete = (skill) => {
        setFormData(skill);
        setIsConfirmOpen(true);
    }

    const handleSubmit = async () => {
        try {
      

            await addCandidateSkill(candidate?.PkCandidateId, formData?.FkSkillId, formData?.YearsOfExperience, headers);
            if (isUpdate) {
                fireToast("Successfully document updated", "success");
                await fetchCandiateSkills();
                setIsFormOpen(false);
            } else {
                fireToast("Successfully document added", "success");
                await fetchCandiateSkills();
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

    const handleConfirmDelete = async () => {
        try {
            await deleteCandidateSkill(formData?.PkCandidateSkillId, headers);
            fireToast("Successfully skill deleted", "success");
            await fetchCandiateSkills();
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

    const columns = ["ID", "Skill", "Year of experience"];
    const datacolumns = ["PkCandidateSkillId", "skill", "YearsOfExperience"];

    const actions = [
        {
            label: "Update",
            color: "primary",
            handler: handleEdit,
            icon: <EditIcon />,
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
            label: "Skill", name: "FkSkillId", type: "select", options: skillOption,
             isMultiple: false, required: true, disabled: isUpdate? true : false
        },
        { label: "Year Of Experience", name: "YearsOfExperience", type: "number", required: true },
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
                Add Skill
            </Button>

            <TableComponent
                columns={columns}
                data={skills1?.length > 0 ? skills1 : []}
                datacolumns={datacolumns}
                actions={actions}
            />

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit}
                title={isUpdate ? "Edit Skill" : "Add SKill"}
                submitButtonText={isUpdate ? "Update" : "Add"}
                fields={formFields}
            >
            </CustomDialogForm>

            <ConfirmationDialog
                open={isConfirmOpen}
                title="Confirm Delete"
                message={`Are you sure you want to delete ${formData?.skill} skill ?`}
                onConfirm={handleConfirmDelete}
                onCancel={setIsConfirmOpen}
            />
        </>
    )
}

export default CandidateSkills;