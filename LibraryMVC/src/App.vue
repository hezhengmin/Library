<template>
    <div id="app">
        <Navigation/>
        <router-view></router-view>
    </div>
</template>

<script>
    import Navigation from "../src/components/Navigation.vue";
    import { mapActions } from 'vuex';
    import axios from 'axios';

    export default {
        name: "App",
        components: {
            Navigation
        },
        methods: {
            ...mapActions([
                'fetchAccessToken',
                'fetchAccessAccount',
                'fetchAccessIsLogin' 
            ]),
        },
        created() {
            //之前有登入，從localStorage設定token
            this.fetchAccessToken();
            //使用者資訊
            this.fetchAccessAccount();
            //存取是否登入
            this.fetchAccessIsLogin();

            //axios在header附加token
            if (this.$store.getters.getJwtToken !== null) {
                this.$axios.defaults.headers.common["Authorization"] = `Bearer ${this.$store.getters.getJwtToken}`;
            }
            //console.log("store.getters.getJwtToken = ", this.$store.getters.getJwtToken);
            //axios.defaults.headers.common["Authorization"] = "Bearer " + localStorage.getItem("member_token");

            //console.log("App.vue 寫入token");
        },
        mounted() {
            // Add a response interceptor
            this.$axios.interceptors.response.use(function (response) {
                // Any status code that lie within the range of 2xx cause this function to trigger
                // Do something with response data

                return response;
            }, function (error) {
                // Any status codes that falls outside the range of 2xx cause this function to trigger
                // Do something with response error

                console.log("error", error);

                //未登入授權
                if (error.response.status === 401) {
                    alert("未授權，請重新登入");
                    //localStorage.clear();
                    //window.location = `${baseWebApiUrl}/Home/Login`;
                }

                return Promise.reject(error);
            });
        }
    };
</script>

<style>
</style>
