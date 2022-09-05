<template>
    <div class="container signup">
        <h2>註冊帳號</h2>
        <form @submit.prevent="signup">
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
            <div class="mb-2">
                <div class="d-inline-flex">
                    <label class="form-label fs-5">電子郵件</label>
                    <input type="email" v-model="email" class="form-control" required />
                </div>
            </div>
            <button type="submit" class="btn btn-primary">確認</button>
            <router-link to="/Home/Login" class="link-danger">回登入頁</router-link>
        </form>
    </div>
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

<style lang="scss">
    .signup {
        .form-label {
            min-width: 100px;
        }
    }
</style>
