import {nodeResolve} from "@rollup/plugin-node-resolve";
import commonjs from "@rollup/plugin-commonjs";
import { uglify } from "rollup-plugin-uglify";

export default [{
    input: ['wwwroot/js/typeTest.js'],
    output: {
        file: 'wwwroot/dist/index.js',
        format: 'cjs'
    },
    plugins: [(process.env.NODE_ENV == "production" && uglify())]
}, {
    input: 'wwwroot/js/common.js',
    output: {
        file: 'wwwroot/dist/common.js',
        format: 'cjs'
    },
    plugins: [(process.env.NODE_ENV == "production" && uglify())]
}, {
    input: 'wwwroot/js/stats.js',
    output: {
        dir: 'wwwroot/dist',
        format: 'cjs'
    },
    plugins: [nodeResolve(), commonjs(), (process.env.NODE_ENV == "production" && uglify())],
    preserveEntrySignatures: false
}]
