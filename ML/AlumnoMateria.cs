using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class AlumnoMateria
    {
        public int IdAlumno { get; set; }
        public ML.Alumno Alumno { get; set; }
        public ML.Materia Materia { get; set; }
        public List<Object> AlumnoMaterias { get; set; }
    }
}
