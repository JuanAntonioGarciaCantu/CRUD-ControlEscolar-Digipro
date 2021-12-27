using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = BL.Materia.GetAll();

            materia.Materias = result.Objects;

            return View(materia);
        }

        [HttpGet]
        public ActionResult Form(int? idMateria)
        {
            ML.Materia materia = new ML.Materia();

            if (idMateria == null)
            {
                return View();
            }
            else
            {                
                materia.IdMateria = idMateria.Value;

                ML.Result result = BL.Materia.GetById(materia);

                if(result.Correct)
                {
                    materia = ((ML.Materia)result.Object);
                    return View(materia);
                }
                else
                {
                    return View(materia);
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            if(materia.IdMateria == 0)
            {
                result = BL.Materia.Add(materia);

                if(result.Correct)
                {
                    ViewBag.Mensaje = "La materia se agrego correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "La materia no se agrego correctamente";
                }
            }
            else
            {
                result = BL.Materia.Update(materia);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "La materia se actulizo correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "La materia no se actualizo correctamente";
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idMateria)
        {
            ML.Materia materia = new ML.Materia();
            materia.IdMateria = idMateria;

            ML.Result result = BL.Materia.Delete(materia);

            if (result.Correct)
            {
                ViewBag.Mensaje = "La materia se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "La materia no se elimino correctamente";
            }

            return PartialView("Modal");
        }
    }
}