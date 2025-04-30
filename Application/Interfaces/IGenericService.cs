namespace Application.Interfaces
{
    public interface IGenericService<SaveDTO, DTO, Model>
        where SaveDTO : class
        where DTO : class
        where Model : class
    {
        Task<List<DTO>> GetAllViewModelService(string userId);

        Task<SaveDTO> GetByIdSaveViewModelService(int id);

        Task<SaveDTO> AddAService(SaveDTO entity);

        Task UpdateService(SaveDTO entity);
        Task DeleteService(int id);
    }
}