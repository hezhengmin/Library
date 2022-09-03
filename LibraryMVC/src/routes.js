import Login from '../src/views/Login.vue'
import SignUp from '../src/views/SignUp.vue'
import AccountEdit from '../src/views/AccountEdit.vue'
import ForgetPassword from '../src/views/ForgetPassword.vue'
import Index from '../src/views/Index.vue'


export const routes = [
    {
        //登入頁面
        path: '/Home/Login',
        component: Login,
        name : 'Login'
    },
    {
        //註冊帳號
        path: '/Home/SignUp',
        component: SignUp 
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
        path: '/Home/ForgetPassword',
        component: ForgetPassword
    },
    {
        //無效網址，都導向首頁
        path: '*',
        redirect: '/Home/Index',
    }
];


