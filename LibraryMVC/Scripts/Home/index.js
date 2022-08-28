import Vue from 'vue';
import axios from 'axios';
import LoginComponent from './login.vue';
import SignupComponent from './signup.vue';
import AccountEdit from './account-edit.vue';


Vue.prototype.$axios = axios;

new Vue({
    el: "#app",
    components: {
        AccountEdit,
        LoginComponent,
        SignupComponent,
    }
})