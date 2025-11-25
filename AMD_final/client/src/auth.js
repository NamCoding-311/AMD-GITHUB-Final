import { reactive } from "vue";

export const authState = reactive({
  token: localStorage.getItem("token")
});

export function setToken(token) {
  authState.token = token;
  if (token) localStorage.setItem("token", token);
  else localStorage.removeItem("token");
}

export function isLoggedIn() {
  return !!authState.token;
}
