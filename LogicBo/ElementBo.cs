using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace LogicBo
{
    public class ElementBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get Elements by Category Code list
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadElement", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Nombre"].ToString());
        }
        public Dictionary<string, string> GetDictionaryByType()
        {
            var result = executeProcedures.DataTable("ENEL_LoadTypes", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Tipo"].ToString());
        }

        public Dictionary<string, string> GetDictionaryByCategory(int idCategory)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="categoriaid", SqlDbType=SqlDbType.Int,Value=idCategory}
            };
            var result = executeProcedures.DataTable("ENEL_LoadElementsByCategoria", parameters);
            return result.AsEnumerable().ToDictionary(row => row["Codigo"].ToString(), row => row["Nombre"].ToString());
        }
        public object[] GetDataElementByRFID(string rfid, int headquarterid)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="rfid", SqlDbType=SqlDbType.VarChar,Value=rfid},
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headquarterid},
            };
            var result = executeProcedures.DataTable("ENEL_LoadDataElementbyRFID", parameters);
            if (result.Rows.Count == 0)
                return new object[0];

            var element = result.AsEnumerable().ToList()?[0].ItemArray;

            return element;
        }


        #region Entity

        #endregion
    }
}
