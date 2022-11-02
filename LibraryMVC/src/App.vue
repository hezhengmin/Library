<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import axios from 'axios';

    export default {
        name: "App",
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
                //this.$axios.defaults.headers.common["Authorization"] = `Bearer ${this.$store.getters.getJwtToken}`;
                axios.defaults.headers.common["Authorization"] = `Bearer ${this.$store.getters.getJwtToken}`;
            }

        }
    };
</script>

<style>

</style>
