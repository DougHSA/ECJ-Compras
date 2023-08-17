document.addEventListener("DOMContentLoaded", function () {

    fetch('/Doacao/BuscarEquipes', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
        })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("equipePessoa");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });

        
    fetch('/Doacao/BuscarProdutos', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("categoriaProduto");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });

        
});

document.getElementById("equipePessoa").onchange = function () {
    fetch('/Doacao/BuscarNomes?equipe=' + this.value, {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("nomePessoa");
            while (selectElement.options.length > 1) {
                selectElement.remove(1);
            }
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });

};

document.getElementById("categoriaProduto").onchange =function () {
    fetch('/Doacao/BuscarUnidade?produto=' + this.value, {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const unidade = document.getElementById("UnidadeProduto");
            unidade.value = data;
        });
};