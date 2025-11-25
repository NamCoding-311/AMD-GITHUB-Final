<template>
  <div class="register-wrapper">
    <div class="register-box">
      <h2>Register</h2>

      <div class="form">
        <input v-model="username" type="text" placeholder="Username" />
        <input v-model="password" type="password" placeholder="Password" />

        <button @click="register">Create Account</button>

        <p v-if="error" class="error">{{ error }}</p>
        <p v-if="success" class="success">{{ success }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "../utils/axios";

export default {
  data() {
    return {
      username: "",
      password: "",
      error: "",
      success: ""
    };
  },

  methods: {
    async register() {
      this.error = "";
      this.success = "";

      if (!this.username || !this.password) {
        this.error = "Vui lòng nhập đầy đủ!";
        return;
      }

      try {
        await axios.post("/auth/register", {
          username: this.username,
          password: this.password
        });

        this.success = "Đăng ký thành công! Đang chuyển sang login...";

        setTimeout(() => {
          this.$router.push("/login");
        }, 1500);

      } catch (err) {
        this.error = "Username đã tồn tại!";
      }
    }
  }
};
</script>

<style>
/* Toàn nền */
.register-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  padding-top: 60px;
}

/* Khung giống Login */
.register-box {
  width: 380px;
  background: #fff;
  border-radius: 10px;
  padding: 30px 25px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.12);
  text-align: center;
}

.register-box h2 {
  margin-bottom: 18px;
  color: #333;
}

/* Form style */
.form input {
  width: 100%;
  padding: 12px;
  margin: 10px 0;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 15px;
}

button {
  width: 100%;
  padding: 12px;
  margin-top: 10px;
  background: #009688;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background: #00796b;
}

.error {
  color: #e53935;
  margin-top: 10px;
}

.success {
  color: #43a047;
  margin-top: 10px;
}
</style>
