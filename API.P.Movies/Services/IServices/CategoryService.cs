using API.P.Movies.DAL.Models;
using API.P.Movies.DAL.Models.Dtos;
using API.P.Movies.Repository.IRepository;
using AutoMapper;

namespace API.P.Movies.Services.IServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            var categoryExists = await _categoryRepository.CategoryExistsByNameAsync(categoryCreateDto.Name);

            if (categoryExists)
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre '{categoryCreateDto.Name}'");
            }

            //Mappear de DTO a la entidad/modelo Category
            var category = _mapper.Map<Category>(categoryCreateDto);

            //Crear la categoría en la base de datos
            var categoryCreated = await _categoryRepository.CreateCategoryAsync(category);

            if (!categoryCreated)
            {
                throw new InvalidOperationException("Ocurrió un error al crear la categoría");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            var categories =  await _categoryRepository.GetCategoriesAsync();//Se llama el metodo desde la capa de repositorio

            return _mapper.Map<ICollection<CategoryDto>>(categories); //Mapeo la lista de categorias a su DTO
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            //Obtiene la categoria del repo
           var category = await _categoryRepository.GetCategoryAsync(id);
            
            //Mapea la categoria a su DTO
           return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
