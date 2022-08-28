import Vue from 'vue'
import axios from 'axios'
/*元件*/
import LoginComponent from './login.vue'
import SignupComponent from './signup.vue'
import AccountEdit from './account-edit.vue'
/*Vuex 共享資訊*/
import store from '../store.js'
import { mapActions } from 'vuex';

Vue.prototype.$axios = axios;

new Vue({
    el: "#app",
    store: store, 
    components: {
        AccountEdit,
        LoginComponent,
        SignupComponent,
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
})