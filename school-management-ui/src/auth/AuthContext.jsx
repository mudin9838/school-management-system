import { jwtDecode } from "jwt-decode";
import { createContext, useEffect, useState } from "react";

export const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState(null);
  const [loading, setLoading] = useState(true); // 🔥 NEW

  useEffect(() => {
    const stored = localStorage.getItem("accessToken");

    if (!stored) {
      setLoading(false);
      return;
    }

    try {
      const decoded = jwtDecode(stored);

      if (decoded.exp * 1000 < Date.now()) {
        logout();
      } else {
        setToken(stored);
      }
    } catch {
      logout();
    }

    setLoading(false); 
  }, []);

  const login = (jwt) => {
    const decoded = jwtDecode(jwt);

    const roles =
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    localStorage.setItem("accessToken", jwt);
    localStorage.setItem(
      "roles",
      JSON.stringify(Array.isArray(roles) ? roles : [roles]) 
    );

    setToken(jwt);
  };

  const logout = () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("roles");
    setToken(null);
  };

  return (
    <AuthContext.Provider value={{ token, loading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
