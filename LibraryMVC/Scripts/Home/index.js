import Vue from 'vue';
import axios from 'axios';

Vue.prototype.$axios = axios;

let app = new Vue({
    el: "#app",
    data: {
        num : 1
    },
    methods: {
        test() {
            this.num++;
            console.log(this.num);
        }
    }
})

console.log(app);