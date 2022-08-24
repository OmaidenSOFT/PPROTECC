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
    public class TechnicalInformationBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures= new ADO.ExecuteProcedures();
        #endregion
        public DataTable GetByMark(int idMark)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="id", SqlDbType=SqlDbType.Int,Value=idMark}
            };
            var result = executeProcedures.DataTable("ENEL_LOADTecFolder", parameters);

            return result;
        }
    }
}
