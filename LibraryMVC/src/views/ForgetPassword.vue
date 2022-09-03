<template>
    <div>
        <h2>忘記密碼</h2>
        <form @submit.prevent="forgetPassword">
            帳號：<input type="text" v-model="accountId" required />
            <br />
            信箱：<input type="email" v-model="email" required />
            <br />
            <button type="submit">確認</button>
            <router-link to="/Home/Login">回登入頁</router-link>
        </form>
    </div>
</template>
<script>
    export default {
        name: "ForgetPassword",
        data() {
            return {
                accountId: '',
                email: '',
            };
        },
        methods: {
            forgetPassword() {
                this.$axios.post('https://localhost:44323/api/Account/ForgetPassword', {
                    accountId: this.accountId,
                    email: this.email
                })
                    .then((response) => {
                        console.log(response);

                        if (response.data.success) {
                            alert("請至信箱確認新密碼");

                            //登入後回主頁
                            this.$router.push("/Home/Login");
                        }
                        else {
                            alert(response.data.errors.join('\n'));
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