var path = require("path");

console.log("路徑", path);

module.exports = {
    //Entry進入哪隻檔案，可以放相對路徑
    entry: './Scripts/login.js',
    output: {
        path: path.resolve(__dirname, './wwwroot/bundle/'),
        filename: 'login.js'
    },
    // 模組的解析相關設定
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },
};