import { createStore } from 'redux';
import { reducer } from './reducers/log_reducer';

export const store = createStore(reducer, { logged: false, loggedUserId: 0 });
