document.addEventListener("DOMContentLoaded", function () {


    fetch('/Consulta/BuscarEquipes', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("equipePessoaConsulta");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });


    fetch('/Consulta/BuscarProdutos', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("categoriaProdutoConsulta");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });
});