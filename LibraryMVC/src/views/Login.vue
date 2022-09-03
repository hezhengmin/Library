<template>
    <div class="login">
        <h2>帳號登入</h2>
        <form @submit.prevent="login">
            帳號：<input type="text" v-model="accountId" required />
            <br />
            密碼：<input type="password" v-model="password" required />
            <br />
            <button type="submit">登入</button>
            <router-link to="/Home/SignUp">註冊帳號</router-link>
            <router-link to="/Home/ForgetPassword">忘記密碼</router-link>

        </form>
    </div>
</template>
<script>
    import mixin from "../mixin.js";

    export default {
        name: "Login",
        mixins: [mixin],
        data() {
            return {
                accountId: '',
                password: '',
            };
        },
        methods: {
            login() {
                this.$axios.post('https://localhost:44323/api/Account/Login', {
                        accountId: this.accountId,
                        password: this.password
                    })
                    .then((response) => {

                        if (response.data.success) {
                            alert("登入成功");

                            //localStorage 存 jwtToken
                            localStorage.setItem('jwtToken', response.data.jwtToken);
                            this.$store.commit('setJwtToken', response.data.jwtToken);
                            //存User基本資訊
                            let accountInfo = JSON.stringify(response.data.account);
                            localStorage.setItem('account', accountInfo);
                            this.$store.commit('setAccountInfo', accountInfo);
                            //存是否登入
                            localStorage.setItem('isLogin', response.data.success);
                            this.$store.commit('setIsLogin', response.data.success);


                            this.$axios.defaults.headers.common['Authorization'] = `Bearer ${this.$store.state.jwtToken}`

                            //登入後回主頁
                            this.$router.push("/Home/Index");
                        }
                    })
                    .catch((error) => {
                        alert(error.response.data.errors.join('\n'));
                    })
            },
        }
    };
</script>

<style>
   
</style>