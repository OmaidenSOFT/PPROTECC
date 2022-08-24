using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBo
{
    public class EquipoIzage
    {
        private string equipo;
        private string marca;
        private string modelo;
        private string serial;
        private string lote;
        private string tag;
        private string maxcapacidad;
        private int unidadcapacidad;
        private string accesorio;
        private string asignadoa;
        private DateTime fechaCompra;
        private DateTime fechaFabricacion;
        private int ubicacion;
        private int sede;
        private DateTime fechaInspeccionInicial;
        private int cbxAssigned;
        private string observaciones;

        public string Equipo { get => equipo; set => equipo = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public string Serial { get => serial; set => serial = value; }
        public string Lote { get => lote; set => lote = value; }
        public string Tag { get => tag; set => tag = value; }
        public string Maxcapacidad { get => maxcapacidad; set => maxcapacidad = value; }
        public int Unidadcapacidad { get => unidadcapacidad; set => unidadcapacidad = value; }
        public string Accesorio { get => accesorio; set => accesorio = value; }
        public string Asignadoa { get => asignadoa; set => asignadoa = value; }
        public DateTime FechaCompra { get => fechaCompra; set => fechaCompra = value; }
        public DateTime FechaFabricacion { get => fechaFabricacion; set => fechaFabricacion = value; }
        public int Ubicacion { get => ubicacion; set => ubicacion = value; }
        public int Sede { get => sede; set => sede = value; }
        public DateTime FechaInspeccionInicial { get => fechaInspeccionInicial; set => fechaInspeccionInicial = value; }
        public int CbxAssigned { get => cbxAssigned; set => cbxAssigned = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
    }
}
