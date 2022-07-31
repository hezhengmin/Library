<template>
    <div id="login">
        <div class="container">
            <div class="row">
                <div class="col">
                </div>
                <div class="col">
                    <div class="row bg-light mt-5 rounded">
                        <div class="col-12 pt-3">
                            <h3 class="text-center">圖書館-後臺管理系統</h3>
                            <label for="acc" class="form-label">帳號</label>
                            <input id="acc" type="text" class="form-control" v-model="account" />
                        </div>
                        <div class="col-12 pt-3">
                            <label for="pwd" class="form-label">密碼</label>
                            <input id="pwd" type="password" class="form-control" v-model="password" />
                        </div>
                        <div class="col-12 p-3">
                            <div class="d-flex justify-content-center">
                                <button type="submit" class="btn btn-primary" v-on:click="SignIn">登入</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col">
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
        methods: {
            SignIn() {

                //this.$axios.get('https://localhost:44323/api/Account')
                //    .then((response) => console.log(response)) //得出來的是類似Response的物件，裏面的data才是我們想抓的資料
                //    .catch((error) => console.log(error))

                this.$axios.post('https://localhost:44323/api/Account/SignIn',
                    {
                        accountId: this.account,
                        password: this.password
                    })
                    .then((response) => console.log(response.data)) //得出來的是類似Response的物件，裏面的data才是我們想抓的資料
                    .catch((error) => console.log(error))
            }
        }
    };
</script>

<style lang="scss">
    body {
        background: url('../../assert/account/img/signin.jpg') no-repeat;
    }
</style>