<template>
    <div class="accountEdit">
        <h2>帳號名稱：{{accountId}}</h2>
        <div class="row">
            <div class="col-md-6">
                <h2>信箱編輯</h2>
                <form @submit.prevent="updateEmail">
                    電子郵件：<input type="email" class="form-control" v-model="email" required />
                    <br />
                    <button type="submit" class="btn btn-primary">儲存</button>
                </form>
            </div>
            <div class="col-md-6">
                <h2>更改密碼</h2>
                <form @submit.prevent="updatePassword">
                    舊密碼：<input type="password" class="form-control" v-model="oldPassword" required />
                    <br />
                    新密碼：<input type="password" class="form-control" v-model="newPassword" required />
                    <br />
                    確認密碼：<input type="password" class="form-control" v-model="confirmPassword" required />
                    <br />
                    <button type="submit" class="btn btn-primary">修改密碼</button>
                </form>
            </div>
        </div>
    </div>
</template>
<script>
    export default {
        name: "AccountEdit",
        data() {
            return {
                accountId: '',
                email: '',
                oldPassword: '',
                newPassword: '',
                confirmPassword: '',
            }
        },
        computed: {

        },
        methods: {
            //更新電子郵件
            updateEmail() {

                this.$axios.patch(`https://localhost:44323/api/Account/${this.$route.params.id}`,
                    [
                        {
                            "op": "replace",
                            "path": "/email",
                            "value": this.email
                        }
                    ])
                    .then((response) => {
                        if (response.status === 204) {
                            alert("更新成功");
                        }
                        else {
                            alert("更新失敗");
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            //更改密碼
            updatePassword() {
                let data = {
                    Id: this.$route.params.id,
                    OldPassword: this.oldPassword,
                    NewPassword: this.newPassword,
                    ConfirmPassword: this.confirmPassword,
                };
                this.$axios.put(`https://localhost:44323/api/Account/ResetPassword`, data)
                    .then((response) => {
                        if (response.status === 204) {
                            alert("更新成功");
                        }
                        else {
                            alert("更新失敗");
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            }
        },
        created() {

            this.$axios.get(`https://localhost:44323/api/Account/${this.$route.params.id}`)
                .then((response) => {
                    this.accountId = response.data.accountId;
                    this.email = response.data.email;
                })
                .catch((error) => {
                    console.log(error);
                })
        }
    }
</script>

<style>
</style>