<template>
    <div id="app">
        <div class="d-flex">
            <div class="page-left">
                <Sidebar />
            </div>
            <div class="page-right">
                <Navigation />
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script>
    import Navigation from "../src/components/Navigation.vue";
    import Sidebar from "../src/components/Sidebar.vue";

    import { mapActions } from 'vuex';

    export default {
        name: "App",
        components: {
            Navigation,
            Sidebar
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

<style lang="scss">
    .page-left {
        min-height: 100vh;
    }
    .page-right {
        min-width: 0;
        width: 100%;
    }
</style>
