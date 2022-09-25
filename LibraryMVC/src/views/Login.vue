<template>
    <div class="login container">
        <form @submit.prevent="login" class="mt-5 p-3 rounded border border-secondary">
            <h2 class="mx-auto">會員登入</h2>
            <div class="mb-2">
                <div class="d-flex">
                    <label class="form-label fs-5">帳號</label>
                    <input type="text" v-model="userId" class="form-control" required />
                </div>
            </div>
            <div class="mb-2">
                <div class="d-flex">
                    <label class="form-label fs-5">密碼</label>
                    <input type="password" v-model="password" class="form-control" required />
                </div>
            </div>
            <div class="mb-2">
                <div class="d-flex">
                    <button type="submit" class="btn btn-primary me-2">登入</button>
                    <router-link to="/Home/SignUp" tag="button" class="btn btn-primary">註冊帳號</router-link>
                    <router-link to="/Home/ForgetPassword" class="link-danger ms-auto align-self-center">忘記密碼</router-link>
                </div>
            </div>
        </form>
    </div>
</template>
<script>
    export default {
        name: "Login",
        data() {
            return {
                userId: '',
                password: '',
            };
        },
        methods: {
            login() {
                this.$axios.post('https://localhost:44323/api/Account/Login', {
                    userId: this.userId,
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
                        console.log(error);
                    })
            },
        }
    };
</script>

<style lang="scss">
    .login {
        display: flex;
        /* 水平置中 */
        justify-content: center;
        /* 垂直置中 */
        align-content: center;
        flex-wrap: wrap;

        .form-label {
            min-width: 100px;
        }

        form {
            width: 400px
        }

        h2 {
            width: 140px;
        }
    }
</style>