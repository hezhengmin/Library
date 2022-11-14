<template>
    <div class="accountEdit">
        <div class="card">
            <div class="card-header">
                <h2 class="card-title">帳號編輯</h2>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="p-3 border rounded">
                            <h4>信箱編輯</h4>
                            <form @submit.prevent="updateEmail">
                                電子郵件：<input type="email" class="form-control" v-model="email" required />
                                <br />
                                <button type="submit" class="btn btn-primary btn-fill">儲存</button>
                            </form>
                        </div>
                    </div>

                    <div class="col-md-6 ">
                        <div class="p-3 border rounded">
                            <h4>更改密碼</h4>
                            <form @submit.prevent="updatePassword">
                                舊密碼：<input type="password" class="form-control" v-model="oldPassword" required />
                                <br />
                                新密碼：<input type="password" class="form-control" v-model="newPassword" required />
                                <br />
                                確認密碼：<input type="password" class="form-control" v-model="confirmPassword" required />
                                <br />
                                <button type="submit" class="btn btn-primary btn-fill">修改密碼</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import { apiPatchAccountEmail, apiPutAccountPassword, apiGetAccount } from 'api'
    
    export default {
        name: "AccountEdit",
        data() {
            return {
                UserId: '',
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
                apiPatchAccountEmail(`/Account/${this.$route.params.id}`,
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
                apiPutAccountPassword(data)
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

            apiGetAccount(`/Account/${this.$route.params.id}`)
                .then((response) => {
                    this.UserId = response.data.UserId;
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