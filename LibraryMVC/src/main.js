import './scss/main.scss';//bootstrap
import Vue from 'vue'
import axios from 'axios'
import Router from "vue-router";
import App from "./App.vue";
import store from './store.js' //Vuex 共享資訊
import { routes } from './routes.js' //路由規則
import Paginate from 'vuejs-paginate'



Vue.prototype.$axios = axios;
Vue.use(Router);//路由
Vue.component('paginate', Paginate) //分頁

const router = new Router({
    routes,
    mode: 'history' //省略井字號，不寫預設hash模式 (#)
});


//導航守衛
router.beforeEach((to, from, next) => {
    //不用驗證的頁面
    const publicPages = ['/Home/Login', '/Home/SignUp', '/Home/ForgetPassword'];
    //頁面是否要驗證
    const authRequired = !publicPages.includes(to.path);
    //登入是不是成功
    const loggedIn = localStorage.getItem('isLogin');

    if (authRequired && (loggedIn === null || loggedIn==='false')) {
        next('/Home/Login');
    }
    else
    {
        next();
    }
})


//攔截器
axios.interceptors.response.use(function (response) {
    return response;
}, function (error) {
    //未授權，回登入頁面
    if (error.response.status === 401) {
        localStorage.clear();
        router.push({ name: "Login" });
        store.commit('setIsLogin', false);
    }
    return Promise.reject(error);
});

new Vue({
    router,
    store: store, 
    render: h => h(App)
}).$mount("#app");

