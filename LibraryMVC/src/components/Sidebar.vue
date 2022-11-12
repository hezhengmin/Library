<template>
    <div class="sidebar-wrapper" v-if="hasAccountInfo && hasIsLogin">
        <div class="logo">
            <!--首頁 Library-->
            <router-link to="/Home/Index" class="simple-text">
                Library
            </router-link>
        </div>
        <ul class="nav">
            <!--書籍列表-->
            <li class="nav-item">
                <router-link to="/Book/Book_Index" class="nav-link">
                    <p>書籍列表</p>
                </router-link>
            </li>
            <!--書籍借閱-->
            <li class="nav-item">
                <router-link to="/Loan/Loan_Index" class="nav-link">
                    <p>書籍借閱</p>
                </router-link>
            </li>
            <!--未登入不能編輯-->
            <li class="nav-item">
                <router-link :to="{
                         name: 'AccountEdit',
                         params: { id: primaryKeyId }
                         }" class="nav-link">
                    <p>帳號編輯</p>
                </router-link>
            </li>
        </ul>
    </div>
</template>

<script>
    export default {
        name: 'Sidebar',
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
    }
</script>

<style lang="scss" scoped>
    
</style>