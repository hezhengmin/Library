<template>
    <div>
        <h1>帳號編輯</h1>
        <form @submit.prevent="updateEmail">
            電子郵件：<input type="email" v-model="email" required />
            <br />
            <button type="submit">儲存</button>
            {{$store.state.jwtToken}}
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
    export default {
        name: "account-edit",
        data() {
            return {
                email: '',
                oldPassword: '',
                newPassword: '',
                confirmPassword: '',
            }
        },
        methods: {
            updateEmail() {
                let id = '2309E8E9-1E85-4355-B7F1-9055891BB1B6';
                let url = `https://localhost:44323/api/Account/` + id;
                let token = this.$store.state.jwtToken;

                console.log(token);
                this.$axios.patch(url,
                        [
                            {
                                "op": "replace",
                                "path": "/email",
                                "value": this.email
                            }
                        ]
                    ,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`
                        }
                    })
                    .then((response) => {
                        console.log(response.data);
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            //更改密碼
            updatePassword() {

            }
        }

    };
</script>

<style>
</style>