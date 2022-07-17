const path = require('path');
const webpack = require('webpack');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const { VueLoaderPlugin } = require('vue-loader')

var bundleExportPath = './wwwroot/bundle/'; // 要把 bundle 過的檔案放在 ./wwwroot/bundle/ 底下


module.exports = {
    /*Entry進入哪隻檔案，可以放相對路徑*/
    entry: ['./Scripts/Account/index.js'],
    output: {
        filename: '[name].js', /*它會被 entry 中的 key 換掉*/
        path: path.resolve(__dirname, bundleExportPath),
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
                    // Compiles Sass to CSS
                    "sass-loader",
                ],

                /*
                // ./src/index.js
                使用
                import './styles/style.scss';
                */
            },
            {
                //Loading Images- 處理圖片
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                use: 'asset/resource',
            },
            {
                //Loading Fonts - 處理字型
                test: /\.(woff|woff2|eot|ttf|otf)$/i,
                use: 'asset/resource',
            },
        ],
    },
    plugins: [
        // make sure to include the plugin for the magic
        new VueLoaderPlugin()
    ],
    optimization: {
        minimizer: [new UglifyJsPlugin()],
    },
};