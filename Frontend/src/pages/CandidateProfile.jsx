import { Button, Input, Typography } from "@mui/material";
import { useAuth } from "../contexts/AuthContext";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Form from "../components/Form";
import candidateService from "../services/candidateService";
import AuthHeader from "../helper/AuthHeader";
import { fireToast } from "../components/fireToast";
import PsychologyIcon from '@mui/icons-material/Psychology';
import TextSnippetIcon from '@mui/icons-material/TextSnippet';
import CustomDialogForm from "../components/CustomDialogForm";
import CandidateDocuments from "./CandidateDocuments";
import CandidateSkills from "./CandidateSkills";

const CandidateProfile = () => {

    const { logout, candidate } = useAuth();
    const [formData, setFormData] = useState(candidate);
    const [file, setFile] = useState(null);
    const navigate = useNavigate();
    const { updateCandidate } = candidateService;
    const headers = AuthHeader();
    const [documents, setDocuments] = useState([]);
    const [skills, setSkills] = useState([]);
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [open, setOpen] = useState("");

    useEffect(() => {
        setFormData((prev) => ({
            ...prev,
            Password: ""
        }));
    }, [])

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            let bodyFormData = new FormData();
            bodyFormData.append("FullName", formData.FullName);

            if (formData.Password !== null && formData.Password !== "") {
                bodyFormData.append("Password", formData.Password)
            }
            bodyFormData.append("Email", formData.Email);
            bodyFormData.append("Phone", formData.Phone);
            bodyFormData.append("YearsOfExperience", formData.YearsOfExperience);
            bodyFormData.append("Resume", file);

            let headers1 = headers;
            headers1['Content-Type'] = "multipart/form-data";

            const data = await updateCandidate(candidate?.PkCandidateId, bodyFormData, headers1);
            if (data != undefined && data != null) {
                data.Password = "";
                setFormData(data);
                localStorage.setItem("candidate", JSON.stringify(data));
                fireToast("Update Profile successfully", "success");
            }

        } catch (error) {
            if (error.response.status === 401 || error.response.status === 403) {
                logout();
                navigate("/");
            }
            fireToast(error.response.data, "error");
        }
    };

    const handleChange = (e) => {
        let { name, value } = e.target;

        const type = e.target.type;

        if (type === "number") {

            if (value !== "") {
                value = parseInt(value);
            }
        }

        setFormData((prevValues) => ({
            ...prevValues,
            [name]: value,
        }));
    };

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };


    const handleDocuments = () => {
        setDocuments(formData?.Documents);
        setOpen("document")
        setIsFormOpen(true);
    }

    const handleSkills = () => {
        setSkills(formData?.CandidateSkills);
        setOpen("skill");
        setIsFormOpen(true);
    }

    const formFields = [
        { label: "Full Name", name: "FullName", value: formData.FullName, onChange: handleChange },
        { label: "Email", name: "Email", type: "email", value: formData.Email, onChange: handleChange },
        { label: "Password", name: "Password", type: "password", value: formData.Password, onChange: handleChange, required: false },
        { label: "Phone", name: "Phone", type: "text", value: formData.Phone, onChange: handleChange },
        { label: "Year Of Experience", name: "YearsOfExperience", type: "number", value: formData.YearsOfExperience, onChange: handleChange },
    ];


    return (
        <>
            <Typography variant="h4" align="center" gutterBottom>
                Profile Management
            </Typography>

            <Button
                variant="contained"
                color="primary"
                style={{ height: "3rem", marginRight: "2rem" }}
                startIcon={<TextSnippetIcon />}
                onClick={handleDocuments}
            >
                Documents
            </Button>

            <Button
                variant="contained"
                color="primary"
                style={{ height: "3rem" }}
                startIcon={<PsychologyIcon />}
                onClick={handleSkills}
            >
                Skills
            </Button>

            <Form
                buttonLabel="Update Profile"
                fields={formFields}
                onSubmit={handleSubmit}
            >
                {formData?.ResumeUrl != "" ? <a href={formData?.ResumeUrl} target="_blank">click here to show CV</a> : ""}
                <br />
                <br />
                <Typography variant="h6" gutterBottom>
                    Upload CV
                </Typography>
                <Input
                    type="file"
                    title="update file"
                    accept=".docx, .pdf"
                    onChange={handleFileChange}
                    sx={{ mb: 2 }}
                />
            </Form>

            <CustomDialogForm
                open={isFormOpen}
                onClose={setIsFormOpen}
                formData={null}
                setFormData={null}
                onSubmit={null}
                title={open=="document" ? "Your Documents" : "Your Skills"}
                submitButtonText={""}
                fields={[]}
                size={open=="document" ? "md" : "sm"}
            >
               {open=="document" ? <CandidateDocuments /> : <CandidateSkills /> }
            </CustomDialogForm>
        </>
    )
}

export default CandidateProfile;