const UserReducer = (state, action) => {

    switch (action.type) {

        case "SET_LOADING":
            return { ...state, isLoading: true, isError: false };

        case "SET_ERROR":
            return { ...state, isLoading: false, isError: true };

        case "ADD_USER":

            let user = action.payload;

            let roles = user?.UserRoles?.map((role) => {
                return role.FkRole.Name;
            })

            roles = roles.toString();
            user = {...user, roles};

            return {
                ...state,
                isLoading: false,
                users: [...state.users, user],
                filteredUsers: [...state.users, user]
            };

        case "GET_USERS":
            let users1 = action.payload;
            users1 = action.payload?.map((user) => {
                let roles = [];

                roles = user?.UserRoles?.map((role) => {
                    return role.FkRole.Name;
                })

                roles = roles.toString();
                return { ...user, roles }
            })
            return {
                ...state,
                isLoading: false,
                users: users1,
                filteredUsers: users1
            };


        case "DELETE_USER":
            return {
                ...state,
                isLoading : false,
                users : state.users.filter((user) => {
                    if(user.PkUserId === action.payload){
                        return false;
                    }
                }),
                filteredUsers : state.users.filter((user) => {
                    if(user.PkUserId === action.payload){
                        return false;
                    } else {
                        return true;
                    }

                })
            }

        case "SEARCH_USER":
            return {
                ...state,
                searchTerm : action.payload,
                filteredUsers : state.users?.filter((user) => 
                    user.Email.toLowerCase().includes(action.payload.toLowerCase())
                ),
            }          


        default:
            return state;
    }
}

export default UserReducer;