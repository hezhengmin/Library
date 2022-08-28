<template>
    <div>
        <h1>帳號編輯</h1>
        <form @submit.prevent="updateEmail">
            電子郵件：<input type="email" v-model="email" required />
            <br />
            <button type="submit">儲存</button>
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

            },
            //更改密碼
            updatePassword() {

            }
        }

    };
</script>

<style>
</style>