using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace LogicBo
{
    public class FinalStateBo
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
            var result = executeProcedures.DataTable("ENEL_LoadFinalState", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Descripcion"].ToString());
        }

        public Dictionary<string, string> GetStateIzaje()
        {
            var result = executeProcedures.DataTable("ENEL_LoadFinalStateIzaje", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Descripcion"].ToString());
        }

        public Dictionary<string, string> GetStatesDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadStatesBYFactors", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Descripcion"].ToString());
        }



    }
}
