using BonoLogin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BonoLogin.Services
{
    public class BonoService
    {
        public List<PGracia> ListPGracia(int id_ficha) {
            List<PGracia> lst = null;
            using (var db = new ApplicationDbContext()) {
                lst = db.PGracia.Where(pg => pg.DatosBonoId == id_ficha).ToList();
            }
            return lst;
        }
        public void EditPG(int id_ficha, int periodo, string tipo)
        {
             PGracia pg = null;
            using (var db = new ApplicationDbContext())
            {
                pg = db.PGracia.Where(f => f.DatosBonoId == id_ficha && f.Periodo == periodo).First();
                pg.Tipo = tipo;
                db.SaveChanges();
            }
        }
        public void DeletePG(int id_ficha) {
            List<PGracia> lst = null;
            using (var db = new ApplicationDbContext()) {
                lst = db.PGracia.Where(pg => pg.DatosBonoId == id_ficha).ToList();
                foreach (var pg in lst) {
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

        public List<SelectListItem> Metodos() {
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


    }
}