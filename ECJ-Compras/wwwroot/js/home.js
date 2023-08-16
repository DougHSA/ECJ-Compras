window.addEventListener('DOMContentLoaded', function () {
    const saldoGeral = document.getElementById('saldo-geral');
    const valorSaldoGeral = parseFloat(saldoGeral.textContent.replace("R$ ",""));

    if (valorSaldoGeral > 0) {
        saldoGeral.style.color = 'green';
        saldoGeral.innerHTML += ' <i class="fas fa-arrow-up" style="color: green;"></i>';
    } else {
        saldoGeral.style.color = 'red';
        saldoGeral.innerHTML += ' <i class="fas fa-arrow-down" style="color: red;"></i>';
    }

});
