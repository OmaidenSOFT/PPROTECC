using ADO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogicBo.ManagmentIndicatorsBo;

namespace LogicBo
{
    public class IndicadoresBo
    {
        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion

        public List<Results> GetInspectionChar1(int idSede, int annio)
        {
            var model = new List<Results>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=idSede},
                new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=annio}
                };
            var result = executeProcedures.DataTable("ENEL_IzageInspectionLoadChar1", parameters);

            foreach (DataRow item in result?.Rows)
            {
                model.Add(new Results { label = item[0].ToString(), value = int.Parse(item[1].ToString()), color = item[2].ToString() });
            }
            return model;
        }

        public List<ResultsByMonth> GetInspectionChar2(int headquarterId, int year)
        {
            var model = new List<ResultsByMonth>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };
            var result = executeProcedures.DataTable("ENEL_IzageInspectionLoadChar2", parameters);

            foreach (DataRow item in result?.Rows)
            {
                model.Add(new ResultsByMonth
                {
                    Enero = int.Parse(item[0].ToString()),
                    Febrero = int.Parse(item[1].ToString()),
                    Marzo = int.Parse(item[2].ToString()),
                    Abril = int.Parse(item[3].ToString()),
                    Mayo = int.Parse(item[4].ToString()),
                    Junio = int.Parse(item[5].ToString()),
                    Julio = int.Parse(item[6].ToString()),
                    Agosto = int.Parse(item[7].ToString()),
                    Septiembre = int.Parse(item[8].ToString()),
                    Octubre = int.Parse(item[9].ToString()),
                    Noviembre = int.Parse(item[10].ToString()),
                    Diciembre = int.Parse(item[11].ToString()),
                });
            }
            return model;
        }
        
        public List<Results> GetPersonChar1(int idSede, int annio)
        {
            var model = new List<Results>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=idSede},
                new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=annio}
                };
            var result = executeProcedures.DataTable("ENEL_PersonLoadChar1", parameters);

            foreach (DataRow item in result?.Rows)
            {
                model.Add(new Results { label = item[0].ToString(), value = int.Parse(item[1].ToString()), color = item[2].ToString() });
            }
            return model;
        }

        public List<ResultsByMonth> GetPersonChar2(int headquarterId, int year)
        {
            var model = new List<ResultsByMonth>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };
            var result = executeProcedures.DataTable("ENEL_PersonLoadChar2", parameters);

            foreach (DataRow item in result?.Rows)
            {
                model.Add(new ResultsByMonth
                {
                    Enero = int.Parse(item[0].ToString()),
                    Febrero = int.Parse(item[1].ToString()),
                    Marzo = int.Parse(item[2].ToString()),
                    Abril = int.Parse(item[3].ToString()),
                    Mayo = int.Parse(item[4].ToString()),
                    Junio = int.Parse(item[5].ToString()),
                    Julio = int.Parse(item[6].ToString()),
                    Agosto = int.Parse(item[7].ToString()),
                    Septiembre = int.Parse(item[8].ToString()),
                    Octubre = int.Parse(item[9].ToString()),
                    Noviembre = int.Parse(item[10].ToString()),
                    Diciembre = int.Parse(item[11].ToString()),
                });
            }
            return model;
        }

        public List<Results> GetFindingChar1(int idSede, int annio)
        {
            var model = new List<Results>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=idSede},
                new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=annio}
                };
            var result = executeProcedures.DataTable("ENEL_HallazgoLoadChar1", parameters);

            foreach (DataRow item in result?.Rows)
            {
                model.Add(new Results { label = item[0].ToString(), value = int.Parse(item[1].ToString()), color = item[2].ToString() });
            }
            return model;
        }

        public List<ResultsByMonth> GetFindingChar2(int headquarterId, int year)
        {
            var model = new List<ResultsByMonth>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };
            var result = executeProcedures.DataTable("ENEL_HallazgoLoadChar2", parameters);

            foreach (DataRow item in result?.Rows)
            {
                model.Add(new ResultsByMonth
                {
                    Enero = int.Parse(item[0].ToString()),
                    Febrero = int.Parse(item[1].ToString()),
                    Marzo = int.Parse(item[2].ToString()),
                    Abril = int.Parse(item[3].ToString()),
                    Mayo = int.Parse(item[4].ToString()),
                    Junio = int.Parse(item[5].ToString()),
                    Julio = int.Parse(item[6].ToString()),
                    Agosto = int.Parse(item[7].ToString()),
                    Septiembre = int.Parse(item[8].ToString()),
                    Octubre = int.Parse(item[9].ToString()),
                    Noviembre = int.Parse(item[10].ToString()),
                    Diciembre = int.Parse(item[11].ToString()),
                });
            }
            return model;
        }
    }
}
