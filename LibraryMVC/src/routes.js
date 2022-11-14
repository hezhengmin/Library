import Login from '../src/views/Login.vue'
import SignUp from '../src/views/SignUp.vue'
import AccountEdit from '../src/views/Account/AccountEdit.vue'
import ForgetPassword from '../src/views/ForgetPassword.vue'
import Index from '../src/views/Index.vue'
import BookIndex from '../src/views/Book/BookIndex.vue'
import BookImport from '../src/views/Book/BookImport.vue'
import BookEdit from '../src/views/Book/BookEdit.vue'
import Layout from '../src/views/Layout.vue'
import LoanIndex from '../src/views/Loan/LoanIndex.vue'
import LoanEdit from '../src/views/Loan/LoanEdit.vue'


export const routes = [
    {
        //登入頁面
        path: '/Home/Login',
        name: 'Login',
        component: Login
    },
    {
        //註冊帳號
        path: '/Home/SignUp',
        component: SignUp 
    },
    {
        //首頁
        path: '/Home',
        component: Layout,
        redirect: "/Home/Index",
        children: [
            {
                //首頁，無/接續父層路徑，/Home/Index
                path: "Index",
                name: "Index",
                component: Index
            },
            {
                //帳號編輯(更改信箱、密碼)
                path: '/Account/Account_Edit/:id',
                name: "AccountEdit",
                component: AccountEdit
            },
            {
                //書籍列表
                path: '/Book/Book_Index',
                name: "BookIndex",
                component: BookIndex
            },
            {
                //書籍匯入列表
                path: '/Book/Book_Import',
                name: "BookImport",
                component: BookImport
            },
            {
                //書籍編輯
                path: '/Book/Book_Edit/:id',
                name: 'BookEdit',
                component: BookEdit
            },
            {
                //借閱列表
                path: '/Loan/Loan_Index',
                name: "LoanIndex",
                component: LoanIndex
            },
            {
                //借閱編輯
                path: '/Loan/Loan_Edit/:id',
                name: 'LoanEdit',
                component: LoanEdit
            },
        ]
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
];


