using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globons_Test.Models;

namespace Globons_Test.Controllers
{
    public class PersonaController : Controller
    {

        private Models.GlobonsEntities db = new GlobonsEntities();

        public ActionResult Index()
        {
            var personas = db.Persona;
            var personasList = GetListPersonaViewModel(personas);
            return View(personasList);
        }

        public ActionResult Create()
        {
            ViewBag.Direcciones = GetDirecciones();
            return View();
        }


        [HttpPost]
        public ActionResult Create(PersonaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!DocumentExists(model.NumeroDocumento))
                    {
                        var personaDb = Map(model);
                        db.Persona.Add(personaDb);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.DocumentoDuplicado = true;
                }

                ViewBag.Direcciones = GetDirecciones();
                return View();

            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var personaDb = db.Persona.Find(id);
            if (personaDb == null)
            { 
                return  HttpNotFound();
            }

            var persona = Map(personaDb);
            ViewBag.Direcciones = GetDirecciones();
            return View(persona);
        }

        [HttpPost]
        public ActionResult Edit(PersonaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!DocumentExists(model.NumeroDocumento))
                    {
                        var persona = Map(model);
                        db.Entry(persona).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.DocumentoDuplicado = true;

                }
                ViewBag.Direcciones = GetDirecciones();
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var personaDb = db.Persona.Find(id);
            if (personaDb == null)
            {
                return HttpNotFound();
            }

            var persona = Map(personaDb);

            return View(persona);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeletedConfirmed(int? id)
        {
            try
            {
                var personaDb = db.Persona.Find(id);
                db.Persona.Remove(personaDb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private SelectList GetDirecciones()
        {
            var direcciones = db.Direccion.Select(x => new {idDireccion = x.idDireccion , Descripcion = x.calle + " " + x.numero });
            return new SelectList(direcciones, "idDireccion", "Descripcion");

        }


        private static List<DireccionViewModel> GetListDireccionesViewModel(System.Data.Entity.DbSet<Direccion> direcciones)
        {
            List<DireccionViewModel> direccionesList = new List<DireccionViewModel>();

            foreach (var item in direcciones)
            {
                var direccionVM = Map(item);

                direccionesList.Add(direccionVM);
            }
            return direccionesList;
        }

        private static List<PersonaViewModel> GetListPersonaViewModel(System.Data.Entity.DbSet<Persona> personas)
        {
            var personasList = new List<PersonaViewModel>();

            foreach (var item in personas)
            {
                var personaVm = Map(item);
                personasList.Add(personaVm);
            }

            return personasList;
        }

        private static DireccionViewModel Map(Models.Direccion direccion)
        {
            var direccionVM = new DireccionViewModel()
            {
                Calle = direccion.calle,
                IdDireccion = direccion.idDireccion,
                Numero = direccion.numero,
            };
            return direccionVM;
        }

        private static Models.Persona Map(PersonaViewModel persona)
        {
            Models.Persona personaDb = new Models.Persona()
            {
            apellido = persona.Apellido,
            fechaNacimiento = persona.FechaNacimiento,
            idPersona = persona.Id,
            nombre = persona.Nombre,
            numeroDocumento = persona.NumeroDocumento,
            direccion = persona.idDireccion
            };

            return personaDb;
        }

        private static PersonaViewModel Map(Models.Persona persona)
        {
            var personaVm = new PersonaViewModel()
            {
                Apellido = persona.apellido,
                idDireccion = persona.direccion,
                Nombre = persona.nombre,
                NumeroDocumento = persona.numeroDocumento,
                FechaNacimiento = persona.fechaNacimiento,
                FechaNacimientoFormateada = persona.fechaNacimiento.ToString("dd/MM/yyyy"),
                Id = persona.idPersona,
                DireccionCompleta = persona.Direccion.calle + " " + persona.Direccion.numero
            };

            return personaVm;
        }

        private bool DocumentExists(int numeroDocumento)
        {
            var documento = db.Persona.Where(x => x.numeroDocumento == numeroDocumento);
            if (documento.Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
