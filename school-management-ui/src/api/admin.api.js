import api from "./axios";

export const getUsers = () =>
  api.get("/admin/users");

export const lockUser = (id) =>
  api.post(`/admin/users/${id}/lock`);

export const unlockUser = (id) =>
  api.post(`/admin/users/${id}/unlock`);

export const softDeleteUser = (id) =>
  api.delete(`/admin/users/${id}`);
