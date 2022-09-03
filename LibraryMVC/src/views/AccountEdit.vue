<template>


    <div>
        <h1>帳號名稱：{{accountId}}</h1>
        <h1>信箱編輯</h1>
        <form @submit.prevent="updateEmail">
            電子郵件：<input type="email" v-model="email" required />
            <br />
            <button type="submit">儲存</button>
            <!--{{$store.state.jwtToken}}-->
        </form>
        <h1>更改密碼</h1>
        <form @submit.prevent="updatePassword">
            舊密碼：<input type="password" v-model="oldPassword" required />
            <br />
            新密碼：<input type="password" v-model="newPassword" required />
            <br />
            確認密碼：<input type="password" v-model="confirmPassword" required />
            <br />
            <button type="submit">修改密碼</button>
        </form>
    </div>
</template>
<script>
    import { apiAccountEmailEdit, apiAccountGet } from "../api/api.js";
    export default {
        name: "AccountEdit",
        data() {
            return {
                accountId:'',
                email: '',
                oldPassword: '',
                newPassword: '',
                confirmPassword: '',
            }
        },
        methods: {
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

            //apiAccountGet(this.$route.params.id)
            //    .then((response) => {
            //        this.accountId = response.data.accountId; 
            //        this.email = response.data.email; 
            //    })
            //    .catch((error) => {
            //        console.log(error);
            //    })
        }
    };
</script>

<style>
</style>