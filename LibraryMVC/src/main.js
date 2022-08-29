import Vue from 'vue'
import axios from 'axios'
import App from "./App.vue";
/*元件*/

//import Signup from '../src/views/Signup.vue'
//import AccountEdit from '../src/views/AccountEdit.vue'
/*Vuex 共享資訊*/
import store from './store.js'
//import { mapActions } from 'vuex';

Vue.prototype.$axios = axios;


new Vue({
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