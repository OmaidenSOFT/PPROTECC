using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LogicBo
{
    public class CronogramaIzajeBo
    {
        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        public DataTable GetIndex(int idSede, int idTipoEquipo, string tipoCronograma, int annio)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                    new SqlParameter(){ ParameterName="IdSede", SqlDbType=SqlDbType.Int,Value=idSede},
                    new SqlParameter(){ ParameterName="IdTipoEquipo", SqlDbType=SqlDbType.Int,Value=idTipoEquipo},
                    new SqlParameter(){ ParameterName="TipoCronograma", SqlDbType=SqlDbType.VarChar,Value=tipoCronograma},
                    new SqlParameter(){ ParameterName="Annio", SqlDbType=SqlDbType.Int,Value=annio},
            };
            var result = executeProcedures.DataTable("ENEL_CronogramaLoad", parameters);
            return result;
        }

        public DataTable GetInfo(int id, string serial, string tag)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
                    new SqlParameter(){ ParameterName="Serial", SqlDbType=SqlDbType.VarChar,Value=serial},
                    new SqlParameter(){ ParameterName="Tag", SqlDbType=SqlDbType.VarChar,Value=tag},
                };
                var result = executeProcedures.DataTable("ENEL_CronogramaDetalleLoad", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
