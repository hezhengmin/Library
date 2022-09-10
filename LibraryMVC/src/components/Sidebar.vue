<template>
    <div class="sidebar border"  v-if="hasAccountInfo && hasIsLogin">
        <ul class="nav list-unstyled flex-column">
            <!-- 首頁Library -->
            <li class="nav-item navbar-brand">
                <router-link to="/Home/Index" class="nav-link">
                    Library
                </router-link>
            </li>
            <!--書籍列表-->
            <li class="nav-item">
                <router-link to="/Book/Book_Index" class="nav-link">書籍</router-link>
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
    .sidebar {
        width: 200px;
        height: 100%;

    }
</style>