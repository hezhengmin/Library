import Vue from 'vue';
import axios from 'axios';
import LoginComponent from './login.vue';
import SignupComponent from './signup.vue';


Vue.prototype.$axios = axios;


new Vue({
    el: "#app",
    components: {
        LoginComponent,
        SignupComponent
    }
})