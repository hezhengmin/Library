<template>
    <ul>
        <li>
            <router-link to="/Home/Index">首頁</router-link>
        </li>
        <li>
            <router-link to="/Home/SignUp">註冊帳號</router-link>
        </li>
        <li>
            <router-link :to="{ 
                         name: 'AccountEdit', 
                         params: { id: primaryKeyId } 
                         }">
            帳號編輯
            </router-link>
        </li>
    </ul>
</template>

<script>
  export default {
        name: 'Navigation',
        computed: {
            primaryKeyId() {
                return this.$store.state.accountInfo.id
            }
        },
        methods: {
            
        }
  }
</script>

<style>
</style>