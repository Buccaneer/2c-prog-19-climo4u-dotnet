namespace Klimatogrammen.Models.Domein {
    public interface IDeterminatieTabelRepository {
        DeterminatieTabel GeefTabel(string naam);
    }
}