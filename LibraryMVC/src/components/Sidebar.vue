<template>
    <div class="sidebar-wrapper" v-if="hasAccountInfo && hasIsLogin">
        <div class="logo" @click ="active($event.target)">
            <!--首頁 Library-->
            <router-link to="/Home/Index" class="simple-text">
                Library
            </router-link>
        </div>
        <ul class="nav" ref="nav">
            <!--書籍列表-->
            <li class="nav-item" @click ="active($event.target)">
                <router-link to="/Book/Book_Index" class="nav-link">
                    <p>書籍列表</p>
                </router-link>
            </li>
            <!--書籍借閱-->
            <li class="nav-item"  @click ="active($event.target)">
                <router-link to="/Loan/Loan_Index" class="nav-link">
                    <p>書籍借閱</p>
                </router-link>
            </li>
            <!--未登入不能編輯-->
            <li class="nav-item"  @click ="active($event.target)">
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
        methods: {
            active(element) {
                //先移除全部的active
                let liList = this.$refs.nav.querySelectorAll('li'); +
                Array.from(liList).forEach(li => li.classList.remove('active'));
                //點選
                element.parentNode.classList.add("active");
            }
        },
        created() {
            console.log("現在路徑" ,this.$router.currentRoute);
        }

    }
</script>

<style lang="scss" scoped>
    
</style>