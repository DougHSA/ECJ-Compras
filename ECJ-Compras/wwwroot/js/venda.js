document.addEventListener("DOMContentLoaded", function () {

    fetch('/Venda/BuscarConsignados', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
        })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("Consignado");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });

        
});

document.getElementById("Consignado").onchange = function () {
    fetch('/Venda/BuscarProdutos', {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const selectElement = document.getElementById("DescricaoProduto");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item;
                option.text = item;
                selectElement.appendChild(option);
            });
        });
}

document.getElementById("DescricaoProduto").onchange = async function () {
    var preco = 0.0;
    await fetch('/Venda/BuscarPreco?produto=' + this.value, {
        method: 'POST',
        //headers: {
        //    'Content-Type': 'application/x-www-form-urlencoded',
        //    'RequestVerificationToken': '@Html.AntiForgeryToken()'
        //}
    })
        .then(response => response.json())
        .then(data => {
            const precoProduto = document.getElementById("PrecoProduto");
            precoProduto.innerText = data;
            precoProduto.value = parseFloat(data).toLocaleString('pt-BR', {
                style: 'currency',
                currency: 'BRL'
            });
            preco = parseFloat(data);
        });
    let quantidade = document.getElementById("QuantidadeProduto");
    const total = document.getElementById("Total");
    if (quantidade.value > 0) {
        const valorTotal = quantidade.value * preco;
        const valorFormatado = parseFloat(valorTotal).toLocaleString('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        });
        total.value = valorFormatado;
    }
    else {
        total.value = 'R$ 0,00';
    }
};

document.getElementById("QuantidadeProduto").onchange = function () {
    let preco = document.getElementById("PrecoProduto");
    const total = document.getElementById("Total");
    if (this.value > 0) {
        const valorTotal = this.value * parseFloat(preco.value.replace(/[^\d.,]/g, '').replace(',', '.'));
        const valorFormatado = parseFloat(valorTotal).toLocaleString('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        });
        total.value = valorFormatado;
    }
    else {
        total.value = 'R$ 0,00';
    }
};