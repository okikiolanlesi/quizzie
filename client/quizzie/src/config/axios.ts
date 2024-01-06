import { useAuthState } from "@/store/authStore";
import axios from "axios";

const axiosConfig = axios.create({
  baseURL: process.env.NEXT_PUBLIC_BACKEND_URL,
  withCredentials: false,
});

axiosConfig.interceptors.request.use(
  function (config) {
    //Getting the auth token that was persisted by zustand
    let authToken;
    const authStateString = localStorage.getItem("auth");
    if (authStateString) {
      const authObj = JSON.parse(authStateString);

      authToken = authObj?.state?.token;
    }

    // Only add authToken to headers if it exists
    if (authToken) {
      config.headers.Authorization = "Bearer " + authToken;
    }

    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

axiosConfig.interceptors.response.use(
  function (response) {
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    return response;
  },
  function (error) {
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error

    if (
      error.response.status == 401 ||
      error.response?.data?.error === "Not authorized, token failed" ||
      error.response.status == 401
    ) {
      window.location.href = "/login";
    }
    return Promise.reject(error);
  }
);

export default axiosConfig;
