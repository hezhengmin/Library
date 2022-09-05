import Login from '../src/views/Login.vue'
import SignUp from '../src/views/SignUp.vue'
import AccountEdit from '../src/views/AccountEdit.vue'
import ForgetPassword from '../src/views/ForgetPassword.vue'
import Index from '../src/views/Index.vue'
import BookIndex from '../src/views/Book/BookIndex.vue'
import BookEdit from '../src/views/Book/BookEdit.vue'




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
        //首頁
        path: '/Home/Index',
        component: Index
    },
    {
        //忘記密碼頁面
        path: '/Home/ForgetPassword',
        component: ForgetPassword
    },
    {
        //無效網址，都導向首頁
        path: '*',
        redirect: '/Home/Index',
    },
    {
        path: '/Book/Book_Index',
        component: BookIndex
    },
    {
        path: '/Book/Book_Edit/:id',
        component: BookEdit,
        name:'BookEdit'
    },
];


