public interface IUniversidadRepository {
    Task<IEnumerable<Universidad>> GetAll();
    Task<Universidad> GetById(int id);
    Task Add(Universidad universidad);
    Task Update(Universidad universidad);
    Task Delete(int id);
}