export const reducer = (state = {}, action) => {
    switch (action.type) {
        case 'LOG_IN':
            return { ...state, logged: true, loggedUserId: action.loggedUserId };
        case 'LOG_OUT':
            return { ...state, logged: false, loggedUserId: null };
        default:
            return state;
    }
};