namespace checkpoint.Views.Catalogs.Turns.Models
{
    public class Turnos
    {
        public int  IdTurno { get; set; }
        public string Nombre { get; set; }
        public bool Estatus { get; set; }
        public int HoraIncio { get; set; }
        public int HoraFin { get; set; }
    }
}
