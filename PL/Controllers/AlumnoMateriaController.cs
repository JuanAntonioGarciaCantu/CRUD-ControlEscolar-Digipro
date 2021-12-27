using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();

            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno.Alumnos = result.Objects;

            return View(alumnoMateria);
        }

        [HttpGet]
        public ActionResult MateriasAsignadas(int idAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno.IdAlumno = idAlumno;

            ML.Result resultAlumno = BL.Alumno.GetByID(alumnoMateria.Alumno);
            ML.Result resultMaterias = BL.AlumnoMateria.MateriasAsignadasByIdAlumno(alumnoMateria);

            alumnoMateria.Alumno = ((ML.Alumno)resultAlumno.Object);            

            alumnoMateria.AlumnoMaterias = resultMaterias.Objects;

            return View(alumnoMateria);
        }

        [HttpGet]
        public ActionResult AsignarMaterias(int idAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno.IdAlumno = idAlumno;

            ML.Result resultAlumno = BL.Alumno.GetByID(alumnoMateria.Alumno);
            alumnoMateria.Alumno = ((ML.Alumno)resultAlumno.Object);

            ML.Result resultMaterias = BL.AlumnoMateria.MateriasNoAsignadasByIdAlumno(idAlumno);            
            alumnoMateria.AlumnoMaterias = resultMaterias.Objects;

            return View(alumnoMateria);
        }

        [HttpPost]
        public ActionResult AsignarMaterias(ML.AlumnoMateria alumnoMateria)
        {

            ViewBag.IdAlumno = alumnoMateria.Alumno.IdAlumno;            

            foreach (string idMateria in alumnoMateria.AlumnoMaterias)
            {
                int id = int.Parse(idMateria);
                ML.Result result = BL.AlumnoMateria.AsignarMateriasByIdAlumno(alumnoMateria.Alumno.IdAlumno, id);               

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Materia(s) asignadas correctamente";                    
                }
                else
                {
                    ViewBag.Mensaje = "Error al asignar las materias";                    
                    break;
                }
            }
            return PartialView("Modal");            
        }

        [HttpGet]
        public ActionResult EliminarMateriaAsignada(int idAlumnoMateria, int idAlumno)
        {

            ML.Result result = BL.AlumnoMateria.MateriaAsignadaDelete(idAlumnoMateria);
            ViewBag.IdAlumno = idAlumno;

            if (result.Correct)
            {
                ViewBag.Mensaje = "Materia eliminada correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Materia no eliminada";
            }

            return PartialView("Modal");
        }
    }
}