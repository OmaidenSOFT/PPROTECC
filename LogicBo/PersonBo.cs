using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LogicBo
{
    public class PersonBo
    {
        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        public DataTable GetIndex(string identificacion, string nombre, int idSede, string fechaInicio, string fechaFin)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter(){ ParameterName="Identificacion", SqlDbType=SqlDbType.VarChar,Value=identificacion},
                new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=nombre},
                new SqlParameter(){ ParameterName="IdSede", SqlDbType=SqlDbType.Int,Value=idSede},
                new SqlParameter(){ ParameterName="FechaInicio", SqlDbType=SqlDbType.VarChar,Value=fechaInicio},
                new SqlParameter(){ ParameterName="FechaFin", SqlDbType=SqlDbType.VarChar,Value=fechaFin},
            };
            var result = executeProcedures.DataTable("ENEL_PersonLoad", parameters);
            return result;
        }

        public DataTable GetInfoCursoDet(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
            };
                var result = executeProcedures.DataTable("ENEL_curdetLoadByIdPer", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetInfo(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
            };
                var result = executeProcedures.DataTable("ENEL_PersonLoadById", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int Create(string identificacion, string nombre, string apellido, int idSede, string telefono, string celular, string correo, string asignado, string foto, string observacion, bool estado)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Identificacion", SqlDbType=SqlDbType.VarChar,Value=identificacion},
                new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=nombre},
                new SqlParameter(){ ParameterName="Apellido", SqlDbType=SqlDbType.VarChar,Value=apellido},
                new SqlParameter(){ ParameterName="IdSede", SqlDbType=SqlDbType.Int,Value=idSede},
                new SqlParameter(){ ParameterName="Telefono", SqlDbType=SqlDbType.VarChar,Value=telefono},
                new SqlParameter(){ ParameterName="Celular", SqlDbType=SqlDbType.VarChar,Value=celular},
                new SqlParameter(){ ParameterName="Correo", SqlDbType=SqlDbType.VarChar,Value=correo},
                new SqlParameter(){ ParameterName="Asignado", SqlDbType=SqlDbType.VarChar,Value=asignado},
                new SqlParameter(){ ParameterName="Foto", SqlDbType=SqlDbType.VarChar,Value=foto},
                new SqlParameter(){ ParameterName="Observacion", SqlDbType=SqlDbType.VarChar,Value=observacion},
                new SqlParameter(){ ParameterName="Estado", SqlDbType=SqlDbType.Bit,Value=estado},

            };
                var result = executeProcedures.DataTable("ENEL_PersonSave", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][2].ToString());

                return int.Parse(result.Rows[0][1].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Edit(int id, string identificacion, string nombre, string apellido, int idSede, string telefono, string celular, string correo, string asignado, string foto, string observacion, bool estado, string imagen)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="Identificacion", SqlDbType=SqlDbType.VarChar,Value=identificacion},
                new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=nombre},
                new SqlParameter(){ ParameterName="Apellido", SqlDbType=SqlDbType.VarChar,Value=apellido},
                new SqlParameter(){ ParameterName="IdSede", SqlDbType=SqlDbType.Int,Value=idSede},
                new SqlParameter(){ ParameterName="Telefono", SqlDbType=SqlDbType.VarChar,Value=telefono},
                new SqlParameter(){ ParameterName="Celular", SqlDbType=SqlDbType.VarChar,Value=celular},
                new SqlParameter(){ ParameterName="Correo", SqlDbType=SqlDbType.VarChar,Value=correo},
                new SqlParameter(){ ParameterName="Asignado", SqlDbType=SqlDbType.VarChar,Value=asignado},
                new SqlParameter(){ ParameterName="Foto", SqlDbType=SqlDbType.VarChar,Value=foto},
                new SqlParameter(){ ParameterName="Observacion", SqlDbType=SqlDbType.VarChar,Value=observacion},
                new SqlParameter(){ ParameterName="Estado", SqlDbType=SqlDbType.Bit,Value=estado},
                new SqlParameter(){ ParameterName="Imagen", SqlDbType=SqlDbType.VarChar,Value=imagen},
            };
                var result = executeProcedures.DataTable("ENEL_PersonEdit", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool EditImg(int id, string imagen)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="Imagen", SqlDbType=SqlDbType.VarChar,Value=imagen},

            };
                var result = executeProcedures.DataTable("ENEL_PersonEditImg", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CreateCursoDet(int idPersona, int idCurso, DateTime fechaInicio, string archivo)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IdCurso", SqlDbType=SqlDbType.Int,Value=idCurso},
                new SqlParameter(){ ParameterName="IdPersona", SqlDbType=SqlDbType.Int,Value=idPersona},
                new SqlParameter(){ ParameterName="FechaInicio", SqlDbType=SqlDbType.DateTime,Value=fechaInicio},
                new SqlParameter(){ ParameterName="Archivo", SqlDbType=SqlDbType.VarChar,Value=archivo},
            };
                var result = executeProcedures.DataTable("ENEL_CursoDetSave", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][2].ToString());

                return int.Parse(result.Rows[0][1].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void DeleteCursoDet(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
            };
                executeProcedures.DataTable("ENEL_CursoDetDelete", parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
