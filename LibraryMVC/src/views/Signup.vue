<template>
    <form @submit.prevent="signup">
        <h2>註冊帳號</h2>
        帳號：<input type="text" v-model="accountId" required />
        <br />
        密碼：<input type="password" v-model="password" required />
        <br />
        電子郵件：<input type="email" v-model="email" required />
        <br />
        <button type="submit">確認</button>
        <router-link to="/Home/Login">回登入頁</router-link>
    </form>
</template>
<script>
    export default {
        name: "SignUp",
        data() {
            return {
                accountId: '',
                password: '',
                email: '',
            };
        },
        methods: {
            signup() {
                this.$axios.post('https://localhost:44323/api/Account',
                    {
                        accountId: this.accountId,
                        password: this.password,
                        email: this.email
                    })
                    .then((response) => {

                        if (response.data.success) {
                            alert("註冊成功");

                            //回登入頁
                            this.$router.push("/Home/Login");
                        }
                        else {
                            alert(response.data.errors.join('\n'));
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
        }
    };
</script>

<style>
</style>