const CandidateReducer = (state, action) => {

    switch (action.type) {

        case "SET_LOADING":
            return { ...state, isLoading: true, isError: false };

        case "SET_ERROR":
            return { ...state, isLoading: false, isError: true };

        case "ADD_CANDIDATE":
            return {
                ...state,
                isLoading: false,
                candidates: [...state.candidates, action.payload],
                filteredCandidates: [...state.candidates, action.payload]
            };

        case "GET_CANDIDATES":

            const candidates1 = action.payload?.map((candidate) => {
                let skills = "";

                candidate?.CandidateSkills?.map((skill) => {
                    let strings1 = "";
                    strings1 += skill.FkSkill.Name + " ";
                    strings1 += "(" + String(skill.YearsOfExperience) + " year),";
                    skills += strings1;
                })

                skills = skills.substring(0, skills.length-1);
                candidate.skills = skills;

                return candidate;
            })           

            return {
                ...state,
                isLoading: false,
                candidates: candidates1,
                filteredCandidates: candidates1
            };


        case "DELETE_CANDIDATE":
            return {
                ...state,
                isLoading : false,
                candidates : state.candidates.filter((candidate) => {
                    if(candidate.PkUserId === action.payload){
                        return false;
                    }
                }),
                filteredCandidates : state.candidates.filter((candidate) => {
                    if(candidate.PkUserId === action.payload){
                        return false;
                    } else {
                        return true;
                    }

                })
            }

        case "SEARCH_CANDIDATE":
            return {
                ...state,
                searchTerm : action.payload,
                filteredCandidates : state.candidates?.filter((candidate) => 
                    candidate.Email.toLowerCase().includes(action.payload.toLowerCase())
                ),
            }          


        default:
            return state;
    }
}

export default CandidateReducer;