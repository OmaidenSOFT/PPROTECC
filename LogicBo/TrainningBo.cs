using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
namespace LogicBo
{
    public class TrainningBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        public Dictionary<string, string> GetDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadInspectorByTSA", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Inspector"].ToString());
        }
        public Dictionary<string, string> GetInspectorDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadInspectors", null);
            return result.AsEnumerable().ToDictionary(row => row["Codigo"].ToString(), row => row["Nombre"].ToString());
        }
        public Dictionary<string, string> GetInspectorDDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadInspectoresD", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Nombre"].ToString());
        }
        public DataTable GetTrainningByHeadquarter(int headquarterId)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="SedeId", SqlDbType=SqlDbType.Int,Value=headquarterId}
                 };
            var result = executeProcedures.DataTable("ENEL_LoadStockBySede", parameters);

            return result;
        }
        public DataTable GetSearchCertificate(string name, string identification)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="nombre", SqlDbType=SqlDbType.VarChar,Value=name },
                new SqlParameter(){ ParameterName="identificacion", SqlDbType=SqlDbType.VarChar,Value=identification}

                 };
            var result = executeProcedures.DataTable("ENEL_LoadPersonalTSA", parameters);

            return result;
        }

        public DataTable GetSearchScheduler(int headQuarterType, int schedulerType, int order)

        {
            if (schedulerType == 1)
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.Int,Value=headQuarterType}
            };
                var result = executeProcedures.DataTable("ENEL_LOADSCHEDULEBYTSABYYEAR", parameters);
                return result;
            }
            else
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.Int,Value=headQuarterType}
                };
                var result = executeProcedures.DataTable("ENEL_LOADSCHEDULEBYTSABYMonth", parameters);
                return result;
            }
        }
        public DataTable GetSearchTrainningByHeadquarter(int? headquarterId)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headquarterId}
                 };
            var result = executeProcedures.DataTable("ENEL_LoadTSaBySede", parameters);

            return result;
        }

        public DataTable GetLoadDetailsScheduler(int headQuarterId, int schedulerType, string month, int type, int year)
        {
            if (type == 1)
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                    //new SqlParameter(){ ParameterName="Year", SqlDbType=SqlDbType.Int,Value=year},
                    new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.Int,Value=headQuarterId},
                    //new SqlParameter(){ ParameterName="Order", SqlDbType=SqlDbType.Int,Value=2},
                };
                var result = executeProcedures.DataTable("ENEL_LOADSCHEDULEBYTSABYYEARDetails", parameters);
                return result;
            }
            else
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.Int,Value=headQuarterId},
                 new SqlParameter(){ ParameterName="Month", SqlDbType=SqlDbType.VarChar,Value=month},
new SqlParameter(){ ParameterName="YearINI", SqlDbType=SqlDbType.VarChar,Value=year}

                };
                var result = executeProcedures.DataTable("ENEL_LOADSCHEDULEBYTSABYMonthDetails", parameters);
                return result;
            }


        }

        public DataTable GetLoadDetailsSchedulerAcumulado(int headQuarterId, int schedulerType, string month, int type, int year)
        {
            if (type == 1)
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                    //new SqlParameter(){ ParameterName="Year", SqlDbType=SqlDbType.Int,Value=year},
                    new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.Int,Value=headQuarterId},
                    //new SqlParameter(){ ParameterName="Order", SqlDbType=SqlDbType.Int,Value=2},
                };
                var result = executeProcedures.DataTable("ENEL_LOADSCHEDULEBYTSABYYEARDetails", parameters);
                return result;
            }
            else
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.Int,Value=headQuarterId},
                 new SqlParameter(){ ParameterName="Month", SqlDbType=SqlDbType.VarChar,Value=month},
