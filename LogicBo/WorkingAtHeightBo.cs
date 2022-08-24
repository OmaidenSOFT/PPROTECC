using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
namespace LogicBo
{
    public class WorkingAtHeightBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        public DataTable GetStock(int CountryID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "PaisId";
            sqlParameter.SqlDbType = SqlDbType.Int;
            sqlParameter.Value = (object)CountryID;
            parameters.Add(sqlParameter);
            return this.executeProcedures.DataTable("ENEL_CargarInventario", parameters);
        }
        public DataTable GetStockByHeadquarter(int headquarterId)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="SedeId", SqlDbType=SqlDbType.Int,Value=headquarterId}
                 };
            var result = executeProcedures.DataTable("ENEL_LoadStockBySede", parameters);
            
            return result;
        }
  public DataTable GetResultbyElement(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Encabezadoid", SqlDbType=SqlDbType.Int,Value=Id}
                 };
            var result = executeProcedures.DataTable("ENEL_LOADResultByRFID", parameters);

            return result;
        }


        





        



        public DataTable GetDatabyElement(int equipementId)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="IDEncabezado", SqlDbType=SqlDbType.Int,Value=equipementId}
                 };
            var result = executeProcedures.DataTable("ENEL_CreateHV", parameters);

            return result;
        }

        public Dictionary<string, string> GetAreaDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadAreas", null);
            return result.AsEnumerable().ToDictionary(row => row["ID"].ToString(), row => row["Descripcion"].ToString());
        }
        public Dictionary<string, string> GetResourceByHeadquarter(int headquarterId)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headquarterId}
                 };
            var result = executeProcedures.DataTable("ENEL_LoadEmployee", parameters);
            return result.AsEnumerable().ToDictionary(row => row["ID"].ToString(), row => row["Inspector"].ToString());
        }
        public bool Unsuscribe(string tag,string comment)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="RFID", SqlDbType=SqlDbType.VarChar,Value=tag},
                new SqlParameter(){ ParameterName="Description", SqlDbType=SqlDbType.VarChar,Value=comment},
                 };
            var result = executeProcedures.DataTable("ENEL_RemoveFromStock", parameters);
            if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                throw new Exception(result.Rows[0][1].ToString());

            return true;
        }
        public bool EditEquipment(string RFID,int areaid, int empleadoid)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="RFID", SqlDbType=SqlDbType.VarChar,Value=RFID},
                new SqlParameter(){ ParameterName="AreaID", SqlDbType=SqlDbType.Int,Value=areaid},
                new SqlParameter(){ ParameterName="EmpleadoID", SqlDbType=SqlDbType.Int,Value=empleadoid},
            };
                var result = executeProcedures.DataTable("ENEL_UpdateEquipment", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataTable GetInfoEquipment(string RFID)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="RFID", SqlDbType=SqlDbType.VarChar,Value=RFID},
            };
                var result = executeProcedures.DataTable("ENEL_LoadInfoElementsByRFID", parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool AssignEquipmentSave(string tag,int headquarterid, int? areaid,int? employedId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="RFID", SqlDbType=SqlDbType.VarChar,Value=tag},
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headquarterid},
                new SqlParameter(){ ParameterName="AreaID", SqlDbType=SqlDbType.Int,Value=areaid},
                new SqlParameter(){ ParameterName="EmpleadoID", SqlDbType=SqlDbType.Int,Value=employedId},
            };
                var result = executeProcedures.DataTable("ENEL_UpdateAssigmentElement", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return bool.Parse(result?.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
