import { useEffect, useState } from "react";
import { getUsers, lockUser, unlockUser, softDeleteUser } from "../api/admin.api";

export default function AdminUsers() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const res = await getUsers();
    setUsers(res.data);
  };

  return (
    <div>
      <h2>Users</h2>

      {users.map(u => (
        <div key={u.id}>
          {u.email} {u.userName}

          <button onClick={() => lockUser(u.id)}>Lock</button>
          <button onClick={() => unlockUser(u.id)}>Unlock</button>
          <button onClick={() => softDeleteUser(u.id)}>Delete</button>
        </div>
      ))}
    </div>
  );
}
