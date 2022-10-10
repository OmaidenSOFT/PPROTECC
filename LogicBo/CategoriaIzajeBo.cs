using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LogicBo
{
    public class CategoriaIzajeBo
    {
        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion

        public DataTable GetInfo()
        {
            var result = executeProcedures.DataTable("ENEL_LoadCategoriaIzaje", null);
            return result;
        }

        public int Create(int idTipoEquipo, string nombre)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IdTipoEquipoFK", SqlDbType=SqlDbType.Int,Value=idTipoEquipo},
                new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=nombre},
            };
                var result = executeProcedures.DataTable("ENEL_SaveCategoriaIzaje", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][2].ToString());
                return int.Parse(result.Rows[0][1].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public DataTable GetById(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
            };
                var result = executeProcedures.DataTable("ENEL_LoadCategoriaIzajeById", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
