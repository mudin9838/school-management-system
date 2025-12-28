//login page
import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { login as loginApi } from "../api/auth.api";
import { AuthContext } from "./AuthContext";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(null);
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      setError(null);
  
      const data = await loginApi(email, password);
  
      // 🔐 STORE TOKENS
      localStorage.setItem("refreshToken", data.refreshToken);
  
      // store access token + roles via context
      login(data.accessToken);
  
      navigate("/", { replace: true });
    } catch (err) {
      console.error(err);
      setError("Invalid email or password.");
    }
  };
  
  
  return (
    <div>
      <h2>Login</h2>
      {error && <p>{error}</p>}
      <input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
}