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
}]