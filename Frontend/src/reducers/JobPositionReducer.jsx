const JobPositionReducer = (state, action) => {
    switch (action.type) {

        case "SET_LOADING":
            return { ...state, isLoading: true, isError: false };

        case "SET_ERROR":
            return { ...state, isLoading: false, isError: true };

        case "ADD_JOB_POSITION":

            let jobPosition = action.payload;

            let requiredSkill = [];
            let preferredSkill = [];

            jobPosition?.JobSkills?.map((skill) => {
                if (skill.IsRequired === true) {
                    requiredSkill.push(skill.FkSkill.Name);
                } else {
                    preferredSkill.push(skill.FkSkill.Name);
                }
            })

            let status = jobPosition?.FkStatus?.Name;
            requiredSkill = requiredSkill.toString();
            preferredSkill = preferredSkill.toString();
            jobPosition = { ...jobPosition, requiredSkill, preferredSkill, status };

            return {
                ...state,
                isLoading: false,
                jobPositions: [...state.jobPositions, jobPosition],
                filteredJobPositions: [...state.jobPositions, jobPosition]
            };

        case "GET_JOB_POSITONS":
            let jobPositions1 = action.payload;
            jobPositions1 = action.payload?.map((jobPosition) => {
                let requiredSkill = [];
                let preferredSkill = [];

                jobPosition?.JobSkills?.map((skill) => {
                    if (skill.IsRequired === true) {
                        requiredSkill.push(skill?.FkSkill?.Name);
                    } else {
                        preferredSkill.push(skill?.FkSkill?.Name);
                    }
                })

                requiredSkill = requiredSkill.toString();
                preferredSkill = preferredSkill.toString();
                let status = jobPosition.FkStatus.Name;
                return { ...jobPosition, requiredSkill, preferredSkill, status };
            })
            return {
                ...state,
                isLoading: false,
                jobPositions: jobPositions1,
                filteredJobPositions: jobPositions1
            };


        case "DELETE_JOB_POSITION":
            return {
                ...state,
                isLoading: false,
                jobPositions: state.jobPositions.filter((jobPosition) => {
                    if (jobPosition.PkJobPositionId === action.payload) {
                        return false;
                    }
                }),
                filteredJobPositions: state.jobPositions.filter((jobPosition) => {
                    if (jobPosition.PkJobPositionId === action.payload) {
                        return false;
                    } else {
                        return true;
                    }

                })
            }

        case "SEARCH_JOB_POSITION":
            return {
                ...state,
                searchTerm: action.payload,
                filteredJobPositions: state.jobPositions?.filter((jobPosition) =>
                    jobPosition.Title.toLowerCase().includes(action.payload.toLowerCase())
                ),
            }


        default:
            return state;
    }
}

export default JobPositionReducer;