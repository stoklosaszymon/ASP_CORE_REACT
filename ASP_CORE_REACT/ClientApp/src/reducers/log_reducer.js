export const reducer = (state = {}, action) => {
    switch (action.type) {
        case 'LOG_IN':
            return { ...state, logged: true };
        case 'LOG_OUT':
            return { ...state, logged: false };
        default:
            return state;
    }
};