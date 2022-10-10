using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace LogicBo
{
    public class ActionResultBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get All Category active for list
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadActionResult", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Descripcion"].ToString());
        }
        public Dictionary<string, string> GetDictionaryByState(int Stateid)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="Stateid", SqlDbType=SqlDbType.Int,Value=Stateid},        };
            var result = executeProcedures.DataTable("ENEL_LoadActionResult", parameters);

            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Descripcion"].ToString());
        }
    }
}
