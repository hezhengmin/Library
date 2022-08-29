<template>
    <ul>
        <li>
            <router-link to="/Home/Login">首頁</router-link>
        </li>
        <li>
            <router-link to="/Home/SignUp">註冊帳號</router-link>
        </li>
        <li>
            <router-link to="/Account/Account_Edit">帳號編輯</router-link>
        </li>
    </ul>
</template>

<script>
  export default {
    name: 'Navigation'
  }
</script>

<style>
</style>