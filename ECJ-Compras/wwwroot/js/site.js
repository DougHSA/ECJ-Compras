// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.addEventListener('load', function () {
    const loadingModal = document.getElementById('loadingModal');
    loadingModal.style.display = 'none';
});

document.addEventListener('DOMContentLoaded', function () {
    const loadingModal = document.getElementById('loadingModal');
    loadingModal.style.display = 'block';
});

function logout() {
    fetch('/Auth/Logout', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => {
            if (response.ok) {
                window.location.href = response.url;
            } else {
                console.error('Erro ao fazer logout');
            }
        })
        .catch(error => {
            console.error('Erro ao fazer logout:', error);
        });
}