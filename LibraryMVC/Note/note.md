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
## 移除套件
* npm uninstall vue-loader
* npm rm \<package name\>
## save 與 save-dev
* save：在開發、發佈時都需要依賴的套件，安裝於 dependencies (已發布環境)
* save-dev：在開發時才會依賴的套件，發佈後不需要使用，安裝於 devDependencies (開發中環境)

