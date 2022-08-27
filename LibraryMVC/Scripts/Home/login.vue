<template>
    <div class="login">
        <form @submit.prevent="login">
            帳號：<input type="text" v-model="accountId" required />
            <br />
            密碼：<input type="password" v-model="password" required />
            <br />
            <button type="submit">登入</button>
        </form>
    </div>
</template>
<script>
    export default {
        name: "login-component",
        data() {
            return {
                accountId: '',
                password: '',

            };
        },
        methods: {
            login() {
                console.log(`accountId ${this.accountId} password ${this.password}`);
                this.$axios.post('https://localhost:44323/api/Account/Login',
                    {
                        accountId: this.accountId,
                        password: this.password
                    })
                    .then((response) => {
                        return response.data;
                    }) 
                    .then((data) => {
                        if (data.success) {
                           console.log(data);
                        }
                        else {
                            alert("帳號或密碼有錯");
                        }
                    }) 
                    .catch((error) => console.log(error))
            }
        }
    };
</script>

<style>
   
</style>