<template>
    <div class="forgetPassword">
        <form @submit.prevent="forgetPassword" class="mt-5 p-3 rounded border border-secondary">
            <h2 class="mx-auto">忘記密碼</h2>
            <div class="mb-2">
                <div class="d-flex">
                    <label class="form-label fs-5">帳號</label>
                    <input type="text" v-model="UserId" class="form-control" required />
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
    import { apiPostAccountForgetPassword } from 'api'
    export default {
        name: "ForgetPassword",
        data() {
            return {
                UserId: '',
                email: '',
            };
        },
        methods: {
            forgetPassword() {
                apiPostAccountForgetPassword({
                    UserId: this.UserId,
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

<style lang="scss">
    .forgetPassword {
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