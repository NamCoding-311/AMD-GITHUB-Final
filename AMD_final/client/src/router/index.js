import { createRouter, createWebHistory } from "vue-router";
import HomePage from "../pages/HomePage.vue";
import MyLinks from "../pages/MyLinks.vue";
import Login from "../pages/Login.vue";
import Register from "../pages/Register.vue";

const routes = [
  { path: "/login", name: "Login", component: Login },
  { path: "/register", name: "Register", component: Register },

  { path: "/", name: "Home", component: HomePage },
  { path: "/mylinks", name: "MyLinks", component: MyLinks },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// ================================
// ⭐ Route Guard: bảo vệ route FE (DUY NHẤT)
// ================================
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem("token");

  // Nếu chưa login -> CHỈ cho vào /login và /register
  if (!token && to.path !== "/login" && to.path !== "/register") {
    return next("/login");
  }

  // Nếu đã login -> KHÔNG cho vào login/register nữa
  if (token && (to.path === "/login" || to.path === "/register")) {
    return next("/");
  }

  next();
});

export default router;
