import axios from 'axios';


const domain = 'https://localhost:44323';
const token = `Bearer ${localStorage.getItem("jwtToken")}`;

// Account相關的 api
//const accountRequest = axios.create({
//    baseURL: `${domain}/api/Account/`
//});

//const bookRequest = axios.create({
//    baseURL: `${domain}/api/Book/`,
//    headers: {
//        Authorization: token
//    }
//});


//帳號Account
export const apiAccountLogin = data => createAxios().post('/Account/Login', data);
export const apiAccountSignup = data => createAxios().post(data);
//書籍Book
export const apiPostBookList = data => createAxios().post('/Book/List', data);
export const apiGetBook = (url) => createAxios().get(url);
export const apiDeleteBook = (url) => createAxios().delete(url);
export const apiPostBookExportExcel = (config) => createAxios().request(config);
//BookPhoto

//UploadFile
export const apiGetUploadFile = (config) => createAxios().request(config);
export const apiDeleteUploadFile = (url) => createAxios().delete(url);


//Loan借閱
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