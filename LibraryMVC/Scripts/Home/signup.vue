<template>
    <div class="login">
        <form @submit.prevent="signup">
            帳號：<input type="text" v-model="accountId" required />
            <br />
            密碼：<input type="password" v-model="password" required />
            <br />
            信箱：<input type="email" v-model="email" required />
            <button type="submit">新增帳號</button>
        </form>
    </div>
</template>
<script>
    export default {
        name: "signup-component",
        data() {
            return {
                accountId: '',
                password: '',
                email:'',
            };
        },
        methods: {
            signup() {
                this.$axios.post('https://localhost:44323/api/Account',
                    {
                        accountId: this.accountId,
                        password: this.password,
                        email:this.email
                    })
                    .then((response) => {
                        console.log(response.data);
                        if (response.data.success) {
                            alert("註冊成功");
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