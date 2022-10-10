using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace LogicBo
{
    public class LocationBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get All Headquarter active for list
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadLocations", null);
            return result.AsEnumerable().ToDictionary(row => row["ID"].ToString(), row => row["Descripcion"].ToString());
        }
        public Dictionary<string, string> GetUbicacionIzaje()
        {
            var result = executeProcedures.DataTable("ENEL_LoadUbicacionIzage", null);
            return result.AsEnumerable().ToDictionary(row => row["ID"].ToString(), row => row["Descripcion"].ToString());
        }

        #region Entity
        #endregion
    }
}
