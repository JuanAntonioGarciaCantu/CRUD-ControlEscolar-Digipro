using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class AlumnoMateria
    {

        public static ML.Result MateriasAsignadasByIdAlumno(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoMateriaByIdAlumno";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno",SqlDbType.Int);
                        collection[0].Value = alumnoMateria.Alumno.IdAlumno;

                        cmd.Parameters.AddRange(collection);

                        DataTable tableAlumnoMateria = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tableAlumnoMateria);

                        result.Objects = new List<object>();

                        if (tableAlumnoMateria.Rows.Count > 0)
                        {                           
                            foreach (DataRow row in tableAlumnoMateria.Rows)
                            {
                                ML.AlumnoMateria alumnoMateriaItem = new ML.AlumnoMateria();
                                alumnoMateriaItem.IdAlumno = int.Parse(row[0].ToString());

                                alumnoMateriaItem.Materia = new ML.Materia();
                                alumnoMateriaItem.Materia.IdMateria = int.Parse(row[1].ToString());
                                alumnoMateriaItem.Materia.Nombre = row[2].ToString();
                                alumnoMateriaItem.Materia.Costo = decimal.Parse(row[3].ToString());

                                result.Objects.Add(alumnoMateriaItem);
                            }

                            result.Correct = false;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontro la información";
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result MateriasNoAsignadasByIdAlumno(int idAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriasNoAsignadasByIdAlumno";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", SqlDbType.Int);
                        collection[0].Value = idAlumno;

                        cmd.Parameters.AddRange(collection);

                        DataTable tableMaterias = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tableMaterias);

                        result.Objects = new List<object>();

                        if (tableMaterias.Rows.Count > 0)
                        {
                                                       
                            foreach (DataRow row in tableMaterias.Rows)
                            {
                                ML.Materia materia = new ML.Materia();                                
                                materia.IdMateria = int.Parse(row[0].ToString());
                                materia.Nombre = row[1].ToString();
                                materia.Costo = decimal.Parse(row[2].ToString());

                                result.Objects.Add(materia);
                            }

                            result.Correct = true;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result AsignarMateriasByIdAlumno(int idAlumno, int idMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AsignarMateriasByIdAlumno";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("IdAlumno", SqlDbType.Int);
                        collection[0].Value = idAlumno;

                        collection[1] = new SqlParameter("IdMateria", SqlDbType.Int);
                        collection[1].Value = idMateria;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        int resultQuery = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if(resultQuery > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al asignar la materia";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result MateriaAsignadaDelete(int idAlumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoMateriaDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumnoMateria", SqlDbType.Int);
                        collection[0].Value = idAlumnoMateria;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        int resultQuery = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if(resultQuery > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al eliminar la información";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
