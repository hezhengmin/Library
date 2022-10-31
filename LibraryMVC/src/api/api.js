import axios from 'axios';


const domain = 'https://localhost:44323';
const token = `Bearer ${localStorage.getItem("jwtToken")}`;

// Account相關的 api
const accountRequest = axios.create({
    baseURL: `${domain}/api/Account/`
});

const bookRequest = axios.create({
    baseURL: `${domain}/api/Book/`,
    headers: {
        Authorization: token
    }
});


//登入
export const apiAccountLogin = data => createAxios().post('/Account/Login', data);
//註冊
export const apiAccountSignup = data => createAxios().post(data);
//書籍
export const apiBookList = data => createAxios().post('/Book/List', data);
//借閱
export const apiLoanList = data => createAxios().post('/Loan/List', data);

//bookRequest.interceptors.response.use(
//    response => {
//        return response;
//    },
//    error => {
//        if (error.response.status === 401) {
//            console.log("401 未授權，回登入頁面", error);
//        }
//        return Promise.reject(error);
//    }
//);


const createAxios = () => {
    const newInstance = axios.create({
        baseURL: `${domain}/api`,
        headers: {
            Authorization: `Bearer ${localStorage.getItem("jwtToken")}`
        }
    });

    newInstance.interceptors.response.use(
        (config) => config,
        (error) => {
            if (error.response.status === 401) {
                console.log("401 未授權，回登入頁面", error);
                alert("請重新登入");
            }
            return Promise.reject(error);
        }
    );

    return newInstance;
}