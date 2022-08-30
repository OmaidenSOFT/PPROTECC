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
    public class CurriculumBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get List By Filters
        /// </summary>
        /// <returns></returns>
        public DataTable GetListByElement(int? headquarterId, int? elementId, string rFID)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="sedeId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                new SqlParameter(){ ParameterName="ElementoID", SqlDbType=SqlDbType.Int,Value=elementId},
                new SqlParameter(){ ParameterName="RFID", SqlDbType=SqlDbType.VarChar,Value=rFID}
            };
            var result = executeProcedures.DataTable("ENEL_LoadHVFromElement", parameters);
            return result;
        }

        public DataTable GetListCertificatesByElement(int? headquarterId, int? elementId, string rFID)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="sedeId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                new SqlParameter(){ ParameterName="ElementoID", SqlDbType=SqlDbType.Int,Value=elementId},
                new SqlParameter(){ ParameterName="RFID", SqlDbType=SqlDbType.VarChar,Value=rFID}
            };
            var result = executeProcedures.DataTable("ENEL_LoadInspectionCErtificateFromElement", parameters);
            return result;
        }

        



        public int Create(string rFID, string serial, string model, string material, DateTime fabricationDate, string mark, string lot, int elementId, int headquarterId, DateTime purchaseDate, int? areaId,int? employedId,int assignedDate,int ubicacionID)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="RFID", SqlDbType=SqlDbType.VarChar,Value=rFID},
                new SqlParameter(){ ParameterName="Serial", SqlDbType=SqlDbType.VarChar,Value=serial},
                new SqlParameter(){ ParameterName="Modelo", SqlDbType=SqlDbType.VarChar,Value=model},
                new SqlParameter(){ ParameterName="Material", SqlDbType=SqlDbType.VarChar,Value=material},
                new SqlParameter(){ ParameterName="FechaFabricacion", SqlDbType=SqlDbType.DateTime,Value=fabricationDate},
                new SqlParameter(){ ParameterName="Marca", SqlDbType=SqlDbType.VarChar,Value=mark},
                new SqlParameter(){ ParameterName="Lote", SqlDbType=SqlDbType.VarChar,Value=lot},
                new SqlParameter(){ ParameterName="ElementoID", SqlDbType=SqlDbType.Int,Value=elementId},
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headquarterId},
                new SqlParameter(){ ParameterName="FechaCompra", SqlDbType=SqlDbType.DateTime,Value=purchaseDate},
                new SqlParameter(){ ParameterName="AreaID", SqlDbType=SqlDbType.Int,Value=areaId},
                new SqlParameter(){ ParameterName="EmpleadoID", SqlDbType=SqlDbType.Int,Value=employedId},
                new SqlParameter(){ ParameterName="TipoFecha", SqlDbType=SqlDbType.Int,Value=assignedDate},
                new SqlParameter(){ ParameterName="UbicacionID", SqlDbType=SqlDbType.Int,Value=ubicacionID},
            };
                var result = executeProcedures.DataTable("ENEL_SaveElement", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                
                    return int.Parse(result.Rows[0][2].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public int CreateSede(string NombreSede, int Paisid, int SedeCategoriaID,string ubicacion, string codeENEL)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="NombreSede", SqlDbType=SqlDbType.VarChar,Value=NombreSede},
                new SqlParameter(){ ParameterName="Ubicacion", SqlDbType=SqlDbType.VarChar,Value=ubicacion},
                new SqlParameter(){ ParameterName="EnelCode", SqlDbType=SqlDbType.VarChar,Value=codeENEL},
                new SqlParameter(){ ParameterName="SedeCategoriaID", SqlDbType=SqlDbType.Int,Value=SedeCategoriaID},
                new SqlParameter(){ ParameterName="PaisID", SqlDbType=SqlDbType.Int,Value=Paisid}
            };
                var result = executeProcedures.DataTable("ENEL_CreateSede", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());


                return int.Parse(result.Rows[0][2].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public int CreatePais(string pais)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="NombrePais", SqlDbType=SqlDbType.VarChar,Value=pais}
                };
                var result = executeProcedures.DataTable("ENEL_CreatePais", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return int.Parse(result.Rows[0][2].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public int CreateSedeCategoria(string NombreCategoriaSede)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="NombreCategoriaSede", SqlDbType=SqlDbType.VarChar,Value=NombreCategoriaSede}
                };
                var result = executeProcedures.DataTable("ENEL_CreateSedeCategoria", parameters);
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
