<template>
    <ul v-if="hasAccountInfo && hasIsLogin">
        <li>
            <router-link to="/Home/Index">首頁</router-link>
        </li>

        <!--未登入不能編輯-->
        <li>
            <router-link :to="{
                         name: 'AccountEdit',
                         params: { id: primaryKeyId }
                         }">
                帳號編輯
            </router-link>
        </li>
        <li v-if="hasAccountInfo">
            <a href="" @click="signOut">登出</a>
        </li>

        <li>
            <router-link to="/Book/Book_Index">書籍</router-link>
        </li>
    </ul>
</template>

<script>
  export default {
        name: 'Navigation',
        computed: {
            //帳號的主key
            primaryKeyId() {
                return this.$store.state.accountInfo.id;
            },
            hasAccountInfo() {
                return this.$store.state.accountInfo !== null;
            },
            hasIsLogin() {
                return this.$store.state.isLogin;
            }
        },
        methods: {
            signOut() {
                localStorage.clear();
            }
        }
  }
</script>

<style>
</style>