using System;

namespace LogicBo
{
    [Serializable]
    public class InspectionIzaje
    {
        private int idestado;
        private int idaccion;
        private int idinspector;
        private DateTime fechaInspeccion;
        private string precinto;
        private int idEquipo;

        public int IdEstado { get => idestado; set => idestado = value; }
        public int IdAccion { get => idaccion; set => idaccion = value; }
        public int IdInspector { get => idinspector; set => idinspector = value; }
        public DateTime FechaInspeccion { get => fechaInspeccion; set => fechaInspeccion = value; }
        public string Precinto { get => precinto; set => precinto = value; }
        public int IdEquipo { get => idEquipo; set => idEquipo = value; }
    }
}
