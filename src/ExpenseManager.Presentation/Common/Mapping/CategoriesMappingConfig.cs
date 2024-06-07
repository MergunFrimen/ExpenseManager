using ExpenseManager.Application.Categories.Commands.CreateCategory;
using ExpenseManager.Application.Categories.Commands.RemoveCategory;
using ExpenseManager.Application.Categories.Commands.UpdateCategory;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Presentation.Contracts.Categories;
using Mapster;

namespace ExpenseManager.Presentation.Common.Mapping;

public class CategoriesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CategoryResult, CategoryResponse>()
            .Map(dest => dest, src => src.Category);

        config.NewConfig<(CreateCategoryRequest Request, Guid UserId), CreateCategoryCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(UpdateCategoryRequest Request, Guid UserId), UpdateCategoryCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(RemoveCategoryRequest Request, Guid UserId), RemoveCategoryCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
    }
}