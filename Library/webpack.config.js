const path = require('path');
const webpack = require('webpack');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const { VueLoaderPlugin } = require('vue-loader')
const fs = require('fs');

var appBasePath = './Scripts/'; // where the source files located
var publicPath = '../bundle/'; // public path to modify asset urls. eg: '../bundle' => 'www.example.com/bundle/main.js'
var bundleExportPath = './wwwroot/bundle/'; // 要把 bundle 過的檔案放在 ./wwwroot/bundle/ 底下

var jsEntries = {}; // listing to compile

// We search for js files inside basePath folder and make those as entries
fs.readdirSync(appBasePath).forEach(function (name) {

    // assumption: modules are located in separate directory and each module component is imported to index.js of particular module
    var indexFile = appBasePath + name + '/index.js'
    if (fs.existsSync(indexFile)) {
        jsEntries[name] = indexFile
    }
});
module.exports = {
    /*Entry進入哪隻檔案，可以放相對路徑*/
    entry: jsEntries,
    output: {
        filename: '[name].js', /*它會被 entry 中的 key 換掉*/
        publicPath: publicPath,
        path: path.resolve(__dirname, bundleExportPath),
    },
    // 模組的解析相關設定
    resolve: {
        extensions: ['.js', '.vue', '.json'],
        alias: {
            'vue$': 'vue/dist/vue.esm.js',
            '@': path.join(__dirname, appBasePath),
            '@scss': path.resolve(__dirname, './scss'),
        }
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            },
            {
                test: /\.s[ac]ss$/i,
                use: [
                    // Creates `style` nodes from JS strings
                    "style-loader",
                    // Translates CSS into CommonJS
                    "css-loader",
                    {
                        loader: 'postcss-loader',
                        options: {
                            postcssOptions: {
                                // postcss plugins, can be exported to postcss.config.js
                                plugins: () => {
                                    [
                                        require('autoprefixer')
                                    ];
                                }
                            }
                        },
                    },
                    // Compiles Sass to CSS
                    "sass-loader",
                ],
            },
            {
                //Loading Fonts - 處理字型
                test: /\.(woff|woff2|eot|ttf|otf)$/i,
                use: 'asset/resource',
            },
            {
                test: /\.(png|jpe?g|gif)$/i,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '[path][name].[ext][query]',
                            esModule: false,
                        },
                    },
                ],
                type: 'javascript/auto'
            },
        ],
    },
    plugins: [
        // make sure to include the plugin for the magic
        new VueLoaderPlugin(),

        new webpack.DefinePlugin({
            'process.env.ASSET_PATH': JSON.stringify(publicPath),
        }),
    ],
    optimization: {
        minimizer: [new UglifyJsPlugin()],
    },
};