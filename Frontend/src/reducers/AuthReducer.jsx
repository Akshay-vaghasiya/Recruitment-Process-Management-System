const AuthReducer = (state, action) => {
    switch (action.type) {

        case "SET_LOADING":
            return { ...state, isLoading: true, isError: false };

        case "SET_ERROR":
            return { ...state, isLoading: false, isError: true };

        case "USER_LOGIN":
            let roles = [];

            roles = action.payload?.user?.UserRoles?.map((role) => {
                return role?.FkRole?.Name;
            })

            return {
                ...state,
                isLoading: false,
                user: action.payload.user,
                token: action.payload.token,
                roles : roles
            };
        
        case "CANDIDATE_LOGIN":
            return {
                ...state,
                isLoading: false,
                candidate: action.payload.candidate,
                token: action.payload.token,
                roles : ["CANDIDATE"]
            }

        case "LOGOUT":
            return {
                ...state,
                user : null,
                token : null,
                roles : [],
                candidate : null,
            }

        default:
            return state;
    }
};

export default AuthReducer;