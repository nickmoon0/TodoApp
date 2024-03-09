'use client';

import React, { createContext, useContext, useMemo } from 'react';
import axios from 'axios';
import { useToken } from '@/hooks/useToken';

const ApiContext = createContext();

export const useApiContext = () => useContext(ApiContext);

export const ApiProvider = ({ children }) => {
  const { getToken, setToken } = useToken();

  const api = useMemo(() => {
    const instance = axios.create({
      baseURL: process.env.NEXT_PUBLIC_BASE_URL
    });

    // Request interceptor
    instance.interceptors.request.use((config) => {
      const token = getToken();
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    }, (error) => Promise.reject(error));

    // Response interceptor
    instance.interceptors.response.use(
      (response) => response,
      async (error) => {
        const originalRequest = error.config;
    
        if (error.response.status === 401 && !originalRequest._retry) {
          originalRequest._retry = true;
          try {
            const response = await axios.get(`${process.env.NEXT_PUBLIC_BASE_URL}/auth/renew-token`, {
              withCredentials: true // Attach refresh-token
            });
    
            // Overwrite old token with new one
            const newToken = response.data.accessToken;
            setToken(newToken);
    
            originalRequest.headers.Authorization = `Bearer ${newToken}`;
            return axios(originalRequest); // LINE 45
    
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

    return instance;
  }, [getToken, setToken]);

  return (
    <ApiContext.Provider value={api}>
      {children}
    </ApiContext.Provider>
  );
};

