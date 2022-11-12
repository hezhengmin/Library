<template>
    <div class="container signup">
        <form @submit.prevent="signup" class="mt-5 p-3 rounded border border-secondary">
            <h2 class="mx-auto">註冊帳號</h2>
            <div class="mb-2">
                <div class="d-flex">
                    <label class="form-label fs-5">帳號</label>
                    <input type="text" v-model="UserId" class="form-control" required />
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
                    <label class="form-label fs-5">電子郵件</label>
                    <input type="email" v-model="email" class="form-control" required />
                </div>
            </div>
            <div class="mb-2">
                <div class="d-flex">
                    <button type="submit" class="btn btn-primary me-2 btn-fill">確認</button>
                    <router-link to="/Home/Login" class="link-danger ms-auto align-self-center">回登入頁</router-link>
                </div>
            </div>
        </form>
    </div>
</template>
<script>
    import { apiAccountSignup } from 'api'

    export default {
        name: "SignUp",
        data() {
            return {
                UserId: '',
                password: '',
                email: '',
            };
        },
        methods: {
            signup() {
                apiAccountSignup(
                    {
                        UserId: this.UserId,
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
