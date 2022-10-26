using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
namespace LogicBo
{
    public class HomeBo
    {

        #region Properties
        private readonly Entity.ModelEntities entities = new Entity.ModelEntities();
        private readonly Transversal.EncryptData encryptData = new Transversal.EncryptData();
        private readonly Transversal.DecryptData decryptData = new Transversal.DecryptData();
        #endregion
        public List<ImagesModels> GetBannerImages(string path)
        {
            path = path.Replace(@"Home\", "").Replace("Index","");
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            var model = new List<ImagesModels>();
            foreach (string filename in files)
            {
                if (Regex.IsMatch(filename, @".jpg|.png|.gif$"))
                {
                    model.Add(new ImagesModels
                    {
                        Extension = Path.GetExtension(filename),
                        Name = Path.GetFileNameWithoutExtension(filename)
                    });
                }
            }
            return model;
        }

        #region Entidades
        public class ImagesModels
        {
            public string Name { get; set; }
            public string Extension { get; set; }
        }
        #endregion
    }
}
