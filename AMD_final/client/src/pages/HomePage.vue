<template>
  <div class="ui container" style="margin-top: 40px; max-width: 900px;">
    
    <!-- Header -->
    <div class="ui center aligned header">
      <h1 class="ui teal header">URL Shortener Dashboard</h1>
      <h3>Create short links & track click statistics</h3>
    </div>

    <!-- Input Form -->
    <div class="ui raised very padded segment" style="margin-top: 30px;">
      <h3 class="ui dividing header">Enter URL</h3>

      <form class="ui form" @submit.prevent="shortenUrl">
        <div class="field">
          <input
            v-model="url"
            type="text"
            placeholder="Enter your long URL here..."
          />
        </div>

        <button class="ui big teal button" style="margin-top: 10px; width: 100%;">
          <i class="linkify icon"></i> Shorten URL
        </button>
      </form>
    </div>

    <!-- Result -->
    <ResultBox v-if="result" :data="result" />

    <!-- Error -->
    <div v-if="error" class="ui negative message" style="margin-top: 20px;">
      <div class="header">Error</div>
      {{ error }}
    </div>

  </div>
</template>

<script>
import axios from "axios";
import ResultBox from "../components/ResultBox.vue";

export default {
  components: { ResultBox },

  data() {
    return {
      url: "",
      result: null,
      error: null,
    };
  },

  methods: {
    async shortenUrl() {
      this.error = null;
      this.result = null;

      try {
        // 🟦 Gửi yêu cầu tạo short URL
        const res = await axios.post("http://localhost:5092/api/url/shorten", {
          originalUrl: this.url,
        });

        const code = res.data.code;

        // 🟩 Lấy thông tin short URL từ API
        const latest = await axios.get(`http://localhost:5092/api/url/${code}`);

        this.result = {
          originalUrl: latest.data.originalUrl,
          code: latest.data.code,
          shortUrl: `http://localhost:5092/${latest.data.code}`,
          createdAt: latest.data.createdAt,
          clicks: latest.data.clicks,
        };

      } catch (err) {
        console.error(err);
        this.error = "Không thể rút gọn URL. Hãy đảm bảo BE đang chạy!";
      }
    }
  }
};
</script>

<style scoped>
a {
  color: teal;
  word-break: break-all;
}
</style>
