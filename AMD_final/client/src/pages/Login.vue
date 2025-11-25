<template>
  <div class="login-wrapper">
    <div class="login-card">
      <h2 class="title">🔐 Login</h2>

      <input
        v-model="username"
        type="text"
        placeholder="Username"
        class="input"
      />

      <input
        v-model="password"
        type="password"
        placeholder="Password"
        class="input"
      />

      <button class="btn" @click="login">
        Login
      </button>

      <p v-if="error" class="error">{{ error }}</p>
    </div>
  </div>
</template>

<script>
import axios from "../utils/axios";
import { setToken } from "../auth";

export default {
  data() {
    return {
      username: "",
      password: "",
      error: "",
    };
  },

  methods: {
    async login() {
      this.error = "";
      try {
        const res = await axios.post("/auth/login", {
          username: this.username,
          password: this.password,
        });

        setToken(res.data.token);
        this.$router.push("/");
      } catch {
        this.error = "Sai username hoặc password!";
      }
    },
  },
};
</script>

<style>
/* --- FULL PAGE CENTER --- */
.login-wrapper {
  width: 100%;
  min-height: 80vh;
  display: flex;
  justify-content: center;
  align-items: center;
}

/* --- LOGIN CARD --- */
.login-card {
  width: 380px;
  padding: 30px;
  border-radius: 12px;
  background: white;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
  text-align: center;
}

.title {
  margin-bottom: 20px;
  color: #0a6c74;
  font-size: 26px;
  font-weight: bold;
}

/* --- INPUT --- */
.input {
  width: 100%;
  padding: 12px 14px;
  margin: 10px 0;
  border-radius: 8px;
  border: 1px solid #dcdcdc;
  font-size: 15px;
  transition: 0.2s;
}

.input:focus {
  border-color: #0a6c74;
  outline: none;
  box-shadow: 0 0 4px rgba(0,150,150,0.3);
}

/* --- BUTTON --- */
.btn {
  width: 100%;
  padding: 12px;
  margin-top: 10px;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  cursor: pointer;
  background: #0a6c74;
  color: white;
  transition: 0.2s;
}

.btn:hover {
  background: #118890;
}

/* --- ERROR --- */
.error {
  margin-top: 10px;
  color: #e60000;
  font-size: 14px;
}
</style>
  