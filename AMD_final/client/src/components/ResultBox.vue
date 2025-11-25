<template>
  <div class="ui raised segment" style="margin-top: 20px;">
    <div class="ui top attached label">
      <i class="chart bar icon"></i> Short URL Details
    </div>

    <div class="content" style="margin-top: 20px;">
      
      <!-- Original URL -->
      <p><strong>Original URL:</strong><br />
        <a :href="detail.originalUrl" target="_blank">{{ detail.originalUrl }}</a>
      </p>

      <!-- Short URL (click để tăng click count + reload stats) -->
      <p><strong>Short URL:</strong><br />
        <a :href="detail.shortUrl" target="_blank" @click="refreshStats" style="font-weight: bold;">
          {{ detail.shortUrl }}
        </a>
      </p>

      <!-- CreatedAt -->
      <p><strong>Created At:</strong><br />
        {{ formatDate(detail.createdAt) }}
      </p>

      <!-- Total Clicks -->
      <p><strong>Total Clicks:</strong>
        <span class="ui green label">{{ detail.clicks }}</span>
      </p>

      <!-- MANUAL REFRESH BUTTON -->
      <button class="ui blue button" style="margin-top: 15px;" @click="refreshStats">
        <i class="sync icon"></i> Refresh Stats
      </button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  props: ["data"],

  data() {
    return {
      detail: this.data   // copy từ props để update realtime
    };
  },

  methods: {
    // Format ngày tháng cho đẹp
    formatDate(dt) {
      if (!dt) return "Unknown";
      const d = new Date(dt);
      return d.toLocaleString(); // format theo local time
    },

    // Reload dữ liệu từ BackEnd để cập nhật clicks + createdAt
    async refreshStats() {
      try {
        const res = await axios.get(`http://localhost:5092/api/url/${this.detail.code}`);

        // Cập nhật UI
        this.detail = {
          ...res.data,
          shortUrl: `http://localhost:5092/${res.data.code}`
        };
        
      } catch (err) {
        console.error("Error refreshing stats:", err);
      }
    }
  }
};
</script>

<style scoped>
a {
  color: #2185d0;
  word-break: break-all;
}
</style>
