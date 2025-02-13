import { Button, Container, Typography, Box } from "@mui/material";
import { useNavigate } from "react-router-dom";
import ErrorOutlineIcon from "@mui/icons-material/ErrorOutline";
import { useAuth } from "../contexts/AuthContext";

const Unauthorized = () => {
    const navigate = useNavigate();
    const { logout } = useAuth();

    return (
        <Container maxWidth="sm">
            <Box
                display={"flex"}
                flexDirection={"column"}
                alignItems={"center"}
                justifyContent={"center"}
                height={"100%"}
                textAlign={"center"}
            >
                <ErrorOutlineIcon color="error" sx={{ fontSize : 80, mb : 2}} />

                <Typography variant="h4" fontWeight={"bold"} gutterBottom>
                    Access Denied
                </Typography>

                <Typography>
                    You do not have the necessary permissions to view this page.
                    Please contact the administrator if you believe this is a mistake.
                </Typography>

                <Button 
                    variant="contained"
                    color="primary"
                    onClick={() => {
                        navigate("/")
                        logout();
                    }}
                    sx={{mt : 2}}
                >
                    Go to Login
                </Button>
            </Box>

        </Container>
    )

}

export default Unauthorized;