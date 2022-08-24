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
    public class MarkBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get Elements by Category Code list
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetDictionaryByHeadquarterElement(int idHeadquarter,int idElement)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=idHeadquarter},
                new SqlParameter(){ ParameterName="ElementoID", SqlDbType=SqlDbType.Int,Value=idElement}
            };
            var result = executeProcedures.DataTable("ENEL_LOADMarcas", parameters);
            return result.AsEnumerable().ToDictionary(row => row["ID"].ToString(), row => string.Format("{0}({1})",row["Marca"].ToString(), row["Modelo"].ToString()));
        }

        #region Entity
        #endregion
    }
}
