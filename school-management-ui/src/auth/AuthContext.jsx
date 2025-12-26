//AUTH CONTEXT (JWT STORAGE)
import { createContext, useState } from "react";

export const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => { //provides authentication context to its children components
  const [token, setToken] = useState(
    localStorage.getItem("accessToken")
  );

  const login = (jwt) => {
    localStorage.setItem("accessToken", jwt);
    setToken(jwt);
  };

  const logout = () => {
    localStorage.removeItem("accessToken");
    setToken(null);
  };

  return (
    <AuthContext.Provider value={{ token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
