<template>
  <div class="ui container" style="margin-top: 40px; max-width: 900px;">
    <h2 class="ui teal header">My Links</h2>

    <div class="ui raised very padded segment">
      <h4>Danh sách các URL đã được rút gọn</h4>

      <div v-if="list.length === 0" class="ui message">
        Chưa có URL nào được rút gọn.
      </div>

      <!-- Vòng lặp -->
      <div
        v-for="item in list"
        :key="item.code"
        class="ui segment link-item"
      >
        <div class="url-section">

          <a :href="baseUrl + '/' + item.code" target="_blank" class="short-url">
            {{ baseUrl }}/{{ item.code }}
          </a>

          <div class="original">Original: {{ item.originalUrl }}</div>
          <div class="created">Created: {{ new Date(item.createdAt).toLocaleString() }}</div>
          <div class="clicks">Clicks: {{ item.clicks }}</div>

          <!-- Nút copy + delete -->
          <div class="actions">
            <button class="ui teal button" @click="copy(item.code)">Copy</button>
            <button class="ui red button" @click="remove(item.code)">Delete</button>
          </div>

        </div>
      </div>

    </div>
  </div>
</template>

<script>
import axios from "../utils/axios";

export default {
  data() {
    return {
      list: [],
      baseUrl: "http://localhost:5092"
    };
  },

  async mounted() {
    this.loadLinks();
  },

  methods: {
    async loadLinks() {
      try {
        const res = await axios.get("/url/all");
        this.list = res.data;
      } catch (err) {
        console.error("Load lỗi:", err);
      }
    },

    copy(code) {
      navigator.clipboard.writeText(`${this.baseUrl}/${code}`);
      alert("Copied!");
    },

    async remove(code) {
      if (!confirm("Bạn chắc chắn muốn xóa?")) return;

      try {
        await axios.delete(`/url/${code}`);
        this.loadLinks();
      } catch (err) {
        console.error("Delete lỗi:", err);
      }
    }
  }
};
</script>

<style scoped>
.link-item {
  margin-bottom: 25px !important;
}

.short-url {
  font-size: 18px;
  font-weight: bold;
}

.actions {
  margin-top: 10px;
  display: flex;
  gap: 10px;
}

.original,
.created,
.clicks {
  margin-top: 3px;
}
</style>
