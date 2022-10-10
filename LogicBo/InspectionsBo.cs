using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace LogicBo
{
    public class InspectionsBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        public DataTable GetStock()
        {
            var result = executeProcedures.DataTable("ENEL_CargarInventario", null);

            return result;
        }
        public DataTable GetTechnicalReportsIndex(int idHeadquarter)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=idHeadquarter},
            };
            var result = executeProcedures.DataTable("ENEL_LoadTechReportsByEnterprise", parameters);
            return result;
        }
        public DataTable GetProceduresReportsIndex(int idHeadquarter)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=idHeadquarter},
            };
            var result = executeProcedures.DataTable("ENEL_LoadProceduresReports", parameters);
            return result;
        }
        public DataTable GetLegalityReportsIndex(int idHeadquarter)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=idHeadquarter},
            };
            var result = executeProcedures.DataTable("ENEL_LoadLegalityReports", parameters);
            return result;
        }
        public DataTable GetFormatsReportsIndex(int idHeadquarter)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=idHeadquarter},
            };
            var result = executeProcedures.DataTable("ENEL_LoadFormatsReports", parameters);
            return result;
        }
        public DataTable GetCertificatesReportsIndex(int idHeadquarter)
        {

            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=idHeadquarter},
            };
            var result = executeProcedures.DataTable("ENEL_LoadCertificatesReports", parameters);
            return result;
        }
        public Dictionary<string, string> GetFactorByElement(string rfid, int headquarterid)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="rfid", SqlDbType=SqlDbType.VarChar,Value=rfid},
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headquarterid},
            };
            var result = executeProcedures.DataTable("ENEL_LoadFactorsByRFID", parameters);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["factor"].ToString());
        }
        public DataTable GetStockByHeadquarter(int headquarterId)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="SedeId", SqlDbType=SqlDbType.Int,Value=headquarterId}
                 };
            var result = executeProcedures.DataTable("ENEL_LoadStockBySede", parameters);

            return result;
        }
        public Dictionary<string, string> GetTypeReportDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LOADTECHNICALREPORTSTYPE", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["tipo"].ToString());
        }
        public Dictionary<string, string> GetFeaturesDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LOADFEATURES", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Descripcion"].ToString());
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
        public bool Unsuscribe(string tag, string comment)
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
        public bool EditEquipment(string RFID, int areaid, int empleadoid)
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
        public DataTable SearchManagementOfFindings(int headQuarterTypeid)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.Int,Value=headQuarterTypeid},
        };
            var result = executeProcedures.DataTable("ENEL_LOADManagementsByElement", parameters);
            return result;


        }
        public DataTable SearchSchedulerInspections(int headQuarterTypeid, int Typeid, int ElementID)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headQuarterTypeid},
                    new SqlParameter(){ ParameterName="Typeid", SqlDbType=SqlDbType.Int,Value=Typeid},
                    new SqlParameter(){ ParameterName="ElementID", SqlDbType=SqlDbType.Int,Value=ElementID},
            };
            var result = executeProcedures.DataTable("ENEL_LOADSCHEINSPECTIONS", parameters);
            return result;
        }
        public DataTable ManagementMaintenanceEquipment(int headQuarterTypeid)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="SedeID", SqlDbType=SqlDbType.Int,Value=headQuarterTypeid},
        };
            var result = executeProcedures.DataTable("ENEL_LOADClosedManagementsByElement", parameters);
            return result;
        }
        public DataTable SearchFindingsByElement(int ElementID)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="ElementID", SqlDbType=SqlDbType.Int,Value=ElementID},
        };
            var result = executeProcedures.DataTable("ENEL_SearchFindingsByElement", parameters);
            return result;


        }
        public DataTable LoadDetailsEquipment(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
                     new SqlParameter(){ ParameterName="id", SqlDbType=SqlDbType.Int,Value=id},
        };
            var result = executeProcedures.DataTable("ENEL_LOADELEMENTACTION", parameters);
            return result;
        }

        public bool FeaturesCreate(int headQuarterType, int cbxFeatures, string name, string file)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="TipoSedeid", SqlDbType=SqlDbType.Int,Value=headQuarterType},
                new SqlParameter(){ ParameterName="CaracterizacionDocid", SqlDbType=SqlDbType.Int,Value=cbxFeatures},
                new SqlParameter(){ ParameterName="Nombre", SqlDbType=SqlDbType.VarChar,Value=name},
                new SqlParameter(){ ParameterName="Archivo", SqlDbType=SqlDbType.VarChar,Value=file},
            };
                var result = executeProcedures.DataTable("ENEL_CreateFilesByFeatures", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return bool.Parse(result?.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool TechInfoCreate(int headQuarterType, int Elementoid, string marca, string modelo, string observaciones, string file)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Sedeid", SqlDbType=SqlDbType.Int,Value=headQuarterType},
                new SqlParameter(){ ParameterName="Elementoid", SqlDbType=SqlDbType.Int,Value=Elementoid},
                new SqlParameter(){ ParameterName="Marca", SqlDbType=SqlDbType.VarChar,Value=marca},
                new SqlParameter(){ ParameterName="Modelo", SqlDbType=SqlDbType.VarChar,Value=modelo},
                new SqlParameter(){ ParameterName="Observaciones", SqlDbType=SqlDbType.VarChar,Value=observaciones},
                new SqlParameter(){ ParameterName="Ficha", SqlDbType=SqlDbType.VarChar,Value=file},
            };
                var result = executeProcedures.DataTable("ENEL_CREATETECHINFO", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return bool.Parse(result?.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public bool ManagmentOfFindingsSave(int encabezadoId, int? accionTomarId, string gestionRealizar, string responsable, DateTime? fechaGestion, DateTime? fechaRealizado, string gestionRealizado, string archivoEvidencia, int tipo)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Encabezadoid", SqlDbType=SqlDbType.Int,Value=encabezadoId},
                new SqlParameter(){ ParameterName="AccionTomarID", SqlDbType=SqlDbType.Int,Value=accionTomarId},
                new SqlParameter(){ ParameterName="GestionRealizar", SqlDbType=SqlDbType.VarChar,Value=gestionRealizar},
                new SqlParameter(){ ParameterName="Responsable", SqlDbType=SqlDbType.VarChar,Value=responsable},
                new SqlParameter(){ ParameterName="FechaGestion", SqlDbType=SqlDbType.Date,Value=fechaGestion},
                new SqlParameter(){ ParameterName="FechaRealizado", SqlDbType=SqlDbType.Date,Value=fechaRealizado},
                new SqlParameter(){ ParameterName="GestionRealizado", SqlDbType=SqlDbType.VarChar,Value=gestionRealizado},
                new SqlParameter(){ ParameterName="ArchivoEvidencia", SqlDbType=SqlDbType.VarChar,Value=archivoEvidencia},
                new SqlParameter(){ ParameterName="Tipo", SqlDbType=SqlDbType.Int,Value=tipo},
            };
                var result = executeProcedures.DataTable("ENEL_UPDATEFINDINGSPLAN", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return bool.Parse(result?.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool UpdateInspection(int id, int estadoFinalId, int accionTomarId, DateTime fechaInspeccion, int inspectorId, int ubicationId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Encabezadoid", SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter(){ ParameterName="EstadoFinalId", SqlDbType=SqlDbType.Int,Value=estadoFinalId},
                new SqlParameter(){ ParameterName="AccionTomarId", SqlDbType=SqlDbType.Int,Value=accionTomarId},
                new SqlParameter(){ ParameterName="FechaInspeccion", SqlDbType=SqlDbType.Date,Value=fechaInspeccion},
                new SqlParameter(){ ParameterName="InspectorID", SqlDbType=SqlDbType.Int,Value=inspectorId},
                new SqlParameter(){ ParameterName="Ubicationid", SqlDbType=SqlDbType.Int,Value=ubicationId},
            };
                var result = executeProcedures.DataTable("ENEL_UPDATEINSPECTIONS", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return bool.Parse(result?.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool UpdateFactorsByInspection(int estadoId, string comentario, int factorId, int encabezadoId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="Estadoid", SqlDbType=SqlDbType.Int,Value=estadoId},
                new SqlParameter(){ ParameterName="Comentario", SqlDbType=SqlDbType.VarChar,Value=comentario},
                new SqlParameter(){ ParameterName="Factorid", SqlDbType=SqlDbType.VarChar,Value=factorId},
                new SqlParameter(){ ParameterName="Encabezadoid", SqlDbType=SqlDbType.Int,Value=encabezadoId},
                          };
                var result = executeProcedures.DataTable("ENEL_UPDATEFACTORBYINSPECTION", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return bool.Parse(result?.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool UpdateVerificateActionPlan(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="id", SqlDbType=SqlDbType.Int,Value=id},
                          };
                var result = executeProcedures.DataTable("ENEL_UPDATEVERIFICATEACTIONPLAN", parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
