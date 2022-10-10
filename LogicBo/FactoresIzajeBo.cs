using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LogicBo
{
    public class FactoresIzajeBo
    {
        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion

        public DataTable GetInfo()
        {
            var result = executeProcedures.DataTable("ENEL_LoadFactorIzaje", null);
            return result;
        }

        public Dictionary<string, string> GetCategoriaDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadCategoriaIzaje", null);
            return result.AsEnumerable().ToDictionary(row => row["Id"].ToString(), row => row["Categoria"].ToString());
        }

        public int Create(int idCategoria, string nombre)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IdCategoriaFK", SqlDbType=SqlDbType.VarChar,Value=idCategoria},
                new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=nombre},
                new SqlParameter(){ ParameterName="Estado", SqlDbType=SqlDbType.Bit,Value=1},
            };
                var result = executeProcedures.DataTable("ENEL_SaveFactorIzaje", parameters);
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
                var result = executeProcedures.DataTable("ENEL_FactorIzajeById", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataTable GetByIdEquipo(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IdEquipo", SqlDbType=SqlDbType.Int,Value=id},
            };
                var result = executeProcedures.DataTable("ENEL_FactorIzajeByIdEquipo", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetInspeccionDetalle(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IdEquipo", SqlDbType=SqlDbType.Int,Value=id},
            };
                var result = executeProcedures.DataTable("ENEL_LoadIzageInspectionDet", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
