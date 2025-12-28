import { Navigate } from "react-router-dom";
import { useContext } from "react";
import { AuthContext } from "./AuthContext";

const ProtectedRoute = ({ children }) => {
  const { token, loading } = useContext(AuthContext);

  if (loading) return null; 

  return token ? children : <Navigate to="/login" replace />;
};

export default ProtectedRoute;
