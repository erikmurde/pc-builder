import axios from 'axios';

const hostBaseURL = process.env.NODE_ENV === "production" 
    ? process.env.REACT_APP_BACKEND_URL
    : "http://localhost:5250/api/v1";

const baseAxios = axios.create(
    {
        baseURL: hostBaseURL,
        headers: {
            'Content-Type': 'application/json'
        },
        params: {
            jwt_data: null
        }
    }
)

export default baseAxios;