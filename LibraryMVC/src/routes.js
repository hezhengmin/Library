import Login from '../src/views/Login.vue'
import Signup from '../src/views/Signup.vue'
import AccountEdit from '../src/views/AccountEdit.vue'

export const routes = [
    {
        //登入頁面
        path: '/Home/Login',
        component: Login
    },
    {
        //註冊帳號
        path: '/Home/Signup',
        component: Signup
    },
    {
        //帳號更改頁面，信箱、密碼
        path: '/Account/Account_Edit',
        component: AccountEdit
    },
    {
        //無效網址，都導向根路徑
        path: '*',
        redirect: '/',
    }
];