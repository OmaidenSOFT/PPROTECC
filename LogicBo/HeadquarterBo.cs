﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
namespace LogicBo
{
    public class HeadquarterBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures= new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get All Headquarter active for list
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadSedes", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(),row => row["Sede"].ToString());
        }

        public Dictionary<string, string> GetRoleDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadRoles", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Descripcion"].ToString());
        }
        public Dictionary<string, string> GetDictionaryWithoutAll()
        {
            var result = executeProcedures.DataTable("ENEL_LoadSedesW", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Sede"].ToString());
        }

        public Dictionary<string, string> GetDictionaryheadQuarterType()
        {
            var result = executeProcedures.DataTable("ENEL_LoadHeadQuarterType", null);
            return result.AsEnumerable().ToDictionary(row => row["id"].ToString(), row => row["Central"].ToString());
        }

        public Dictionary<string, string> GetYearsDictionary()
        {
            var result = executeProcedures.DataTable("ENEL_LoadAnios", null);
            return result.AsEnumerable().ToDictionary(row => row["Año"].ToString(), row => row["Año"].ToString());
        }

        #region Entity
        #endregion
    }
}
