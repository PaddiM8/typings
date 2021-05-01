import {setCookie, getCookie} from "./cookieManager";

// Get cookies
//getCookie('theme') === '' ? setTheme('light') : setTheme(getCookie('theme'));

export function setTheme(_theme) {
    if (!_theme) return;
    const theme = _theme.toLowerCase();
    fetch(`/themes/${theme}.css`)
        .then(response => {
            if (response.status === 200) {
                response
                    .text()
                    .then(css => {
                        setCookie('theme', theme, 90);
                        document.querySelector('#theme').setAttribute('href', `/themes/${theme}.css`);
                        //setText();
                    })
                    .catch(err => console.error(err));
            } else {
                console.log(`theme ${theme} is undefined`);
            }
        })
        .catch(err => console.error(err));
}

// Run it once to make sure it can be reached from HTML.
setTheme();

function showAllThemes(){
    fetch(`/themes/theme-list.json`)
        .then(response => {
            if (response.status === 200) {
                response
                    .text()
                    .then(body => {
                        let themes = JSON.parse(body);
                        let keys = Object.keys(themes);
                        let i;
                        for(i = 0;i < keys.length; i ++){

                            let theme = document.createElement('div');
                            theme.setAttribute('class', 'theme-button');
                            theme.setAttribute('onClick', `setTheme('${keys[i]}')`);
                            theme.setAttribute('id', keys[i]);

                            // set tabindex to current theme index + 4 for the test page
                            theme.setAttribute('tabindex', i + 5);
                            /*theme.addEventListener('keydown', e => {
                                if (e.key === 'Enter') {
                                    setTheme(theme.id);
                                    inputField.focus();

                                }
                            })*/

                            if(themes[keys[i]]['customHTML'] !== undefined){
                                theme.style.background = themes[keys[i]]['background'];
                                theme.innerHTML = themes[keys[i]]['customHTML']
                            }else{
                                theme.textContent = keys[i];
                                theme.style.background = themes[keys[i]]['background'];
                                theme.style.color = themes[keys[i]]['color'];
                            }
                            document.getElementById('theme-area').appendChild(theme);
                        }
                    })
                    .catch(err => console.error(err));
            } else {
                console.log(`Cant find theme-list.json`);
            }
        })
        .catch(err => console.error(err));
}

// enter to open theme area
/*document.getElementById('show-themes').addEventListener('keydown', (e) => {
    if (e.key === 'Enter') {
        showThemeCenter();
        inputField.focus();
    }
});*/

export function initThemes() {
    showAllThemes();
}

export function showThemeCenter(shown) {
    if (shown) {
        document.getElementById('theme-center').classList.remove('hidden');
        document.querySelector('main').classList.add('hidden');
    } else {
        document.getElementById('theme-center').classList.add('hidden');
        document.querySelector('main').classList.remove('hidden');
    }
}

initThemes();