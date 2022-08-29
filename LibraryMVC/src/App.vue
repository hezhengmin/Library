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
                'fetchAccessToken'
            ]),
        },
        created() {
            //之前有登入，從localStorage設定token
            this.fetchAccessToken();
        }
    };
</script>

<style>
</style>
