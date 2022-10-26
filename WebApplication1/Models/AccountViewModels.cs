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
        public string Pais { get; set; }
        public string Bandera { get; set; }
        public string Welcome { get; set; }
        public string Invite { get; set; }
        public string TextoPro1 { get; set; }
        public string TextoPro2 { get; set; }
        public string TextoPro3 { get; set; }
        public string TextoPro4 { get; set; }

        public string Idioma { get; set; }
        public string Inicio { get; set; }
        public int Estado { get; set; }

        public string Modulos { get; set; }
        public string Subtitulos { get; set; }
        public string Descripciones { get; set; }
        public string piedepaginacards { get; set; }

        public string ModuloSeleccionado { get; set; }


    }
    public class RoleListModels
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; }
    }
}
