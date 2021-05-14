import {nodeResolve} from "@rollup/plugin-node-resolve";
import commonjs from "@rollup/plugin-commonjs";

export default [{
    input: ['wwwroot/js/typeTest.js'],
    output: {
        file: 'wwwroot/dist/index.js',
        format: 'cjs'
    }
}, {
    input: 'wwwroot/js/common.js',
    output: {
        file: 'wwwroot/dist/common.js',
        format: 'cjs'
    }
}, {
    input: 'wwwroot/js/stats.js',
    output: {
        dir: 'wwwroot/dist',
        format: 'cjs'
    },
    plugins: [nodeResolve(), commonjs()],
    preserveEntrySignatures: false
}]