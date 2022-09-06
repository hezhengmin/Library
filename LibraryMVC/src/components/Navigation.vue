<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light mb-2" v-if="hasAccountInfo && hasIsLogin">
        <div class="container-fluid">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <router-link to="/Home/Index" class="nav-link">首頁</router-link>
                </li>
                <!--未登入不能編輯-->
                <li class="nav-item">
                    <router-link :to="{
                         name: 'AccountEdit',
                         params: { id: primaryKeyId }
                         }" class="nav-link">
                        帳號編輯
                    </router-link>
                </li>
                <li v-if="hasAccountInfo" class="nav-item">
                    <a href="" class="nav-link" @click="signOut">登出</a>
                </li>

                <li class="nav-item">
                    <router-link to="/Book/Book_Index" class="nav-link">書籍</router-link>
                </li>
            </ul>
        </div>
    </nav>
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