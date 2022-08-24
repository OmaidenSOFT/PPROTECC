using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace LogicBo
{
    public class PartnerBo
    {
        //#region Propiedades
        ////private readonly ADO.ExecuteProcedures _executeProcedures = new ADO.ExecuteProcedures();
        //private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        //private readonly Transversal.EncryptData encryptData = new Transversal.EncryptData();
        //private readonly Transversal.DecryptData decryptData = new Transversal.DecryptData();
        //#endregion
        //public List<Entity.Partner> GetAllPartner() {
        //    return entities.Partner.ToList();
        //}
        //public bool SavePartner(string name, string email, string phone, string address, bool state,int idUser)
        //{
        //    try
        //    {
        //        if (entities.Partner.Where(x => x.name == name || x.email== email).Count() > 0)
        //            throw new Exception("Ya existe un patrocinador con esta información");
        //        var partner = new Entity.Partner();

        //        partner.name = name;
        //        partner.email = email;
        //        partner.phone = phone;
        //        partner.address= address;
        //        partner.state = state;
        //        partner.createDate = DateTime.Now;
        //        partner.createBy = idUser;
        //        entities.Entry(partner).State = System.Data.Entity.EntityState.Added;
        //        entities.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
