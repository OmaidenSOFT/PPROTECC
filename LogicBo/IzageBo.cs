using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace LogicBo
{
    public class IzageBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion

        public Dictionary<string, string> GetUnidadDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LOADUnidadMedida", null);
            return result.AsEnumerable().ToDictionary(row => row["ID"].ToString(), row => row["Descripcion"].ToString());
        }

        public Dictionary<string, string> GetTipoEquipoDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadTipoEquipo", null);
            return result.AsEnumerable().ToDictionary(row => row["Id"].ToString(), row => row["Nombre"].ToString());
        }

        public int Create(EquipoIzage equipo)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IdTipoEquipo", SqlDbType=SqlDbType.VarChar,Value=equipo.IdTipoEquipo},
                new SqlParameter(){ ParameterName="Marca", SqlDbType=SqlDbType.VarChar,Value=equipo.Marca},
                new SqlParameter(){ ParameterName="Modelo", SqlDbType=SqlDbType.VarChar,Value=equipo.Modelo},
                new SqlParameter(){ ParameterName="Serial", SqlDbType=SqlDbType.VarChar,Value=equipo.Serial},
                new SqlParameter(){ ParameterName="Lote", SqlDbType=SqlDbType.VarChar,Value=equipo.Lote},
                new SqlParameter(){ ParameterName="Tag", SqlDbType=SqlDbType.VarChar,Value=equipo.Tag},
                new SqlParameter(){ ParameterName="MaxCapacidad", SqlDbType=SqlDbType.VarChar,Value=equipo.Maxcapacidad},
                new SqlParameter(){ ParameterName="UnidadCapacidad_id", SqlDbType=SqlDbType.Int,Value=equipo.Unidadcapacidad},
                new SqlParameter(){ ParameterName="Accesorio", SqlDbType=SqlDbType.VarChar,Value=equipo.Accesorio},
                new SqlParameter(){ ParameterName="AsignadoA", SqlDbType=SqlDbType.VarChar,Value=equipo.Asignadoa},
                new SqlParameter(){ ParameterName="FechaCompra", SqlDbType=SqlDbType.DateTime,Value=equipo.FechaCompra},
                new SqlParameter(){ ParameterName="FechaFabricacion", SqlDbType=SqlDbType.DateTime,Value=equipo.FechaFabricacion},
                new SqlParameter(){ ParameterName="Ubicacion_id", SqlDbType=SqlDbType.Int,Value=equipo.Ubicacion},
                new SqlParameter(){ ParameterName="Sede_id", SqlDbType=SqlDbType.Int,Value=equipo.Sede},
                new SqlParameter(){ ParameterName="FechaInspeccionInicial", SqlDbType=SqlDbType.DateTime,Value=equipo.FechaInspeccionInicial},
                new SqlParameter(){ ParameterName="Observaciones", SqlDbType=SqlDbType.VarChar,Value=equipo.Observaciones},
                new SqlParameter(){ ParameterName="Imagen", SqlDbType=SqlDbType.VarChar,Value=equipo.Imagen},
            };
                var result = executeProcedures.DataTable("ENEL_SaveIzageequipment", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][2].ToString());

                return int.Parse(result.Rows[0][1].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int CreateUbicacion(string nombre)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=nombre},
            };
                var result = executeProcedures.DataTable("ENEL_SaveUbicacionIzage", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][2].ToString());

                return int.Parse(result.Rows[0][1].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Edit(EquipoIzage equipo, int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="IdTipoEquipo", SqlDbType=SqlDbType.VarChar,Value=equipo.IdTipoEquipo},
                new SqlParameter(){ ParameterName="Marca", SqlDbType=SqlDbType.VarChar,Value=equipo.Marca},
                new SqlParameter(){ ParameterName="Modelo", SqlDbType=SqlDbType.VarChar,Value=equipo.Modelo},
                new SqlParameter(){ ParameterName="Serial", SqlDbType=SqlDbType.VarChar,Value=equipo.Serial},
                new SqlParameter(){ ParameterName="Lote", SqlDbType=SqlDbType.VarChar,Value=equipo.Lote},
                new SqlParameter(){ ParameterName="Tag", SqlDbType=SqlDbType.VarChar,Value=equipo.Tag},
                new SqlParameter(){ ParameterName="MaxCapacidad", SqlDbType=SqlDbType.VarChar,Value=equipo.Maxcapacidad},
                new SqlParameter(){ ParameterName="UnidadCapacidad_id", SqlDbType=SqlDbType.Int,Value=equipo.Unidadcapacidad},
                new SqlParameter(){ ParameterName="Accesorio", SqlDbType=SqlDbType.VarChar,Value=equipo.Accesorio},
                new SqlParameter(){ ParameterName="AsignadoA", SqlDbType=SqlDbType.VarChar,Value=equipo.Asignadoa},
                new SqlParameter(){ ParameterName="FechaCompra", SqlDbType=SqlDbType.DateTime,Value=equipo.FechaCompra},
                new SqlParameter(){ ParameterName="FechaFabricacion", SqlDbType=SqlDbType.DateTime,Value=equipo.FechaFabricacion},
                new SqlParameter(){ ParameterName="Ubicacion_id", SqlDbType=SqlDbType.Int,Value=equipo.Ubicacion},
                new SqlParameter(){ ParameterName="Sede_id", SqlDbType=SqlDbType.Int,Value=equipo.Sede},
                new SqlParameter(){ ParameterName="FechaInspeccionInicial", SqlDbType=SqlDbType.DateTime,Value=equipo.FechaInspeccionInicial},
                new SqlParameter(){ ParameterName="Observaciones", SqlDbType=SqlDbType.VarChar,Value=equipo.Observaciones},
                new SqlParameter(){ ParameterName="Imagen", SqlDbType=SqlDbType.VarChar,Value=equipo.Imagen},
            };
                var result = executeProcedures.DataTable("ENEL_EditIzageEquipment", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool EditUbicacion(int ubi, int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="Ubicacion_id", SqlDbType=SqlDbType.VarChar,Value=ubi},
            };
                var result = executeProcedures.DataTable("ENEL_EditUbicacionIzage", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool EditImg(string img, int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="Imagen", SqlDbType=SqlDbType.VarChar,Value=img},
            };
                var result = executeProcedures.DataTable("ENEL_EditImgIzageEquipment", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetIndex(string serial, int idSede, string equipo, string marca, string estado, string tag)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="Serial", SqlDbType=SqlDbType.VarChar,Value=serial},
                    new SqlParameter(){ ParameterName="IdSede", SqlDbType=SqlDbType.Int,Value=idSede},
                    new SqlParameter(){ ParameterName="Equipo", SqlDbType=SqlDbType.VarChar,Value=equipo},
                    new SqlParameter(){ ParameterName="Marca", SqlDbType=SqlDbType.VarChar,Value=marca},
                    new SqlParameter(){ ParameterName="Estado", SqlDbType=SqlDbType.VarChar,Value=estado},
                    new SqlParameter(){ ParameterName="TagENEL", SqlDbType=SqlDbType.VarChar,Value=tag},
            };
            var result = executeProcedures.DataTable("ENEL_LoadIzageEquipment", parameters);
            return result;
        }

        public DataTable GetInfo(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Id", SqlDbType=SqlDbType.Int,Value=id},
            };
                var result = executeProcedures.DataTable("ENEL_LoadIzageEquipmentById", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetInfoByTagSerial(string tagSerial)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="TagSerial", SqlDbType=SqlDbType.VarChar,Value=tagSerial},
            };
                var result = executeProcedures.DataTable("ENEL_LoadIzageEquipmentByTagSerial", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetInventario()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                };
                var result = executeProcedures.DataTable("ENEL_InventarioIzaje", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Entity
        #endregion
    }
}
