using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBo
{
    public class UserBo
    {

        #region Propiedades
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly Transversal.EncryptData encryptData = new Transversal.EncryptData();
        private readonly Transversal.DecryptData decryptData = new Transversal.DecryptData();
        #endregion
        public List<Entity.Usuario> GetAllUser()
        {
            return entities.Usuario.ToList();
        }
    }
}
