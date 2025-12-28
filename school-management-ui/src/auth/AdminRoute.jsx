import { Navigate } from "react-router-dom";

export default function AdminRoute({ children }) {
  const roles = JSON.parse(localStorage.getItem("roles")) || [];

  if (!roles.includes("Admin")) {
    return <Navigate to="/" replace />; 
  }

  return children;
}
