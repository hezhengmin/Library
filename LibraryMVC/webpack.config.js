var path = require("path");
//JS壓縮成一行
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const fs = require('fs');

var appBasePath = './Scripts/'; // where the source files located
var jsEntries = {}; // listing to compile
var bundleExportPath = './wwwroot/bundle/'; // 要把 bundle 過的檔案放在 ./wwwroot/bundle/ 底下


// We search for js files inside basePath folder and make those as entries
fs.readdirSync(appBasePath).forEach(function (name) {

    // assumption: modules are located in separate directory and each module component is imported to index.js of particular module
    var indexFile = appBasePath + name + '/index.js'
    if (fs.existsSync(indexFile)) {
        jsEntries[name] = indexFile
    }
});


module.exports = {
    //Entry進入哪隻檔案，可以放相對路徑
    entry: jsEntries,
    output: {
        path: path.resolve(__dirname, bundleExportPath),
        filename: '[name].js'
    },
    // 模組的解析相關設定
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },
    optimization: {
        //minify your JavaScript
        minimizer: [new UglifyJsPlugin()],
    },
};