using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace LogicBo
{
    public class CategoryBo
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
            var result = executeProcedures.DataTable("ENEL_LoadCategories", null);
            return result.AsEnumerable().ToDictionary(row => row["Codigo"].ToString(), row => row["nombre"].ToString());
        }

    }
}
