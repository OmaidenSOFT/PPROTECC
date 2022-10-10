using System.Data;
namespace LogicBo
{
    public class EquipmentBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get Elements by Category Code list
        /// </summary>
        /// <returns></returns>
        public DataTable GetIndex()
        {
            var result = executeProcedures.DataTable("ENEL_LoadElements", null);
            return result;
        }

        #region Entity
        #endregion
    }
}
