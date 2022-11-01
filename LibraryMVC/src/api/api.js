import axios from 'axios';

const domain = 'https://localhost:44323';
const token = `Bearer ${localStorage.getItem("jwtToken")}`;

//帳號Account
export const apiAccountLogin = data => createAxios().post('/Account/Login', data);
export const apiAccountSignup = data => createAxios().post(data);
export const apiPatchAccountEmail = (url, data) => createAxios().patch(url, data);
export const apiPutAccountPassword = (data) => createAxios().put('/Account/ResetPassword', data);
export const apiGetAccount = (url, data) => createAxios().get(url, data);

//書籍Book
export const apiPostBookList = data => createAxios().post('/Book/List', data);
export const apiGetBook = (url) => createAxios().get(url);
export const apiDeleteBook = (url) => createAxios().delete(url);
export const apiPostBookExportExcel = (config) => createAxios().request(config);
export const apiPutBook = (url, data, config) => createAxios().put(url, data, config);
export const apiPostBook = (url, data, config) => createAxios().post(url, data, config);

//BookPhoto
export const apiGetBookPhoto = (url) => createAxios().get(url);
export const apiPostBookPhoto = (url, data, config) => createAxios().post(url,data, config);

//UploadFile
export const apiGetUploadFile = (config) => createAxios().request(config);
export const apiDeleteUploadFile = (url) => createAxios().delete(url);


//Loan借閱
export const apiLoanList = data => createAxios().post('/Loan/List', data);

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
                window.location = `${domain}/Home/Login`;
            }
            return Promise.reject(error);
        }
    );

    return newInstance;
}