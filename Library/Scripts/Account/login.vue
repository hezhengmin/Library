<template>
    <div id="login">
        <div class="row bg-light align-items-center w-100">
            <div class="col-12">
                <label for="acc" class="form-label">帳號</label>
                <input id="acc" type="text" class="form-control" v-model="account" />
            </div>
            <div class="col-12">
                <label for="pwd" class="form-label">密碼</label>
                <input id="pwd" type="password" class="form-control" v-model="password" />
            </div>
            <div class="col-12">
                <div class="d-flex justify-content-center">
                    <button type="submit" class="btn btn-primary">登入</button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    export default {
        name: "login-component",
        data() {
            return {
                account: '',
                password: '',
            };
        },
    };
</script>

<style lang="scss">
    body {
        align-items: center;
        padding-top: 40px;
        padding-bottom: 40px;
        background: url('../../assert/account/img/signin.jpg') no-repeat;
    }
</style>