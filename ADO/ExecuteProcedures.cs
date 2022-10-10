using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ADO
{

    public class ExecuteProcedures
    {

        #region Propiedades
        private readonly ConnectionBd _connectionBd;
        private DataTable _dtable;
        private DataSet _dset;
        private SqlDataAdapter _dataAdapter;
        private SqlCommand _sqlCommand;
        private SqlDataReader _dataReader;
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["AppConnection"].ToString();
        private int _timeOut;
        System.Diagnostics.TextWriterTraceListener _logFisico;
        #endregion

        #region Encapsulamiento
        public ConnectionBd ConnectionBd
        {
            get { return _connectionBd; }
        }
        public int TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }
        #endregion

        #region Constructor
        public ExecuteProcedures()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AppConnection"].ToString();
            _connectionBd = new ConnectionBd();
            _timeOut = 180;
            _sqlCommand = new SqlCommand();
            _dataAdapter = new SqlDataAdapter(_sqlCommand);
            _sqlCommand.CommandTimeout = _timeOut;

            _logFisico = new System.Diagnostics.TextWriterTraceListener("LogComponentAdo.log", "listener");

        }
        #endregion

        #region DataTable
        public DataTable DataTable(string name, List<SqlParameter> parameters)
        {
            try
            {
                _dtable = new DataTable();
                _connectionBd.OpenConnection();
                _sqlCommand = new SqlCommand();
                _sqlCommand.CommandText = name;
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Connection = _connectionBd.SqlConnection;
                _dataAdapter = new SqlDataAdapter(_sqlCommand);
                _sqlCommand.CommandTimeout = TimeOut;
                if (parameters != null && parameters.Count != 0)
                {
                    foreach (var listSqlParameter in parameters)
                    {
                        if (listSqlParameter.Value == null)
                        {
                            listSqlParameter.Value = DBNull.Value;
                        }
                        _sqlCommand.Parameters.Add(listSqlParameter);
                    }
                }
                try
                {
                    _dataAdapter.Fill(_dtable);
                    _connectionBd.CloseConnection();

                }
                catch (Exception ex)
                {
                    _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                    _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                    _logFisico.WriteLine("----------------------------------------");
                    _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                    _logFisico.WriteLine("----------------------------------------");
                    _logFisico.WriteLine(ex.Message);
                    _logFisico.WriteLine("----------------------------------------");
                    _logFisico.Flush();
                    _connectionBd.CloseConnection();

                    throw;
                }
                _connectionBd.CloseConnection();

                return _dtable;


            }
            catch (Exception ex)
            {
                _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                _logFisico.WriteLine(ex.Message);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.Flush();
                _connectionBd.CloseConnection();
                throw;
            }
        }

        #endregion

        #region DataSet
        public DataSet DataSet(string nameProcedure, List<SqlParameter> parameters)
        {
            try
            {
                _dset = new DataSet();
                _connectionBd.OpenConnection();
                _sqlCommand = new SqlCommand();
                _sqlCommand.CommandText = nameProcedure;
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Connection = _connectionBd.SqlConnection;
                _dataAdapter = new SqlDataAdapter(_sqlCommand);
                if (parameters != null && parameters.Count != 0)
                {
                    foreach (var listSqlParameter in parameters)
                    {
                        if (listSqlParameter.Value == null)
                        {
                            listSqlParameter.Value = DBNull.Value;
                        }
                        _sqlCommand.Parameters.Add(listSqlParameter);
                    }
                }

                try
                {
                    _dataAdapter.Fill(_dset);
                }
                catch (Exception ex)
                {
                    _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                    _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                    _logFisico.WriteLine("----------------------------------------");
                    _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                    _logFisico.WriteLine("----------------------------------------");
                    _logFisico.WriteLine(ex.Message);
                    _logFisico.WriteLine("----------------------------------------");
                    _logFisico.Flush();
                    throw;
                }
                _dataReader = _sqlCommand.ExecuteReader();
                _connectionBd.CloseConnection();

                return _dset;
            }
            catch (Exception ex)
            {
                _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                _logFisico.WriteLine(ex.Message);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.Flush();
                return null;
            }
        }
        #endregion

        #region Metodo DataTable a Bool
        public bool ExecuteCrudToBool(string nameStoreProcedure, List<SqlParameter> parameterList)
        {
            try
            {
                var result = DataTable(nameStoreProcedure, parameterList);

                if (result != null && result.Rows.Count > 0)
                {
                    var estadoOper = (from DataRow item in result.Rows
                                      select item[0].ToString()).FirstOrDefault();
                    var convert = Convert.ToBoolean(estadoOper);
                    return convert;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.WriteLine(ex.Message);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.Flush();
                throw;
            }
        }
        #endregion

        #region ExecuteScalarBoolean

        public bool ExecuteScalarBoolean(string consulta)
        {
            try
            {
                var cnn = new SqlConnection(_connectionString);
                cnn.Open();
                var cmd = new SqlCommand(consulta, cnn) { CommandType = CommandType.Text };
                var result = cmd.ExecuteScalar();
                cnn.Dispose();
                cnn.Close();
                if (result != null && result.ToString() != "0")
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.WriteLine(ex.Message);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.Flush();
                throw;

            }
        }


        #endregion

        #region ExecuteScalar

        public string ExecuteScalar(string consulta)
        {
            try
            {
                var cnn = new SqlConnection(_connectionString);
                cnn.Open();
                var cmd = new SqlCommand(consulta, cnn) { CommandType = CommandType.Text };
                var result = cmd.ExecuteScalar();
                cnn.Dispose();
                cnn.Close();
                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.WriteLine(ex.Message);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.Flush();
                throw;
            }
        }


        #endregion

        #region ExecuteReader
        public DataTable ExecuteReader(string consulta)
        {

            try
            {
                _dtable = new DataTable();
                var cnn = new SqlConnection(_connectionString);
                cnn.Open();
                var cmd = new SqlCommand(consulta, cnn) { CommandType = CommandType.Text };
                var datareader = cmd.ExecuteReader();
                _dtable.Load(datareader);
                datareader.Dispose();
                datareader.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.WriteLine(ex.Message);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.Flush();
                throw;

            }
            return _dtable;
        }


        #endregion

        #region datetableOutput
        public List<string> DataTableOutput(string name, List<ParameterSqlModel> parameters)
        {
            try
            {
                List<string> estado = new List<string>();
                _dtable = new DataTable();
                _connectionBd.OpenConnection();
                _sqlCommand = new SqlCommand();
                _sqlCommand.CommandText = name;
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Connection = _connectionBd.SqlConnection;
                _dataAdapter = new SqlDataAdapter(_sqlCommand);
                _sqlCommand.CommandTimeout = TimeOut;

                if (parameters != null && parameters.Count != 0)
                {
                    foreach (var listSqlParameter in parameters)
                    {
                        if (listSqlParameter.OutParameter)
                        {
                            _sqlCommand.Parameters.Add(listSqlParameter.Parameter);
                            _sqlCommand.Parameters[listSqlParameter.Parameter.ParameterName].Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            _sqlCommand.Parameters.Add(listSqlParameter.Parameter);
                        }
                    }
                    _dataReader = _sqlCommand.ExecuteReader();

                    foreach (var item in parameters)
                    {
                        if (item.OutParameter)
                        {
                            var valueestado = _sqlCommand.Parameters[item.Parameter.ParameterName].Value;
                            estado.Add(Convert.ToString(valueestado));
                        }
                    }
                }
                //try
                //{
                //    estado = _sqlCommand.Parameters[listSqlParameterOut.ParameterName].Value.ToString();
                //    _dataAdapter.Fill(_dtable);
                //    _connectionBd.CloseConnection();
                //}
                //catch (Exception ex)
                //{
                //    _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                //    _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                //    _logFisico.WriteLine("----------------------------------------");
                //    _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                //    _logFisico.WriteLine("----------------------------------------");
                //    _logFisico.WriteLine(ex.Message);
                //    _logFisico.WriteLine("----------------------------------------");
                //    _logFisico.Flush();
                //    _connectionBd.CloseConnection();

                //    throw;
                //}
                _connectionBd.CloseConnection();

                return estado;
            }
            catch (Exception ex)
            {
                _logFisico.WriteLine(string.Format("FECHA {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                _logFisico.WriteLine("TYPE EXCEPTION :" + ex.GetType().ToString());
                _logFisico.WriteLine("STACK TRACE" + ex.StackTrace);
                _logFisico.WriteLine(ex.Message);
                _logFisico.WriteLine("----------------------------------------");
                _logFisico.Flush();
                _connectionBd.CloseConnection();
                throw;
            }
        }

        #endregion
    }

    public class ParameterSqlModel
    {
        public SqlParameter Parameter { get; set; }
        public bool OutParameter { get; set; }
    }
}
