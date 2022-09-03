import axios from 'axios'


//WebApi domain
export const baseWebApiUrl = 'https://localhost:44323';

//console.log("store.getters.getJwtToken = ", store.getters.getJwtToken);

//setTimeout(() => {
//    console.log("setTimeout()5秒後，store.getters.getJwtToken = ", store.getters.getJwtToken);
//}, "5000")

//axios.defaults.headers.common['Authorization'] = `Bearer ${this.$store.state.jwtToken}`;

// Add a response interceptor
axios.interceptors.response.use(function (response) {
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data

    return response;
}, function (error) {
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error

    console.log("error", error);

    //未登入授權
    if (error.response.status === 401) {
        alert("未授權，請重新登入");
        localStorage.clear();
        window.location = `${baseWebApiUrl}/Home/Login`;
    }

    return Promise.reject(error);
});




const accountRequest = axios.create({
    baseURL: `${baseWebApiUrl}/api/Account`
});


//const accountAuthRequest = axios.create({
//    baseURL: `${baseWebApiUrl}/api/Account`,
//    headers: {
//        Authorization: `Bearer ${localStorage.getItem('jwtToken')}`
//    }
//});

//console.log("api.js", localStorage.getItem('jwtToken'));

//帳號登入
export const apiAccountLogin = data => accountRequest.post('/Login', data);
//編輯帳號email
export const apiAccountEmailEdit = (id, data) => accountRequest.patch(`/${id}`, data);
//取得帳號資訊
export const apiAccountGet = (id) => accountRequest.get(`/${id}`);


