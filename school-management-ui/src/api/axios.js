import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7209/api",
  headers: {
    "Content-Type": "application/json",
  },
});

// REQUEST INTERCEPTOR
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("accessToken");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// RESPONSE INTERCEPTOR
api.interceptors.response.use(
  res => res,
  async err => {
    if (err.response?.status === 401) {
      const refresh = localStorage.getItem("refreshToken");
      if (!refresh) return Promise.reject(err);

      const res = await api.post("/auth/refresh", {
        refreshToken: refresh
      });

      localStorage.setItem("accessToken", res.data.accessToken);
      localStorage.setItem("refreshToken", res.data.refreshToken);

      err.config.headers.Authorization =
        `Bearer ${res.data.accessToken}`;

      return api(err.config);
    }

    return Promise.reject(err);
  }
);

export default api;
