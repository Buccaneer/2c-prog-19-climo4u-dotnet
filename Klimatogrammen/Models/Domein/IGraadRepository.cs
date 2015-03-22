namespace Klimatogrammen.Models.Domein
{
    /// <summary>
    /// Een interface die door de GraadRepository wordt geïmplementeerd
    /// </summary>
    public interface IGraadRepository
    {
       
        Graad GeefGraad(int graad, int jaar);
    }
}