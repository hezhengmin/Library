var path = require("path");
//JS壓縮成一行
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const fs = require('fs');
const { VueLoaderPlugin } = require('vue-loader')


var appBasePath = './Scripts/'; // where the source files located
var publicPath = '../bundle/'; // public path to modify asset urls. eg: '../bundle' => 'www.example.com/bundle/main.js'
var bundleExportPath = './wwwroot/bundle/'; // 要把 bundle 過的檔案放在 ./wwwroot/bundle/ 底下

var jsEntries = {}; // listing to compile

// We search for js files inside basePath folder and make those as entries
fs.readdirSync(appBasePath).forEach(function (name) {
    // assumption: modules are located in separate directory and each module component is imported to index.js of particular module
    var indexFile = appBasePath + name + '/index.js'
    console.log(`indexFile： ${indexFile}`);

    if (fs.existsSync(indexFile)) {
        jsEntries[name] = indexFile
    }
});
console.log(`jsEntries： ${JSON.stringify(jsEntries)}`);

module.exports = {
    //Entry進入哪隻檔案，可以放相對路徑
    entry: jsEntries,
    output: {
        filename: '[name].js',
        publicPath: publicPath,
        path: path.resolve(__dirname, bundleExportPath),
    },
    // 模組的解析相關設定
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            },
        ]
    },
    plugins: [
        //解析.vue 元件檔
        new VueLoaderPlugin(),
    ],
    optimization: {
        //minify your JavaScript
        minimizer: [new UglifyJsPlugin()],
    },
};