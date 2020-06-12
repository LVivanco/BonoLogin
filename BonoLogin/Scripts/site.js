const btnSubmit = document.getElementById('btnSub');
const inputTea = document.getElementById('Tea');
const inputTdea = document.getElementById('Tdea');
/*id=valTasa id=tipTasa id=fecTasa id=capTasa id=btnTea id=Tea*/
CalculoTEA('valTasa', 'tipTasa', 'fecTasa', 'capTasa', 'btnTea', 'Tea');
/*id=valTasaDes id=tipTasaDes id=fecTasaDes id=capTasaDes id=btnTeaDes id=Tdea*/
CalculoTEA('valTasaDes', 'tipTasaDes', 'fecTasaDes', 'capTasaDes', 'btnTeaDes', 'Tdea');

btnSubmit.addEventListener('click', (ev) => {
    inputTea.disabled = false;
    inputTdea.disabled = false;
});