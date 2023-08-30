document.addEventListener("DOMContentLoaded", function () {


    fetch('/Consulta/BuscarTipos', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("tipoLancamentoConsulta");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });

    fetch('/Consulta/BuscarMetodosPagamentos', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("metodoPagamentoConsulta");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });


    fetch('/Consulta/BuscarAutores', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("autorConsulta");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });
    fetch('/Consulta/BuscarOrdenacaoLancamentos', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("ordenarLancamentos");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });

});