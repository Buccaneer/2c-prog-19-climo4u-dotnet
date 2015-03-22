namespace Klimatogrammen.Models.Domein
{
    /// <summary>
    /// VegetatieType is een klasse die alles bijhoudt van het vegetatietype.
    /// </summary>
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
