using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globons_Test.Models;
using System.Data.Entity;

namespace Globons_Test.Controllers
{
    public class DireccionController : Controller
    {

        private Models.GlobonsEntities db = new GlobonsEntities();

        public ActionResult Index()
        {

            var direcciones = db.Direccion;

            List<DireccionViewModel> direccionesList = GetListDireccionesViewModel(direcciones);

            return View(direccionesList);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(DireccionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var direccionDb = Map(model);
                    db.Direccion.Add(direccionDb);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            DireccionViewModel direccionVm = new DireccionViewModel();
            var direccionDb = db.Direccion.Find(id);

            if (direccionDb == null)
            {
                return HttpNotFound();
            }

            direccionVm = Map(direccionDb);
            return View(direccionVm);
        }


        [HttpPost]
        public ActionResult Edit(DireccionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var direccion = Map(model);
                        db.Entry(direccion).State = EntityState.Modified;
                        db.SaveChanges();
                    return RedirectToAction("Index");

                }

                return View(model);
            }
            catch(Exception ex)
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            var direccion = db.Direccion.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Mensaje = ValidateAssociatedPerson(id);

            var direccionVm = Map(direccion);
            return View(direccionVm);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeletedConfirmed(int? id)
        {
            try
            {
                var direccion = db.Direccion.Find(id);
                db.Direccion.Remove(direccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

        private static Models.Direccion Map(DireccionViewModel direccion)
        {
            var direccionDb = new Models.Direccion()
            {
                idDireccion = direccion.IdDireccion,
                calle = direccion.Calle,
                numero = direccion.Numero,
                Persona = null,
            };
            return direccionDb;
        }

        private string ValidateAssociatedPerson(int idDireccion)
        {
            var peopleAssociated = db.Persona.Where(x => x.Direccion.idDireccion == idDireccion).Count();

            if (peopleAssociated > 0)
            {
                return string.Format("La direccion que quiere eliminar está asociada a {0} personas", peopleAssociated);  
            }
            return null;
        }

    }
}
