namespace Klimatogrammen.Models.Domein
{
    public class VegetatieType
    {
        public int VegetatieTypeId { get; set; }

        public string Naam { get; set; }

        public string Foto { get; set; }

        public VegetatieType(string naam, string foto)
        {
            Naam = naam;
            Foto = foto;
        }

        public VegetatieType(){}
    }
}
