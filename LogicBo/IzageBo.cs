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

        public int Create(EquipoIzage equipo)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Equipo", SqlDbType=SqlDbType.VarChar,Value=equipo.Equipo},
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

            };
                var result = executeProcedures.DataTable("ENEL_SaveIzageEquipment", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                
                    return int.Parse(result.Rows[0][2].ToString());
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
