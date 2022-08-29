import Vue from 'vue'
import axios from 'axios'
import Router from "vue-router";

import App from "./App.vue";
/*元件*/

//import Signup from '../src/views/Signup.vue'
//import AccountEdit from '../src/views/AccountEdit.vue'
/*Vuex 共享資訊*/
import store from './store.js'
import { routes } from './routes.js' //路由規則
//import { mapActions } from 'vuex';

Vue.prototype.$axios = axios;
Vue.use(Router);//路由

const router = new Router({
    routes,
    mode: 'history'
});

new Vue({
    router,
    store: store, 
    render: h => h(App)
}).$mount("#app");


//new Vue({
//    el: "#app",
//    store: store, 
//    components: {
//        AccountEdit,
//        Login,
//        Signup,
//    },
//    methods: {
//        ...mapActions([
//            'fetchAccessToken'
//        ]),
//    },
//    created() {
//        //之前有登入，從localStorage設定token
//        this.fetchAccessToken();
//    }
//})