

using Application.Interfaces;
using Application.IRepository;
using AutoMapper;

namespace Application.Service
{
    public class GenericService<SaveViewModel, ViewModel, Model> : IGenericService<SaveViewModel, ViewModel, Model>
         where SaveViewModel : class
         where ViewModel : class
         where Model : class
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<SaveViewModel> AddAService(SaveViewModel vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            entity = await _repository.Add(entity);

            SaveViewModel entityVm = _mapper.Map<SaveViewModel>(entity);

            return entityVm;
        }

        public async  Task DeleteService(int id)
        {
            var product = await _repository.GetById(id);
            if (product != null)
            {
                await _repository.Delete(product);
            }
        }

        public async Task<List<ViewModel>> GetAllViewModelService(string userId)
        {
            var entityList = await _repository.GetAll(userId);

            return _mapper.Map<List<ViewModel>>(entityList);
        }

        public async  Task<SaveViewModel> GetByIdSaveViewModelService(int id)
        {
            var entity = await _repository.GetById(id);

            SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);
            return vm;
        }

        public async Task UpdateService(SaveViewModel vm)
        {
            Model entity = _mapper.Map<Model>(vm);
            await _repository.Update(entity);
        }
    }
}
