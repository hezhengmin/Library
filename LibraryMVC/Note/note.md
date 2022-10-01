## 起步
```
**********************************************************************
** Visual Studio 2022 Developer PowerShell v17.2.6
**********************************************************************
PS D:\Github\Library> cd LibraryMVC
PS D:\Github\Library\LibraryMVC> npm install
```
## 執行的命令
```
**********************************************************************
** Visual Studio 2022 Developer Command Prompt v17.2.6
** Copyright (c) 2022 Microsoft Corporation
**********************************************************************

D:\Github\Library>cd librarymvc

D:\Github\Library\LibraryMVC>npm run watch
```

## 安裝套件
* npm install axios
* npm install vue@2.7.6
* npm install cross-env --save-dev
```
NODE_ENV 環境變數，要多安裝cross-env
```
* npm install fs
* npm install vue-loader@15.9.8
* npm install vue-template-compiler
* npm install vuex@3
```
vuex 紀錄 token、使用者資訊
```
* npm install vue-router@3
```
vue-router 路由
```
* npm install vuejs-paginate --save
```
A Vue.js(v2.x+) component to make pagination.
```

* npm install vue-i18n@8
```
裝vue2用的
https://vee-validate.logaretm.com/v2/guide/getting-started.html#installation
```
* npm install vue-i18n --save
```
Vue.use(VueI18n);

const i18n = new VueI18n({
  locale: 'zhTW',
});

Vue.use(VeeValidate, {
  events: 'input|blur',
  i18n,
  dictionary: {
    zhTW,
  },
});
```
* npm install vue-awesome-swiper@4.1.1
```
圖片輪播
https://github.com/surmon-china/vue-awesome-swiper
```
* npm install swiper@5.2.0

* npm install v-select2-component --save
```
下拉式選單
https://github.com/godbasin/vue-select2
```
* npm install vue2-datepicker --save
```
https://github.com/mengxiong10/vue2-datepicker
```
## 使用 Node.js npm 安裝 bootstrap
Webpack 和其他 bundlers 將 [Bootstrap](https://bootstrap5.hexschool.com/docs/5.0/getting-started/webpack/)  加入到你的專案
* npm install bootstrap --save)
* npm install @popperjs/core
```
Bootstrap 需要 Popper
https://bootstrap5.hexschool.com/docs/5.0/getting-started/webpack/
```

## 樣式
* npm install --save-dev css-loader
```
https://webpack.js.org/loaders/css-loader/
```
* npm install --save-dev style-loader
```
https://webpack.js.org/loaders/style-loader/
```
* npm install sass-loader sass webpack --save-dev
```
https://webpack.js.org/loaders/sass-loader/
```
* npm install --save-dev postcss-loader postcss
```
https://webpack.js.org/loaders/postcss-loader/
```
* npm install --save-dev autoprefixer
```
https://webpack.js.org/loaders/postcss-loader/#autoprefixer
```
## 移除套件
* npm uninstall vue-loader
* npm rm \<package name\>
## save 與 save-dev
* save：在開發、發佈時都需要依賴的套件，安裝於 dependencies (已發布環境)
* save-dev：在開發時才會依賴的套件，發佈後不需要使用，安裝於 devDependencies (開發中環境)

