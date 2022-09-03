import Vue from 'vue'
import axios from 'axios'
import Router from "vue-router";
import App from "./App.vue";
/*Vuex 共享資訊*/
import store from './store.js'
import { routes } from './routes.js' //路由規則


Vue.prototype.$axios = axios;
Vue.use(Router);//路由

const router = new Router({
    routes,
    mode: 'history' //省略井字號，不寫預設hash模式 (#)
});


//導航守衛
router.beforeEach((to, from, next) => {
    //console.log(to, from);

    //不用驗證的頁面
    const publicPages = ['/Home/Login', '/Home/SignUp'];
    //頁面是否要驗證
    const authRequired = !publicPages.includes(to.path);
    //登入是不是成功
    const loggedIn = localStorage.getItem('isLogin');

    //console.log(authRequired, typeof (loggedIn), loggedIn);


    if (authRequired && (loggedIn === null || loggedIn==='false')) {
        next('/Home/Login');
    }
    else
    {
        next();
    }
})

new Vue({
    router,
    store: store, 
    render: h => h(App)
}).$mount("#app");

