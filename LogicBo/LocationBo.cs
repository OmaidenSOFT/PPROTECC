using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
namespace LogicBo
{
    public class LocationBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures= new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get All Headquarter active for list
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadLocations", null);
            return result.AsEnumerable().ToDictionary(row => row["ID"].ToString(),row => row["Descripcion"].ToString());
        }

        #region Entity
        #endregion
    }
}
