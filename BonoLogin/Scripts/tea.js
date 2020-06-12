function CalculoTEA(id_tasa, id_tipoTasa, id_frecTasa, id_frecCap, id_btnCal, id_resultTEA) {
    const tasa_ingresada = document.getElementById(id_tasa);
    const tipoTasa = document.getElementById(id_tipoTasa);
    const frecTasa = document.getElementById(id_frecTasa);
    const capi = document.getElementById(id_frecCap);
    const btnTEA = document.getElementById(id_btnCal);
    const result_Tea = document.getElementById(id_resultTEA);

    result_Tea.disabled = true;

    var tipo = tipoTasa.value;

    tipoTasa.addEventListener('change', (ev) => {
        tipo = tipoTasa.value;
        if (tipo == "nominal") {
            capi.disabled = false;
        }
        if (tipo == "efectiva") {
            capi.disabled = true;
        }
    });

    function diasFrecuencia(periodos) {
        let d = 0;
        switch (periodos) {
            case "diaria":
                d = 1;
                break;
            case "semanal":
                d = 7;
                break;
            case "quincenal":
                d = 15;
                break;
            case "mensual":
                d = 30;
                break;
            case "bimestral":
                d = 60;
                break;
            case "trimestral":
                d = 90;
                break;
            case "semestral":
                d = 180;
                break;
            case "anual":
                d = 360;
                break;
            default:
                d = -1;
                break;
        }
        return d;
    }

    function CalcularTea(tasa, tipTasa, periodoTasa, periodoCap) {
        /*Obtiene la tasa en formato ##% y luego lo calcula en decimal*/
        let tea;
        let t = Number(tasa) / 100;
        let n;
        let dpt;
        let dpc;
        dpt = Number(diasFrecuencia(periodoTasa));
        dpc = Number(diasFrecuencia(periodoCap));
        switch (tipTasa) {
            case "nominal":
                n = dpt / dpc;
                tea = Math.pow((1 + (t / 360)), n);
                tea = tea - 1;
                break;
            case "efectiva":
                console.log(t + 1);
                tea = Math.pow((1 + t), (360 / dpt));
                console.log(tea);
                tea = tea - 1;
                console.log(tea);
                break;
            default:
                tea = -1;
                break;
        }
        /*Esta tea esta en decimal ##.###*/
        return tea;
    }

    btnTEA.addEventListener('click', (ev) => {
        let val = CalcularTea(tasa_ingresada.value, tipoTasa.value, frecTasa.value, capi.value);
        result_Tea.value = val * 100;
    });
}

//console.log("Aqui ESTOY!!!!");