using BonoLogin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BonoLogin.Services
{
    public class BonoService
    {
        private CalculoBono calculo = new CalculoBono();

        public void createTypes()
        {
            using (var db = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var userId = userManager.FindByEmail("emisor@emisor.com").Id;

                var resultado = roleManager.Create(new IdentityRole("Emisor"));
                if (userId != null)
                {
                    resultado = userManager.AddToRole(userId, "Emisor");

                    DatosEmisor de1 = new DatosEmisor()
                    {
                        Alias = "Default",
                        Ianual = 0.0,
                        Ir = 30.0,
                        Pprima = 1.0,
                        PCavali = 0.5,
                        Pcolocacion = 0.5,
                        Pestructuracion = 0.5,
                        Pflotacion = 0.45,
                        UserId = userId,
                    };

                    DatosEmisor de2 = new DatosEmisor()
                    {
                        Alias = "Default2",
                        Ianual = 0.0,
                        Ir = 30.0,
                        Pprima = 1.0,
                        PCavali = 0.5,
                        Pcolocacion = 0.25,
                        Pestructuracion = 0.45,
                        Pflotacion = 0.15,
                        UserId = userId,
                    };

                    db.DatosEmisor.Add(de1);
                    db.DatosEmisor.Add(de2);
                    db.SaveChanges();
                }

            }
        }


        public List<string> stringToList(string cadena)
        {
            return cadena.Split('|').ToList();
        }

        public string listToString(List<string> lst)
        {
            return string.Join("|", lst.ToArray());
        }

        public List<string> listStringsTwoDecimals(List<double> lst)
        {
            List<string> list = new List<string>();
            List<double> numbers = new List<double>();
            foreach (double n in lst)
            {
                numbers.Add(Math.Round(n, 2));
            }
            foreach (double n in numbers)
            {
                list.Add(n.ToString());
            }
            return list;
        }

        public double nDecimals(double number, int d)
        {
            return Math.Round(number, d);
        }

        public ResultadoBono GetResultado(int ficha_id)
        {
            ResultadoBono rb = null;
            using (var db = new ApplicationDbContext())
            {
                rb = db.ResultadoBono.Where(res => res.DatosBonoId == ficha_id).First();
            }
            return rb;
        }

        public void CreateResult(int id_ficha, bool createOrEdit)
        {
            DatosBono datosBono = null;
            List<PGracia> lstPGracia = null;
            ResultadoBono resultBono = null;
            using (var db = new ApplicationDbContext())
            {
                datosBono = db.DatosBono.Find(id_ficha);
                lstPGracia = db.PGracia.Where(pg => pg.DatosBonoId == id_ficha).ToList();
            }
            double cie = calculo.CostoInicialEmisor(datosBono.ValComercial, datosBono.Pestructuracion / 100, datosBono.Pcolocacion / 100, datosBono.Pflotacion / 100, datosBono.PCavali / 100);
            double cib = calculo.CostoInicialBonista(datosBono.ValComercial, datosBono.Pflotacion / 100, datosBono.PCavali / 100);
            int numeroPeriodos = calculo.NumeroPeriodos(datosBono.TipoAno, datosBono.FrecPago, datosBono.AnosVida);
            int diasXPeriodo = calculo.DiasXPeriodo(datosBono.FrecPago);
            int diasXAnio = calculo.DiasAnio(datosBono.TipoAno);
            double tea = datosBono.Tea / 100;
            double tdea = datosBono.Tdea / 100;
            double tep = calculo.TasaDelPeriodo(tea, diasXPeriodo, diasXAnio);
            double tedp = calculo.TasaDelPeriodo(tdea, diasXPeriodo, diasXAnio);
            double ip = calculo.InflacionXPeriodo(datosBono.Ianual / 100, diasXPeriodo, diasXAnio);
            int n = 0;
            //Listas
            List<double> bono = new List<double>();
            List<double> bonoIndexado = new List<double>();
            List<double> cupon = new List<double>();
            List<double> cuota = new List<double>();
            List<double> amortizacion = new List<double>();
            List<double> prima = new List<double>();
            List<double> escudo = new List<double>();
            List<double> flujoEmisor = new List<double>();
            List<double> flujoEmisorEscudo = new List<double>();
            List<double> flujoBonista = new List<double>();
            List<DateTime> flujoFechas = new List<DateTime>();
            //Objetos a añadir a los flujos
            double eBono = 0.0;
            double eBonoIndexado = 0.0;
            double eCupon = 0.0;
            double eCuota = 0.0;
            double eAmortizacion = 0.0;
            double ePrima = 0.0;
            double eEscudo = 0.0;
            double eFEmisor = 0.0;
            double eFEmisorEscudo = 0.0;
            double eFBonista = 0.0;
            //Primer flujo 0
            flujoFechas.Add(datosBono.fechaEmision);

            bono.Add(eBono);
            bonoIndexado.Add(eBonoIndexado);
            cupon.Add(eCupon);
            cuota.Add(eCuota);
            amortizacion.Add(eAmortizacion);
            prima.Add(ePrima);
            escudo.Add(eEscudo);

            eFEmisor = calculo.FlujoEmisor(n, numeroPeriodos, datosBono.ValComercial, cie, cuota[n], prima[n]);
            flujoEmisor.Add(eFEmisor);
            eFEmisorEscudo = calculo.FlujoEmisorEscudo(n, flujoEmisor[n], escudo[n]);
            flujoEmisorEscudo.Add(eFEmisorEscudo);
            eFBonista = calculo.FlujoBonista(n, datosBono.ValComercial, cib, flujoEmisor[n]);
            flujoBonista.Add(eFBonista);

            //Flujo
            for (int i = 1; i <= numeroPeriodos; i++)
            {

                flujoFechas.Add(flujoFechas[i - 1].AddDays(diasXPeriodo));

                if (i - 2 < 0)
                {
                    eBono = calculo.Bono(i, datosBono.ValNominal, numeroPeriodos, "S", bonoIndexado[i - 1], cupon[i - 1], amortizacion[i - 1]);
                    bono.Add(eBono);
                }
                else
                {
                    eBono = calculo.Bono(i, datosBono.ValNominal, numeroPeriodos, lstPGracia[i - 2].Tipo, bonoIndexado[i - 1], cupon[i - 1], amortizacion[i - 1]);
                    bono.Add(eBono);
                }

                eBonoIndexado = calculo.BonoIndexado(bono[i], ip);
                bonoIndexado.Add(eBonoIndexado);
                eCupon = calculo.Cupon(tep, bonoIndexado[i]);
                cupon.Add(eCupon);
                if (datosBono.Metodo == "aleman" || datosBono.Metodo == "americano")
                {
                    eAmortizacion = calculo.Amortizacion(i, numeroPeriodos, lstPGracia[i - 1].Tipo, bonoIndexado[i], datosBono.Metodo, 0, cupon[i]);
                    amortizacion.Add(eAmortizacion);
                    eCuota = calculo.Cuota(i, numeroPeriodos, lstPGracia[i - 1].Tipo, cupon[i], amortizacion[i], datosBono.Metodo, datosBono.ValNominal, tep);
                    cuota.Add(eCuota);
                }
                else
                {
                    eCuota = calculo.Cuota(i, numeroPeriodos, lstPGracia[i - 1].Tipo, cupon[i], 0, datosBono.Metodo, datosBono.ValNominal, tep);
                    cuota.Add(eCuota);
                    eAmortizacion = calculo.Amortizacion(i, numeroPeriodos, lstPGracia[i - 1].Tipo, bonoIndexado[i], datosBono.Metodo, cuota[i], cupon[i]);
                    amortizacion.Add(eAmortizacion);
                }
                ePrima = calculo.Prima(i, numeroPeriodos, datosBono.Pprima / 100, bonoIndexado[i]);
                prima.Add(ePrima);
                eEscudo = calculo.Escudo(cupon[i], datosBono.Ir / 100);
                escudo.Add(eEscudo);

                //Flujos Emisor y bonista
                eFEmisor = calculo.FlujoEmisor(i, numeroPeriodos, datosBono.ValComercial, cie, cuota[i], prima[i]);
                flujoEmisor.Add(eFEmisor);
                eFEmisorEscudo = calculo.FlujoEmisorEscudo(i, flujoEmisor[i], escudo[i]);
                flujoEmisorEscudo.Add(eFEmisorEscudo);
                eFBonista = calculo.FlujoBonista(i, datosBono.ValComercial, cib, flujoEmisor[i]);
                flujoBonista.Add(eFBonista);
            }

            //Las fechas se pasan a una lista de strings
            List<string> listaFlujoFechas = new List<string>();
            foreach (var x in flujoFechas)
            {
                listaFlujoFechas.Add(x.ToShortDateString());
            }

            //Añadiremos porsiacaso se quiera acceder a los resultados

            //Resultados
            double vaBonista = nDecimals(calculo.ValorActual(flujoBonista.ToArray(), tedp, numeroPeriodos), 2);
            double vanBonista = nDecimals(calculo.VANeto(flujoBonista.ToArray(), vaBonista), 2);
            double tirBonista = nDecimals(calculo.Tir(flujoBonista.ToArray()) * 100, 7);
            double tceaBonista = nDecimals(calculo.Tcea(tirBonista / 100, diasXAnio, diasXPeriodo) * 100, 7);
            double tirEmisor = nDecimals(calculo.Tir(flujoEmisor.ToArray()) * 100, 7);
            double tceaEmisor = nDecimals(calculo.Tcea(tirEmisor / 100, diasXAnio, diasXPeriodo) * 100, 7);
            double tasaPeriodo = nDecimals(tep * 100, 7);
            double cokPeriodo = nDecimals(tedp * 100, 7);

            //Cadenas de flujos
            string cadenaBono = listToString(listStringsTwoDecimals(bono));
            string cadenaBonoIndexado = listToString(listStringsTwoDecimals(bonoIndexado));
            string cadenaCupon = listToString(listStringsTwoDecimals(cupon));
            string cadenaCuota = listToString(listStringsTwoDecimals(cuota));
            string cadenaAmortizacion = listToString(listStringsTwoDecimals(amortizacion));
            string cadenaPrima = listToString(listStringsTwoDecimals(prima));
            string cadenaEscudo = listToString(listStringsTwoDecimals(escudo));
            string cadenaFlujoEmisor = listToString(listStringsTwoDecimals(flujoEmisor));
            string cadenaFlujoEmisorEscudo = listToString(listStringsTwoDecimals(flujoEmisorEscudo));
            string cadenaBonista = listToString(listStringsTwoDecimals(flujoBonista));
            string cadenaFlujoFechas = listToString(listaFlujoFechas);

            if (createOrEdit)
            {
                //Añadiendo el resultado a la base de datos
                resultBono = new ResultadoBono()
                {
                    DatosBonoId = id_ficha,
                    TirBonista = tirBonista,
                    TceaBonista = tceaBonista,
                    TirEmisor = tirEmisor,
                    TceaEmisor = tceaEmisor,
                    Va = vaBonista,
                    Van = vanBonista,
                    Bono = cadenaBono,
                    BonoIndexado = cadenaBonoIndexado,
                    Cupon = cadenaCupon,
                    Cuota = cadenaCuota,
                    Amortizacion = cadenaAmortizacion,
                    Prima = cadenaPrima,
                    Escudo = cadenaEscudo,
                    FlujoEmisor = cadenaFlujoEmisor,
                    FlujoEmisorEsc = cadenaFlujoEmisorEscudo,
                    FlujoBonista = cadenaBonista,
                    FlujoFechas = cadenaFlujoFechas,
                    Ip = ip,
                    Tep = tasaPeriodo,
                    Tdep = cokPeriodo,
                };
                using (var db = new ApplicationDbContext())
                {
                    db.ResultadoBono.Add(resultBono);
                    db.SaveChanges();
                }

                //Fin de añadir
            }
            else
            {
                //Actualizar Resultado
                using (var db = new ApplicationDbContext())
                {
                    resultBono = db.ResultadoBono.Where(res => res.DatosBonoId == id_ficha).First();
                    resultBono.Va = vaBonista;
                    resultBono.Van = vanBonista;
                    resultBono.TirBonista = tirBonista;
                    resultBono.TceaBonista = tceaBonista;
                    resultBono.TirEmisor = tirEmisor;
                    resultBono.TceaEmisor = tceaEmisor;
                    resultBono.Bono = cadenaBono;
                    resultBono.BonoIndexado = cadenaBonoIndexado;
                    resultBono.Cupon = cadenaCupon;
                    resultBono.Cuota = cadenaCuota;
                    resultBono.Amortizacion = cadenaAmortizacion;
                    resultBono.Prima = cadenaPrima;
                    resultBono.Escudo = cadenaEscudo;
                    resultBono.FlujoEmisor = cadenaFlujoEmisor;
                    resultBono.FlujoEmisorEsc = cadenaFlujoEmisorEscudo;
                    resultBono.FlujoBonista = cadenaBonista;
                    resultBono.FlujoFechas = cadenaFlujoFechas;
                    resultBono.Ip = ip;
                    resultBono.Tep = tasaPeriodo;
                    resultBono.Tdep = cokPeriodo;
                    db.SaveChanges();

                }

                //Fin de actualizar
            }


        }
        public List<PGracia> ListPGracia(int id_ficha)
        {
            List<PGracia> lst = null;
            using (var db = new ApplicationDbContext())
            {
                lst = db.PGracia.Where(pg => pg.DatosBonoId == id_ficha).ToList();
            }
            return lst;
        }
        public void EditPG(int id_pg, string tipo)
        {
            PGracia pg = null;
            using (var db = new ApplicationDbContext())
            {
                pg = db.PGracia.Find(id_pg);
                pg.Tipo = tipo;
                db.SaveChanges();
            }
        }
        public void DeletePG(int id_ficha)
        {
            List<PGracia> lst = null;
            using (var db = new ApplicationDbContext())
            {
                lst = db.PGracia.Where(pg => pg.DatosBonoId == id_ficha).ToList();
                foreach (var pg in lst)
                {
                    db.PGracia.Remove(pg);
                    db.SaveChanges();
                }
            }
        }
        public void CreatePG(int id_ficha, int numeroPeriodos)
        {
            PGracia pg = null;
            for (int i = 1; i <= numeroPeriodos; i++)
            {
                pg = new PGracia()
                {
                    DatosBonoId = id_ficha,
                    Periodo = i,
                    Tipo = "S"
                };
                using (var db = new ApplicationDbContext())
                {
                    db.PGracia.Add(pg);
                    db.SaveChanges();
                }
            }
        }

        public void CreateFicha(DatosBono ficha)
        {
            using (var db = new ApplicationDbContext())
            {
                db.DatosBono.Add(ficha);
                db.SaveChanges();
            }
        }

        public List<DatosBono> ObtenerListaFichas(string id_user)
        {
            List<DatosBono> fichas;

            using (var db = new ApplicationDbContext())
            {
                fichas = db.DatosBono.Where(f => f.UserId == id_user).ToList();
            }

            return fichas;
        }

        public List<SelectListItem> Metodos()
        {
            List<SelectListItem> lst = new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = "Americano",
                    Value = "americano"
                },
                new SelectListItem() {
                    Text = "Frances",
                    Value = "frances"
                },
                new SelectListItem() {
                    Text = "Aleman",
                    Value = "aleman"
                }
            };
            return lst;
        }

        public List<SelectListItem> Frecuencias()
        {
            List<SelectListItem> lst = new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = "Diaria",
                    Value = "diaria"
                },
                new SelectListItem() {
                    Text = "Quincenal",
                    Value = "quincenal"
                },
                new SelectListItem() {
                    Text = "Mensual",
                    Value = "mensual"
                },
                new SelectListItem() {
                    Text = "Bimestral",
                    Value = "bimestral"
                },
                new SelectListItem() {
                    Text = "Trimestral",
                    Value = "trimestral"
                },
                new SelectListItem() {
                    Text = "Semestral",
                    Value = "semestral"
                },
                new SelectListItem() {
                    Text = "Anual",
                    Value = "anual"
                }
            };
            return lst;
        }

        public List<SelectListItem> TipoAnios()
        {
            List<SelectListItem> lst = new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = "Ordinario",
                    Value = "ordinario"
                },
                new SelectListItem() {
                    Text = "Exacto",
                    Value = "exacto"
                },
            };
            return lst;
        }

        public List<SelectListItem> TiposPG()
        {
            List<SelectListItem> lst = new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = "Simple",
                    Value = "S"
                },
                new SelectListItem() {
                    Text = "Parcial",
                    Value = "P"
                },
                new SelectListItem() {
                    Text = "Total",
                    Value = "T"
                }
            };
            return lst;
        }

        public List<SelectListItem> TiposTasa()
        {
            List<SelectListItem> lst = new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = "Nominal",
                    Value = "nominal"
                },
                new SelectListItem() {
                    Text = "Efectiva",
                    Value = "efectiva"
                }
            };
            return lst;
        }

        public List<SelectListItem> TiposMoneda()
        {
            List<SelectListItem> lst = new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = "S/",
                    Value = "Soles"
                },
                new SelectListItem() {
                    Text = "$",
                    Value = "Dollar"
                },
                new SelectListItem() {
                    Text = "€",
                    Value = "Euro"
                }
            };
            return lst;
        }

        public List<SelectListItem> TiposPorcentajes()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            List<DatosEmisor> lst_de = new List<DatosEmisor>();
            ApplicationUser userAdmin = null;
            string idAdmin;
            using (var db = new ApplicationDbContext())
            {
                userAdmin = db.Users.Where(u => u.Email == "emisor@emisor.com").First();
                idAdmin = userAdmin.Id;
                lst_de = db.DatosEmisor.Where(de => de.UserId == idAdmin).ToList();

            }

            foreach (var item in lst_de)
            {
                SelectListItem temp = new SelectListItem()
                {
                    Text = item.Alias,
                    Value = item.Id.ToString(),
                };
                lst.Add(temp);
            }

            return lst;
        }

    }
}