<template>
    <div class="login">
        <form @submit.prevent="login">
            帳號：<input type="text" v-model="accountId" required />
            <br />
            密碼：<input type="password" v-model="password" required />
            <br />
            <button type="submit">登入</button>
        </form>
        <button type="button" @click="addAccount">新增帳號</button>
        <hr />
        <button type="button" @click="addNum">num++</button>
        {{num}}

        <button type="button" @click="increment">increment</button>
        {{$store.state.count}}
    </div>
</template>
<script>
    import mixin from "../mixin.js";

    export default {
        name: "Login",
        mixins: [mixin],
        data() {
            return {
                accountId: '',
                password: '',
            };
        },
        methods: {
            login() {
                //console.log(`accountId ${this.accountId} password ${this.password}`);
                this.$axios.post('https://localhost:44323/api/Account/Login',
                    {
                        accountId: this.accountId,
                        password: this.password
                    })
                    .then((response) => {
                        console.log(response.data);
                        if (response.data.success) {
                            alert("登入成功");

                            //localStorage 存 jwtToken
                            localStorage.setItem('jwtToken', response.data.jwtToken);
                            this.$store.commit('setJwtToken', response.data.jwtToken);
                            //存User基本資訊
                            let accountInfo = JSON.stringify(response.data.account);
                            localStorage.setItem('account', accountInfo);
                            this.$store.commit('setAccountInfo', accountInfo);
                            //存是否登入
                            localStorage.setItem('isLogin', response.data.success);
                            this.$store.commit('setIsLogin', response.data.success);


                            //登入後回主頁
                            this.$router.push("/Home/Index");

                            //this.$router.push({ name: 'AccountEdit', params: { id: response.data.account.id } })
                        }
                    })
                    .catch((error) => {
                        alert(error.response.data.errors.join('\n'));
                    })
            },
            //新增帳密
            addAccount() {
                
            },
            increment() {
                this.$store.commit('increment')
                console.log(this.$store.state.count)
            }
        }
    };
</script>

<style>
   
</style>