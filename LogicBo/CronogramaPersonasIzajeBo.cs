using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LogicBo
{
    public class CronogramaPersonasIzajeBo
    {
        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        public DataTable GetIndex(int idSede, string tipoCronograma, int annio)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                    new SqlParameter(){ ParameterName="IdSede", SqlDbType=SqlDbType.Int,Value=idSede},
                    new SqlParameter(){ ParameterName="TipoCronograma", SqlDbType=SqlDbType.VarChar,Value=tipoCronograma},
                    new SqlParameter(){ ParameterName="Annio", SqlDbType=SqlDbType.Int,Value=annio},
            };
            var result = executeProcedures.DataTable("ENEL_CronogramaPersonLoad", parameters);
            return result;
        }

        public DataTable GetInfo(int id, string identificacion, string nombre, int idSede, string fechaInicio, string fechaFin)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
                    new SqlParameter(){ ParameterName="Identificacion", SqlDbType=SqlDbType.VarChar,Value=identificacion},
                    new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=nombre},
                    new SqlParameter(){ ParameterName="IdSede", SqlDbType=SqlDbType.Int,Value=idSede},
                    new SqlParameter(){ ParameterName="FechaInicio", SqlDbType=SqlDbType.VarChar,Value=fechaInicio},
                    new SqlParameter(){ ParameterName="FechaFin", SqlDbType=SqlDbType.VarChar,Value=fechaFin},
                };
                var result = executeProcedures.DataTable("ENEL_CronogramaPersonDetalleLoad", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
