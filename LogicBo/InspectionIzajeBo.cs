using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LogicBo
{
    [Serializable]
    public class InspectionIzajeBo
    {
        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion

        public DataTable GetInfo()
        {
            var result = executeProcedures.DataTable("ENEL_LoadIzageInspection", null);
            return result;
        }

        public DataTable GetInfoFinding()
        {
            var result = executeProcedures.DataTable("ENEL_LoadHallazgoIzage", null);
            return result;
        }

        public DataTable GetFindingById(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
            };
                var result = executeProcedures.DataTable("ENEL_LoadHallazgoIzageById", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public bool Edit(InspectionIzaje inspeccion, int id)
        //{
        //    try
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter> {
        //        new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
        //        new SqlParameter(){ ParameterName="FechaInspeccion", SqlDbType=SqlDbType.VarChar,Value=inspeccion.FechaInspeccion},
        //        new SqlParameter(){ ParameterName="Precinto", SqlDbType=SqlDbType.VarChar,Value=inspeccion.Precinto},
        //        new SqlParameter(){ ParameterName="IdEquipo", SqlDbType=SqlDbType.VarChar,Value=inspeccion.IdEquipo},
        //        new SqlParameter(){ ParameterName="Id_Estado", SqlDbType=SqlDbType.VarChar,Value=inspeccion.IdEstado},
        //        new SqlParameter(){ ParameterName="Id_Accion", SqlDbType=SqlDbType.VarChar,Value=inspeccion.IdAccion},
        //        new SqlParameter(){ ParameterName="Id_Inspector", SqlDbType=SqlDbType.VarChar,Value=inspeccion.IdInspector},
        //    };
        //        var result = executeProcedures.DataTable("ENEL_EditIzageInspection", parameters);
        //        if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
        //            throw new Exception(result.Rows[0][1].ToString());
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public bool EditFinding(int id, int idEstado, string planAccion, string gestionRealizar, string responsable, DateTime fechaGestion, int idEstadoGestion, DateTime fechaRealizado, string gestionRealizado, string archivo, int verificado)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="IdEstado", SqlDbType=SqlDbType.Int,Value=idEstado},
                new SqlParameter(){ ParameterName="DetallePlanAccion", SqlDbType=SqlDbType.VarChar,Value=planAccion},
                new SqlParameter(){ ParameterName="GestionRealizar", SqlDbType=SqlDbType.VarChar,Value=gestionRealizar},
                new SqlParameter(){ ParameterName="Responsable", SqlDbType=SqlDbType.VarChar,Value=responsable},
                new SqlParameter(){ ParameterName="FechaGestion", SqlDbType=SqlDbType.Date,Value=fechaGestion},
                new SqlParameter(){ ParameterName="IdEstadoGestion", SqlDbType=SqlDbType.Int,Value=idEstadoGestion},
                new SqlParameter(){ ParameterName="FechaRealizado", SqlDbType=SqlDbType.Date,Value=fechaRealizado},
                new SqlParameter(){ ParameterName="GestionRealizado", SqlDbType=SqlDbType.VarChar,Value=gestionRealizado},
                new SqlParameter(){ ParameterName="ArchivoEvidencia", SqlDbType=SqlDbType.VarChar,Value=archivo},
                new SqlParameter(){ ParameterName="Verificado", SqlDbType=SqlDbType.Int,Value=verificado},
            };
                var result = executeProcedures.DataTable("ENEL_EditHallazgoIzage", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Create(InspectionIzaje inspeccion)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="FechaInspeccion", SqlDbType=SqlDbType.DateTime,Value=inspeccion.FechaInspeccion},
                new SqlParameter(){ ParameterName="Precinto", SqlDbType=SqlDbType.VarChar,Value=inspeccion.Precinto},
                new SqlParameter(){ ParameterName="IdEquipo", SqlDbType=SqlDbType.Int,Value=inspeccion.IdEquipo},
                new SqlParameter(){ ParameterName="Id_Estado", SqlDbType=SqlDbType.Int,Value=inspeccion.IdEstado},
                new SqlParameter(){ ParameterName="Id_Accion", SqlDbType=SqlDbType.Int,Value=inspeccion.IdAccion},
                new SqlParameter(){ ParameterName="Id_Inspector", SqlDbType=SqlDbType.Int,Value=inspeccion.IdInspector},
            };
                var result = executeProcedures.DataTable("ENEL_SaveIzageInspection", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][2].ToString());
                return int.Parse(result.Rows[0][1].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CreateDetalle(int idFactor, string estado, string comentario)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IdFactor", SqlDbType=SqlDbType.Int,Value=idFactor},
                new SqlParameter(){ ParameterName="Estado", SqlDbType=SqlDbType.VarChar,Value=estado},
                new SqlParameter(){ ParameterName="Comentario", SqlDbType=SqlDbType.VarChar,Value=comentario},
            };
                var result = executeProcedures.DataTable("ENEL_SaveIzageInspectionDet", parameters);
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
                var result = executeProcedures.DataTable("ENEL_LoadIzageInspectionById", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
