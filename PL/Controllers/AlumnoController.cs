using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();

            ML.Alumno alumno = new ML.Alumno();

            alumno.Alumnos = result.Objects;

            return View(alumno);
        }

        [HttpGet]
        public ActionResult Form(int? idAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();

            if(idAlumno == null) //agregar
            {
                return View(alumno);
            }
            else //actualizar
            {
                alumno.IdAlumno = idAlumno.Value;

                ML.Result result = BL.Alumno.GetByID(alumno);

                if (result.Correct)
                {
                    alumno = ((ML.Alumno)result.Object);
                    return View(alumno);
                }
                else
                {
                    return View();
                }
            }             
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            if(alumno.IdAlumno == 0)
            {
                result = BL.Alumno.Add(alumno);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "El alumno se registro correctamente";
                }
                else
                {
                    ViewBag.Mensaje = result.ErrorMessage;
                }
            }
            else
            {
                result = BL.Alumno.Update(alumno);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "El alumno se actualizo correctamente";
                }
                else
                {
                    ViewBag.Mensaje = result.ErrorMessage;
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.IdAlumno = idAlumno;

            ML.Result result = BL.Alumno.Delete(alumno);

            if(result.Correct)
            {
                ViewBag.Mensaje = "El alumno se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = result.ErrorMessage;
            }

            return PartialView("Modal");
        }

    }
}