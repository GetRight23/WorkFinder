
var webpack = require('webpack');

module.exports = env => {

    return {
        entry: {
            index: './src/applications/index.jsx',
            photo: './src/applications/photo.jsx'
        },
        output: {
            path: __dirname + '/public/build',
            publicPath: "build/",
            filename: '[name].js',
        },
        resolveLoader: {
            moduleExtensions: ["-loader"]
        },
        module: {
            loaders: [
                {
                    test: /\.js$/,
                    loader: "babel",
                    exclude: [/node_modules/, /public/]
                },
                {
                    test: /\.css$/,
                    loaders: "style-loader!css-loader!autoprefixer-loader",
                    exclude: [/node_modules/, /public/]
                },
                {
                    test: /\.gif$/,
                    loader: "url-loader?limit=10000&mimetype=image/gif"
                },
                {
                    test: /\.jpg$/,
                    loader: "url-loader?limit=10000&mimetype=image/jpg"
                },
                {
                    test: /\.png$/,
                    loader: "url-loader?limit=10000&mimetype=image/png"
                },
                {
                    test: /\.svg$/,
                    use: [
                        {
                            loader: "babel-loader"
                        },
                        {
                            loader: "react-svg-loader",
                            options: {
                                jsx: true // true outputs JSX tags
                            }
                        }
                    ]
                },
                {
                    test: /\.jsx$/,
                    loaders: ['react-hot-loader/webpack', 'babel-loader?presets[]=react'],
                    exclude: [/node_modules/, /public/]
                },
                {
                    test: /\.json$/,
                    loader: "json-loader"
                },
                {
                    test: /\.scss$/,
                    loaders: ["style", "css", "sass"],
                    exclude: [/node_modules/, /public/]
                },
            ]
        },
        plugins: [
            new webpack.DefinePlugin({
                'process.env': {
                    'NODE_ENV': JSON.stringify('production'),
                }
            }),

            new webpack.NoEmitOnErrorsPlugin(),
            new webpack.optimize.CommonsChunkPlugin({
                children: true,
                async: true,
            }),
            new webpack.optimize.UglifyJsPlugin({
                beautify: false,
                comments: false,
                compress: {
                    sequences: true,
                    booleans: true,
                    loops: true,
                    unused: true,
                    warnings: false,
                    drop_console: false,
                    unsafe: true,
                    drop_debugger: true
                }
            }),
            new webpack.optimize.OccurrenceOrderPlugin()
        ]
    }
};
