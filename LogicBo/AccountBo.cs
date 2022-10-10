using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace LogicBo
{
    public class AccountBo
    {
        #region Propiedades
        //private readonly ADO.ExecuteProcedures _executeProcedures = new ADO.ExecuteProcedures();
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly Transversal.EncryptData encryptData = new Transversal.EncryptData();
        private readonly Transversal.DecryptData decryptData = new Transversal.DecryptData();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion

        public Dictionary<string, string> GetDictionary()
        {
            return executeProcedures.DataTable("ENEL_LoadCountries", null).AsEnumerable().ToDictionary((DataRow row) => row["id"].ToString(), (DataRow row) => row["Nombre"].ToString());
        }
        /// <summary>
        /// Realiza la validacion del Usuario (A la espera de base de datos)
        /// </summary>
        /// <param name="nameUser"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserEntityAccount ValidUser(string nameUser, string password)
        {
            try
            {
                var passwordEncrypt = encryptData.Encrypt(password, true);
                var modelUser = new UserEntityAccount();
                var modelRole = new List<RoleEntityAccount>();

                //busqueda del usuario 
                var user = (from us in entities.Usuario
                            join usSe in entities.Usuario_Sede on us.Usuario_ID equals usSe.UsuarioID
                            join se in entities.Sede on usSe.SedeID equals se.id
                            join rol in entities.usuario_Rol on us.Usuario_ID equals rol.UsuarioID
                            where us.Usuario1 == nameUser && us.PasswordMD5 == passwordEncrypt
                            select new { us.Usuario_ID, us.Usuario1, us.Nombres, us.Apellidos, se.SedeCategoriaid, rol.RolId }).FirstOrDefault();
                if (user != null)
                {
                    modelUser.IdUser = user.Usuario_ID;
                    modelUser.LoginUser = user.Usuario1;
                    modelUser.NameUser = user.Nombres;
                    modelUser.SurNameUser = user.Apellidos;
                    modelUser.SedeCategoriaId = user.SedeCategoriaid;
                    modelUser.RolId = user.RolId;
                }


                return modelUser;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataSet CreateEmailAptitudConcept()
        {
            try
            {
                var result = executeProcedures.DataSet("ENEL_LoadEmailfromCM", null);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public bool CreateSentEmailt(int employeeId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="MedID", SqlDbType=SqlDbType.Int,Value=employeeId}
                };
                var result = executeProcedures.DataTable("ENEL_INSERTSENTMAIL", parameters);
                if (!Convert.ToBoolean(result?.Rows[0][0].ToString()))
                    throw new Exception(result.Rows[0][1].ToString());

                return bool.Parse(result?.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }




        #region Entidades
        public class UserEntityAccount
        {
            public int IdUser { get; set; }
            public string LoginUser { get; set; }
            public string NameUser { get; set; }
            public string SurNameUser { get; set; }
            public int? SedeCategoriaId { get; set; }
            public int? RolId { get; set; }
        }
        public class RoleEntityAccount
        {
            public int IdRole { get; set; }
            public string NameRole { get; set; }
        }
        #endregion
    }
}
