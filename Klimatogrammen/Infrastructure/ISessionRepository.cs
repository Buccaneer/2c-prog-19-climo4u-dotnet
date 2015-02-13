namespace Klimatogrammen.Infrastructure {
    public interface ISessionRepository {
        object this[string sleutel] { get; set; }
        bool BestaatSleutel(string sleutel);
        void VerwijderSleutel(string sleutel);
    }
}