import { FETCH_POSTS, READ_POST } from '../actions/index';


export default function (state = [], action) {
    switch (action.type) {
        case FETCH_POSTS:
            return [...state, ...action.payload.data];
        case READ_POST:
            var postIndexToRemove = state.findIndex((element) => {
                return element.postId === action.payload;
            })
            return [...state.slice(0, postIndexToRemove), ...state.slice(postIndexToRemove + 1, state.length)];
    }
    return state;
}