import './scss/main.scss';//bootstrap
import Vue from 'vue'
import axios from 'axios'
import Router from "vue-router";
import App from "./App.vue";
import store from './store.js' //Vuex 共享資訊
import { routes } from './routes.js' //路由規則
import Paginate from 'vuejs-paginate'//分頁
import VeeValidate from 'vee-validate';//驗證
import zhTW from 'vee-validate/dist/locale/zh_TW';
import VueI18n from 'vue-i18n';
import Select2 from 'v-select2-component';//下拉選單
import DatePicker from 'vue2-datepicker';//日期選擇器
import 'vue2-datepicker/index.css';

Vue.prototype.$axios = axios;
Vue.use(Router);//路由
Vue.component('paginate', Paginate) //分頁
Vue.use(VueI18n);

Vue.component('ValidationProvider', VeeValidate.ValidationProvider);
Vue.component('ValidationObserver', VeeValidate.ValidationObserver);

Vue.component('Select2', Select2);//下拉選單
Vue.component('DatePicker', DatePicker);//日期選擇器

const i18n = new VueI18n({
    locale: 'zhTW',
});

Vue.use(VeeValidate, {
    events: 'input|blur',
    i18n,
    dictionary: {
        zhTW,
    },
});

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
    i18n, //中文化
    render: h => h(App)
}).$mount("#app");

