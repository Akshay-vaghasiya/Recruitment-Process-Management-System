import { Button, colors, TextField } from "@mui/material";
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
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import dayjs from 'dayjs';

const Interview = ({ application }) => {

    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isFormOpen1, setIsFormOpen1] = useState(false);
    const [isFormOpen2, setIsFormOpen2] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [formData, setFormData] = useState({});
    const [interviews, setInterviews] = useState([]);
    const { logout } = useAuth();
    const headers = AuthHeader();
    const [loading, setLoading] = useState(false);
    const { getInterviewByCandidateAndPosition, addInterview, deleteInterview, updateInterview, 
        interviewAllStatus, getAllInteviewRounds } = interviewService;
    const [value, setValue] = useState(dayjs(new Date().toISOString()));
    const [interviewStatusOptions, setInterviewStatusOptions] = useState([]);
    const [interviewRoundOptions, setInterviewRoundOptions] = useState([]);

    useEffect(() => {

        const fetchinterview = async () => {

            try {

                let data = await getInterviewByCandidateAndPosition(application?.FkCandidateId, application?.FkJobPositionId, headers);

                if (data != null) {

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

    useEffect(() => {
        const fetchInterviewStatus = async () => {
            try {
                const data = await interviewAllStatus();
                setInterviewStatusOptions(
                    data?.map((status) => {
                        return { label: status.Name, value: status.PkInterviewStatusId }
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

        const fetchInterviewRounds = async () => {
            try {
                const data = await getAllInteviewRounds();
                setInterviewRoundOptions(
                    data?.map((round) => {
                        return { label: round.Name, value: round.PkInterviewRoundId }
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

        fetchInterviewStatus();
        fetchInterviewRounds();
    }, [])


    const handleAdd = () => {

        setFormData({
            FkCandidateId: application?.FkCandidateId,
            FkJobPositionId: application?.FkJobPositionId,
            FkInterviewRoundId: "",
            RoundNumber: 1,
            FkStatusId: "",
            ScheduledTime: new Date().toISOString()
        })
        setValue(dayjs(new Date().toISOString()))
        setIsFormOpen(true);
    }


    const handleSubmit = async () => {

        try {
            const data = await addInterview(formData, headers);
            if (data != null) {
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

            const data = await updateInterview(formData.PkInterviewId, formData, headers);
            if (data != null) {
                fireToast("successfully interview details updated", "success");
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
        setValue(dayjs(interview.ScheduledTime));
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
            if (data != null) {
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


    const columns = ["ID", "Round Name", "Round no.", "Status", "ScheduledTime", "CreatedAt"];
    const datacolumns = ["PkInterviewId", "round", "RoundNumber", "status", "ScheduledTime", "CreatedAt"];

    const actions = [
        {
            label: "Update",
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
            label: "Interview Round", name: "FkInterviewRoundId", type: "select", options: interviewRoundOptions, isMultiple: false, required: true
        },
        {
            label: "Interview Status", name: "FkStatusId", type: "select", options: interviewStatusOptions,
            isMultiple: false, required: true
        },
    ];

    const formFields1 = [
        { label: "Round number", name: "RoundNumber", type: "number", required: true },
        {
            label: "Interview Status", name: "FkStatusId", type: "select", options: interviewStatusOptions,
            isMultiple: false, required: true
        },
    ]

    const handleDateTimeChange = (newValue) => {
        setFormData((prev) => ({
            ...prev, ScheduledTime: newValue.toISOString()
        }))
        setValue(newValue);
    };

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                startIcon={<AddIcon />}
                onClick={handleAdd}
                sx={{ mb: 2 }}
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
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <DemoContainer components={['DateTimePicker']}>
                        <DateTimePicker label="Schedule time" value={value}
                            onChange={handleDateTimeChange}
                            renderInput={(params) => <TextField {...params} />}
                        />
                    </DemoContainer>
                </LocalizationProvider>

            </CustomDialogForm>

            <CustomDialogForm
                open={isFormOpen1}
                onClose={setIsFormOpen1}
                formData={formData}
                setFormData={setFormData}
                onSubmit={handleSubmit1}
                title={"Update Interview"}
                submitButtonText={"Update"}
                fields={formFields1}
            >
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <DemoContainer components={['DateTimePicker']}>
                        <DateTimePicker label="Schedule time" value={value}
                            onChange={handleDateTimeChange}
                            renderInput={(params) => <TextField {...params} />}
                        />
                    </DemoContainer>
                </LocalizationProvider>

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
                    interview={formData}
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