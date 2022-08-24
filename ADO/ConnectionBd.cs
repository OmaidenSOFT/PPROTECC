using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ADO
{

    public class ConnectionBd
    {
        #region Propiedades Privadas
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["AppConnection"].ToString();
        private readonly SqlConnection _sqlConnection;
        #endregion

        #region Encapsulamiento
        public SqlConnection SqlConnection
        {
            get { return _sqlConnection; }
        }
        #endregion

        #region Constructor
        public ConnectionBd()
        {
            _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = _connectionString;
        }
        #endregion

        #region Open
        public void OpenConnection()
        {
            try
            {
                SqlConnection.Open();
            }
            catch (Exception exception)
            {
                var exceptionString = exception;
                throw;
            }

        }
        #endregion

        #region Close
        public void CloseConnection()
        {
            try
            {
                SqlConnection.Close();
            }
            catch (Exception exception)
            {
                var exceptioString = exception;
                throw;
            }
        }
        #endregion
    }
}
