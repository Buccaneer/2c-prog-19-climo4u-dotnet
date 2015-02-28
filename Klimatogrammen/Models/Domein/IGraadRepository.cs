namespace Klimatogrammen.Models.Domein
{
    public interface IGraadRepository
    {
        Graad GeefGraad(int graad, int jaar);
    }
}