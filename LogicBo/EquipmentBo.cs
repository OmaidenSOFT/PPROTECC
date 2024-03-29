﻿using System;
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
    public class EquipmentBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly ADO.ExecuteProcedures executeProcedures = new ADO.ExecuteProcedures();
        #endregion
        /// <summary>
        /// Get Elements by Category Code list
        /// </summary>
        /// <returns></returns>
            public DataTable GetIndex()
            {
                var result = executeProcedures.DataTable("ENEL_LoadElements", null);
                return result;
            }

        #region Entity
        #endregion
    }
}
