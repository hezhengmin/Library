<template>
    <div id="app">
        <Navigation/>
        <router-view></router-view>
    </div>
</template>

<script>
    import Navigation from "../src/components/Navigation.vue";
    import { mapActions } from 'vuex';

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
        beforeCreate() {
           
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
        },
        mounted() {
           
           
        }
    };
</script>

<style>
</style>
