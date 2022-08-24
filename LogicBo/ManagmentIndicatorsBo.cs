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
    public class ManagmentIndicatorsBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion

        #region Métodos Públicos
        public List<Results> GetStock(int headquarterId, int year)
        {
            var model = new List<Results>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };
            var result = executeProcedures.DataTable("ENEL_LoadStockByFilters", parameters);

            foreach (DataRow item in result?.Rows)
            {
                model.Add(new Results { label = item[0].ToString(), value = int.Parse(item[1].ToString()), color = item[2].ToString() });
            }
            return model;
        }
        public List<ResultsByMonth> GetAims(int headquarterId, int year)
        {
            var model = new List<ResultsByMonth>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };

            var result = executeProcedures.DataTable("ENEL_LOADINSPECTIONSBYMONTHBYFILTERS", parameters);

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
        public List<Results> GetCoverage(int headquarterId, int year)
        {
            var model = new List<Results>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };
            var result = executeProcedures.DataTable("ENEL_LOADSCHTSABYFILTERS", parameters);

            foreach (DataRow item in result?.Rows)
            {
                model.Add(new Results { label = item[0].ToString(), value = int.Parse(item[1].ToString()), color = item[2].ToString() });
            }
            return model;
        }
        public List<ResultsByMonth> GetAimsTrainning(int headquarterId, int year)
        {
            var model = new List<ResultsByMonth>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };
            var result = executeProcedures.DataTable("ENEL_LOADTRAINNINGBYMONTHBYFILTERFINAL", parameters);

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
        public List<ResultsByMonth> GetFindingsByMonth(int headquarterId, int year)
        {
            var model = new List<ResultsByMonth>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };
            var result = executeProcedures.DataTable("ENEL_LOADFINDINGSBYMONTHBYFILTERS", parameters);

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
        public List<Results> GetFindingsTotal(int headquarterId, int year)
        {
            var model = new List<Results>();
            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter(){ ParameterName="headquarterId", SqlDbType=SqlDbType.Int,Value=headquarterId},
                    new SqlParameter(){ ParameterName="year", SqlDbType=SqlDbType.Int,Value=year}
                };
            var result = executeProcedures.DataTable("ENEL_LOADFindingsTotalByFilters", parameters);
            foreach (DataRow item in result?.Rows)
            {
                model.Add(new Results { label = item[0].ToString(), value = int.Parse(item[1].ToString()), color = item[2].ToString() });
            }
            return model;

        }

        #endregion

        #region Entity

        public class Results
        {
            public string label { get; set; }
            public int value { get; set; }
            public string color { get; set; }
        }

        public class ResultsByMonth
        {
            public int Enero { get; set; }
            public int Febrero { get; set; }
            public int Marzo { get; set; }
            public int Abril { get; set; }
            public int Mayo { get; set; }
            public int Junio { get; set; }
            public int Julio { get; set; }
            public int Agosto { get; set; }
            public int Septiembre { get; set; }
            public int Octubre { get; set; }
            public int Noviembre { get; set; }
            public int Diciembre { get; set; }
        }
        #endregion
    }
}
