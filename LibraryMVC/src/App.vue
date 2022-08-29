<template>
    <div id="app">
        <login></login>
    </div>
</template>

<script>
    import Login from '../src/views/Login.vue'
    import { mapActions } from 'vuex';
    export default {
        name: "App",
        components: {
            Login
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
