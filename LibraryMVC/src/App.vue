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
                'fetchAccessAccount'
            ]),
        },
        created() {
            //之前有登入，從localStorage設定token
            this.fetchAccessToken();
            //使用者資訊
            this.fetchAccessAccount();
        }
    };
</script>

<style>
</style>
