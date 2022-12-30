using SQLite;

namespace ScanDni.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string GeneroDesc { get; set; }
        public string GeneroImg { get; set; }
        public string Tramite { get; set; }
        public string Ejemplar { get; set; }
        public string FechaEmision { get; set; }
    }
}
