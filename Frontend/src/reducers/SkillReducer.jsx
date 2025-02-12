const UserReducer = (state, action) => {

    switch (action.type) {

        case "SET_LOADING":
            return { ...state, isLoading: true, isError: false };

        case "SET_ERROR":
            return { ...state, isLoading: false, isError: true };

        case "ADD_SKILL":
            return {
                ...state,
                isLoading: false,
                skills: [...state.skills, action.payload],
                filteredSkills: [...state.skills, action.payload]
            };

        case "GET_SKILLS":
            return {
                ...state,
                isLoading: false,
                skills: action.payload,
                filteredSkills: action.payload
            };

        case "DELETE_SKILL":
            return {
                ...state,
                isLoading : false,
                skills : state.skills.filter((skill) => {
                    if(skill.PkSkillId === action.payload){
                        return false;
                    }
                }),
                filteredSkills : state.skills.filter((skill) => {
                    if(skill.PkSkillId === action.payload){
                        return false;
                    } 
                })
            }

        case "SEARCH_SKILL":
            return {
                ...state,
                searchTerm : action.payload,
                filteredSkills : state?.skills?.filter((skill) => 
                    skill.Name.toLowerCase().includes(action.payload.toLowerCase())
                ),
            }          


        default:
            return state;
    }
}

export default UserReducer;