using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBo
{
    public class MissionBo
    {
        //#region Propiedades
        //private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        //#endregion
        //public List<Entity.Mission> GetAllMission()
        //{
        //    return entities.Mission.ToList();
        //}
        //public List<ReturnMission> GetAllMissionHome()
        //{
        //    var returnMission = new List<ReturnMission>();
        //    var mission=GetAllMission().Where(x => x.state && x.date >= DateTime.Now).ToList();
        //    foreach (var item in mission)
        //    {
        //        returnMission.Add(new ReturnMission { name = item.name, date = item.date.ToString("yyyy-MM-dd hh:mm:ss tt"),city=item.City.name, description = item.description });
        //    }
        //    return returnMission;
        //}
        //public bool SaveMission(string name, int idCity, string description, string listParticipants, DateTime dateSelect, bool state, int idUser)
        //{
        //    try
        //    {
        //        var mission = new Entity.Mission();

        //        mission.name = name;
        //        mission.idCityFk = idCity;
        //        mission.description = description;
        //        mission.date= dateSelect;
        //        mission.state= state;
        //        mission.createBy=idUser;
        //        mission.createDate= DateTime.Now;
        //        entities.Entry(mission).State = System.Data.Entity.EntityState.Added;
        //        entities.SaveChanges();
        //        foreach (var item in listParticipants.Split(',')) {
        //            var missionParticipant = new Entity.MissionParticipant();
        //            missionParticipant.idMissionFk= mission.idMissionPk;
        //            missionParticipant.idFriendFk= int.Parse(item);
        //            missionParticipant.createBy = idUser;
        //            missionParticipant.createDate = DateTime.Now;
        //            entities.Entry(missionParticipant).State = System.Data.Entity.EntityState.Added;
        //            entities.SaveChanges();

        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public List<Entity.Friend> GetParticipants(int idMission)
        //{
        //    return entities.MissionParticipant.Where(x => x.idMissionFk == idMission).Select(x => x.Friend).ToList();
        //}
    }
    #region Propiedades
    public class ReturnMission {
        public string name { get; set; }
        public string description{ get; set; }
        public string date { get; set; }
        public string city { get; set; }
    }
    #endregion
}
