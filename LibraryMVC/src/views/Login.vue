<template>
    <div class="login container">
        <h2>帳號登入</h2>
        <form @submit.prevent="login">
            <div class="mb-2">
                <div class="d-inline-flex">
                    <label class="form-label fs-5">帳號</label>
                    <input type="text" v-model="accountId" class="form-control" required />
                </div>
            </div>
            <div class="mb-2">
                <div class="d-inline-flex">
                    <label class="form-label fs-5">密碼</label>
                    <input type="password" v-model="password" class="form-control" required />
                </div>
            </div>
            <button type="submit" class="btn btn-primary">登入</button>
            <router-link to="/Home/SignUp" tag="button" class="btn btn-primary">註冊帳號</router-link>
            <router-link to="/Home/ForgetPassword" class="link-danger">忘記密碼</router-link>
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

<style lang="scss">
    .login {
        .form-label {
            min-width: 100px;
        }
    }
</style>