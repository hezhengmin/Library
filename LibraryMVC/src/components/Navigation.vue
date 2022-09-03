<template>
    <ul v-if="hasAccountInfo">
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
    </ul>
</template>

<script>
  export default {
        name: 'Navigation',
        computed: {
            //帳號的主key
            primaryKeyId() {
                return this.$store.state.accountInfo.id
            },
            hasAccountInfo() {
                return this.$store.state.accountInfo !== null;
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