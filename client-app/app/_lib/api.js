import axios from 'axios';
import { getToken, setToken } from './getToken';
const baseURL = process.env.NEXT_PUBLIC_BASE_URL

const api = axios.create({
  baseURL: baseURL
});

/*
 * Defining request interceptor
 */
api.interceptors.request.use((config) => {
  const token = getToken();

  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config;
}, (error) => Promise.reject(error));

/*
 * Defining response interceptor.
 * Responses forwarded on status codes that arent 4xx or 5xx.
 * Intercepted if response code is an error.
 */
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      try {
        const response = await axios.get(`${baseURL}/auth/renew-token`, {
          withCredentials: true // Attach refresh-token
        });

        // Overwrite old token with new one
        const token = response.data.accessToken;
        setToken(token);

        originalRequest.headers.Authorization = `Bearer ${token}`;
        return axios(originalRequest);

      } catch (error) {
        // redirect to login (unless page is already login)
        if (typeof window !== "undefined" && window.location.pathname !== '/login') {
          // Perform a client-side redirect
          window.location.href = '/login';
        }
      }
    }
    return Promise.reject(error);
  });

export default api;