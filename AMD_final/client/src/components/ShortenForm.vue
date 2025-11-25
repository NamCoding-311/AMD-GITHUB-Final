<template>
  <div class="ui very padded segment">
    
    <!-- URL INPUT -->
    <div class="field">
      <label>Enter URL to shorten</label>
      <div class="ui left icon input fluid">
        <input
          type="text"
          placeholder="https://example.com"
          v-model="url"
        />
        <i class="linkify icon"></i>
      </div>
    </div>

    <!-- CUSTOM CODE -->
    <div class="field" style="margin-top: 20px;">
      <label>Custom Code (optional)</label>
      <input
        type="text"
        class="ui input fluid"
        placeholder="mycustomcode"
        v-model="customCode"
      />
      <small>Minimum 3 alphabetical characters</small>
    </div>

    <!-- BUTTON -->
    <button
      class="ui primary fluid large button"
      style="margin-top: 25px;"
      @click="submitForm"
    >
      Shorten URL
    </button>

  </div>
</template>

<script setup>
import { ref } from "vue";
import axios from "axios";

const emit = defineEmits(["shortened"]);

const url = ref("");
const customCode = ref("");

// CLICK HANDLER
const submitForm = async () => {
  if (!url.value) {
    alert("Please enter a URL!");
    return;
  }

  const payload = {
    url: url.value,
    customCode: customCode.value || null,
  };

  console.log("Submitting:", payload);

  try {
    // GỌI API BE ĐÚNG CỔNG 5092
    const response = await axios.post(
      "http://localhost:5092/api/url/shorten",
      payload
    );

    console.log("Response:", response.data);

    // Emit dữ liệu lên HomePage (short URL)
    emit("shortened", response.data);

  } catch (err) {
    console.error("Error:", err);
    alert("Không thể rút gọn URL. Kiểm tra lại BE!");
  }
};
</script>
