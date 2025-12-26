//auth call api ,which calls our existing AuthController.
import api from "./axios";

export const login = async (email, password) => {
  const response = await api.post("/auth/login", { email, password })
    return response.data;
};