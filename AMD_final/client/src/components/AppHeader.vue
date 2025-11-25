<template>
  <div class="navbar">
    <div class="left">
      <span class="logo" @click="$router.push('/')">URL Shortener</span>

      <button class="nav-btn" @click="$router.push('/')">Home</button>
      <button class="nav-btn" @click="$router.push('/mylinks')">My Links</button>
    </div>

    <div class="right">
      <template v-if="!isLoggedIn">
        <button class="nav-btn" @click="$router.push('/login')">Login</button>
        <button class="nav-btn" @click="$router.push('/register')">Register</button>
      </template>

      <template v-else>
        <button class="nav-btn" @click="logout">Logout</button>
      </template>
    </div>
  </div>
</template>

<script>
import { isLoggedIn, setToken } from "../auth";

export default {
  computed: {
    isLoggedIn() {
      return isLoggedIn();
    }
  },
  methods: {
    logout() {
      setToken(null);
      this.$router.push("/login");
    }
  }
};
</script>

<style>
/* Navbar Wrapper */
.navbar {
  width: 100%;
  height: 60px;
  background: #17a2b8;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 25px;
  box-sizing: border-box;
  color: white;
  font-family: sans-serif;
}

/* Left group: logo + menu */
.left {
  display: flex;
  align-items: center;
  gap: 15px;
}

/* Right group: login/logout */
.right {
  display: flex;
  align-items: center;
  gap: 15px;
}

/* Logo */
.logo {
  font-size: 20px;
  font-weight: bold;
  cursor: pointer;
}

/* Navbar Buttons */
.nav-btn {
  background: transparent;
  border: none;
  color: white;
  font-size: 16px;
  cursor: pointer;
}

.nav-btn:hover {
  text-decoration: underline;
}
</style>
