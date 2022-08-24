using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SessionModels
    {
        public string Email { get; set; }
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string LoginUser { get; set; }
        public int? SedeCategoriaId { get; set; }
        public int? RolId { get; set; }
    }
    public class RoleListModels
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; }
    }
}
