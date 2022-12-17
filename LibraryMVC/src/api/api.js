import axios from 'axios';

const domain = 'https://localhost:44323';//WebApi
const website = 'https://localhost:44331';//前台

const token = `Bearer ${localStorage.getItem("jwtToken")}`;

//帳號登入
export const apiAccountLogin = data => createAxios().post('/Account/Login', data);
//帳號註冊
export const apiAccountSignup = data => createAxios().post('/Account', data);
export const apiPatchAccountEmail = (url, data) => createAxios().patch(url, data);
export const apiPutAccountPassword = (data) => createAxios().put('/Account/ResetPassword', data);
export const apiGetAccount = (url, data) => createAxios().get(url, data);
//忘記密碼
export const apiPostAccountForgetPassword = (data) => createAxios().post('/Account/ForgetPassword', data);
//帳號列表清單
export const apiGetAccountSelectList = () => createAxios().get('/Account/SelectList');


//書籍Book
export const apiPostBookList = data => createAxios().post('/Book/List', data);
export const apiGetBook = (url) => createAxios().get(url);
export const apiDeleteBook = (url) => createAxios().delete(url);
export const apiPostBookExportExcel = (config) => createAxios().request(config);
export const apiPutBook = (url, data, config) => createAxios().put(url, data, config);
export const apiPostBook = (url, data, config) => createAxios().post(url, data, config);
export const apiPostBooks = (data) => createAxios().post('/Book/PostBooks', data);
export const apiPostBookImportExcel = (data, config) => createAxios().post('/Book/ImportExcel', data, config);
export const apiGetBookSelectList = () => createAxios().get('/Book/SelectList');
//下載書籍匯入範本Excel
export const apiGetBookImportExcelExample = (config) => createAxios().request(config);

//書籍圖片BookPhoto
export const apiGetBookPhoto = (url) => createAxios().get(url);
export const apiPostBookPhoto = (url, data, config) => createAxios().post(url,data, config);

//上傳檔案UploadFile
export const apiGetUploadFile = (config) => createAxios().request(config);
export const apiDeleteUploadFile = (url) => createAxios().delete(url);


//借閱Loan
export const apiGetLoan = (url) => createAxios().get(url);
export const apiPostLoan = (data) => createAxios().post('/Loan', data);
export const apiPutLoan = (url, data) => createAxios().put(url, data);
export const apiPostLoanList = data => createAxios().post('/Loan/List', data);
export const apiDeleteLoan = (url) => createAxios().delete(url);
export const apiPostLoanExportExcel = (config) => createAxios().request(config);

//登入驗證碼圖片
export const apiGetValidateCode = (config) => createAxios().request(config);

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
                alert("未授權，回登入頁面");
                localStorage.clear();

                window.location = `${website}/Home/Login`;
            }
            return Promise.reject(error);
        }
    );

    return newInstance;
}