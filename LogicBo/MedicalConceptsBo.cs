using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
namespace LogicBo
{
    public class MedicalConceptsBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion

        #region Métodos Públicos
        public Dictionary<string, string> GetEmployeeByHeadquarter(int idCategory)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=idCategory}
            };
            var result = executeProcedures.DataTable("ENEL_LOADTSABYHEADQUARTER", parameters);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Empleado"].ToString());
        }
        public Dictionary<string, string> GetConceptsDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LOADCONCEPTSTYPE", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Concepto"].ToString());
        }

        public bool CreateAptitudConcept(int employeeId, int conceptId, DateTime conceptDate, string file)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="TSAid", SqlDbType=SqlDbType.Int,Value=employeeId},
                new SqlParameter(){ ParameterName="TipoConceptoID", SqlDbType=SqlDbType.Int,Value=conceptId},
                new SqlParameter(){ ParameterName="FechaConcepto", SqlDbType=SqlDbType.DateTime,Value=conceptDate},
                new SqlParameter(){ ParameterName="@Archivo", SqlDbType=SqlDbType.VarChar,Value=file}

            };
                var result = executeProcedures.DataTable("ENEL_CREATEMEDICALCONCEPTS", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return bool.Parse(result?.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public DataTable SearchMedicalConcepts(int headQuarterType)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headQuarterType},
                            };
                var result = executeProcedures.DataTable("ENEL_LOADMEDICALCONCEPTSBYTSA", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #endregion

        #region Entity

        #endregion
    }
}
