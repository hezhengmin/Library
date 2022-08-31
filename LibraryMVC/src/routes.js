import Login from '../src/views/Login.vue'
import Signup from '../src/views/Signup.vue'
import AccountEdit from '../src/views/AccountEdit.vue'
import Index from '../src/views/Index.vue'


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
        path: '/Account/Account_Edit/:id', // 動態路徑參數 以冒號開頭
        component: AccountEdit,
        name: 'AccountEdit'
    },
    {
        path: '/Home/Index',
        component: Index
    },
    {
        //無效網址，都導向根路徑
        path: '*',
        redirect: '/Home/Index',
    }
];