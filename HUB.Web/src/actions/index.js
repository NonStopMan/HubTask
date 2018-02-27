import axios from 'axios';
const ROOT_URL = '/api/posts';
export const FETCH_POSTS = 'FETCH_POSTS';
export const READ_POST = 'READ_POST';

export function getPosts() {
    const url = `${ROOT_URL}/get-user-posts`;
    const request = axios.get(url);
    return {
        type: FETCH_POSTS,
        payload: request
    }
}
export function readPost(postId) {
    return {
        type: READ_POST,
        payload: postId
    }
}