new SqlParameter(){ ParameterName="YearINI", SqlDbType=SqlDbType.VarChar,Value=year}

                };
                var result = executeProcedures.DataTable("ENEL_LOADSCHEDULEBYTSABYMonthDetailsAcumulado", parameters);
                return result;
            }


        }

        public DataTable GetLoadDetailsUser(int userId)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="TSAID", SqlDbType=SqlDbType.Int,Value=userId}
                 };
            var result = executeProcedures.DataTable("ENEL_LoadTSAHistory", parameters);

            return result;
        }
        public DataTable GetIndexPerson()
        {
            var result = executeProcedures.DataTable("ENEL_LoadInitialTSA", null);
            return result;
        }
        public DataTable GetIndexUser()
        {
            var result = executeProcedures.DataTable("ENEL_LoadInitialUser", null);
            return result;
        }

        public DataTable GetIndexInspector()
        {
            var result = executeProcedures.DataTable("[ENEL_LoadInitialInspectors]", null);
            return result;
        }
        public DataTable GetIndexRol()
        {
            var result = executeProcedures.DataTable("ENEL_LoadInitialRols", null);
            return result;
        }


        public Dictionary<string, string> GetCourseInitialDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadInitialCourses", null);
            return result.AsEnumerable().ToDictionary(row => row["ID"].ToString(), row => row["DescripcionCurso"].ToString());
        }
        public Dictionary<string, string> GetCourseDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadCourses", null);
            return result.AsEnumerable().ToDictionary(row => row["Codigo"].ToString(), row => row["Curso"].ToString());
        }
        public int CreatePerson(string name, string identification, string asigned, int headquarterId, DateTime initialDate, int courseInitial, string apellido)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=name},
                new SqlParameter(){ ParameterName="Identificacion", SqlDbType=SqlDbType.VarChar,Value=identification},
                new SqlParameter(){ ParameterName="Asignado", SqlDbType=SqlDbType.VarChar,Value=asigned},
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headquarterId},
                new SqlParameter(){ ParameterName="FechaInicial", SqlDbType=SqlDbType.DateTime,Value=initialDate},
                new SqlParameter(){ ParameterName="CursoInicialID", SqlDbType=SqlDbType.Int,Value=courseInitial},
                new SqlParameter(){ ParameterName="@LastName", SqlDbType=SqlDbType.VarChar,Value=apellido}
            };
                var result = executeProcedures.DataTable("ENEL_CreateTSA", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return int.Parse(result?.Rows[0][2].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public int CreateUser(string usuario, string nombres, string apellidos, string email, string pass, int rolid, int headQuarterType)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Usuario", SqlDbType=SqlDbType.VarChar,Value=usuario},
                new SqlParameter(){ ParameterName="Nombres", SqlDbType=SqlDbType.VarChar,Value=nombres},
                new SqlParameter(){ ParameterName="Apellidos", SqlDbType=SqlDbType.VarChar,Value=apellidos},
                new SqlParameter(){ ParameterName="Email", SqlDbType=SqlDbType.VarChar,Value=email},
                new SqlParameter(){ ParameterName="PasswordMD5", SqlDbType=SqlDbType.VarChar,Value=pass},
                new SqlParameter(){ ParameterName="RolID", SqlDbType=SqlDbType.VarChar,Value=rolid},
                new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.VarChar,Value=headQuarterType},


                };
                var result = executeProcedures.DataTable("ENEL_CREATEUSER", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return int.Parse(result?.Rows[0][2].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public int CreateInspector(string usuario, string nombres, string apellidos, string identi, string pass, int rolid, int headQuarterType, int activo)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Usuario", SqlDbType=SqlDbType.VarChar,Value=usuario},
                new SqlParameter(){ ParameterName="Nombres", SqlDbType=SqlDbType.VarChar,Value=nombres},
                new SqlParameter(){ ParameterName="Apellidos", SqlDbType=SqlDbType.VarChar,Value=apellidos},
                new SqlParameter(){ ParameterName="PasswordMD5", SqlDbType=SqlDbType.VarChar,Value=pass},
                new SqlParameter(){ ParameterName="RolID", SqlDbType=SqlDbType.VarChar,Value=rolid},
                new SqlParameter(){ ParameterName="identificacion", SqlDbType=SqlDbType.VarChar,Value=identi},
                new SqlParameter(){ ParameterName="activo", SqlDbType=SqlDbType.Int,Value=activo},

                                };
                var result = executeProcedures.DataTable("ENEL_CREATEUSER", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return int.Parse(result?.Rows[0][2].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public bool EditPerson(int idPerson, int headquarterId, string name, string lastName)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="TSAId", SqlDbType=SqlDbType.Int,Value=idPerson},
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headquarterId},
                new SqlParameter(){ ParameterName="name", SqlDbType=SqlDbType.VarChar,Value=name},
                new SqlParameter(){ ParameterName="lastName", SqlDbType=SqlDbType.VarChar,Value=lastName}
            };
                var result = executeProcedures.DataTable("ENEL_UpdateTSA", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public int EditUser(int id, string usuario, string email, string pass, int headQuarterType, int rolid)
        {
            try
            {
                if (string.IsNullOrEmpty(pass))
                {

                    List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="id", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="Usuario", SqlDbType=SqlDbType.VarChar,Value=usuario},
                new SqlParameter(){ ParameterName="Email", SqlDbType=SqlDbType.VarChar,Value=email},
                new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.VarChar,Value=headQuarterType},
                new SqlParameter(){ ParameterName="RoleID", SqlDbType=SqlDbType.VarChar,Value=rolid},

                };
                    var result = executeProcedures.DataTable("ENEL_EDITUSER", parameters);
                    if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                        throw new Exception(result.Rows[0][1].ToString());

                    return int.Parse(result?.Rows[0][2].ToString());
                }
                else
                {
                    List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="id", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="Usuario", SqlDbType=SqlDbType.VarChar,Value=usuario},
                new SqlParameter(){ ParameterName="Email", SqlDbType=SqlDbType.VarChar,Value=email},
                new SqlParameter(){ ParameterName="PasswordMD5", SqlDbType=SqlDbType.VarChar,Value=pass},
                 new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.VarChar,Value=headQuarterType},
                new SqlParameter(){ ParameterName="RoleID", SqlDbType=SqlDbType.VarChar,Value=rolid},

                };
                    var result = executeProcedures.DataTable("ENEL_EDITUSER", parameters);
                    if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                        throw new Exception(result.Rows[0][1].ToString());

                    return int.Parse(result?.Rows[0][2].ToString());
                };

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int EditInspector(int id, int activo)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="id", SqlDbType=SqlDbType.Int,Value=id},
              new SqlParameter(){ ParameterName="activo", SqlDbType=SqlDbType.Int,Value=activo},
                };
                var result = executeProcedures.DataTable("ENEL_EDITINSPECTOR", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return int.Parse(result?.Rows[0][2].ToString());


            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public int CreateCourse(int employedId, int courseId, DateTime ccourseDate)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="EmpleadoID", SqlDbType=SqlDbType.Int,Value=employedId},
                new SqlParameter(){ ParameterName="CursoID", SqlDbType=SqlDbType.Int,Value=courseId},
                new SqlParameter(){ ParameterName="Fecha", SqlDbType=SqlDbType.DateTime,Value=ccourseDate}
            };
                var result = executeProcedures.DataTable("ENEL_CreateCoursesFromTSA", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return int.Parse(result.Rows[0][2].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool DeleteCoursesById(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="TSAID ", SqlDbType=SqlDbType.Int,Value=id},
            };
                var result = executeProcedures.DataTable("ENEL_DELETETSA", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataTable GetCoursesByPerson(int employedId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IdEmpleado", SqlDbType=SqlDbType.Int,Value=employedId},
            };
                var result = executeProcedures.DataTable("ENEL_LoadCoursesFromTSA", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataTable GetInfoPerson(int employedId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="TSAID", SqlDbType=SqlDbType.Int,Value=employedId},
            };
                var result = executeProcedures.DataTable("ENEL_LoadInitialTSAByID", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataTable GetInfoUser(int userId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="UserID", SqlDbType=SqlDbType.Int,Value=userId},
            };
                var result = executeProcedures.DataTable("ENEL_LoadInitialUserByID", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataTable GetInfoInspector(int userId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="UserID", SqlDbType=SqlDbType.Int,Value=userId},
            };
                var result = executeProcedures.DataTable("ENEL_LoadInitialInspectorByID", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
