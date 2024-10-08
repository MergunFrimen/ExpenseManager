using ExpenseManager.Application.Categories.Commands.CreateCategory;
using ExpenseManager.Application.Categories.Commands.RemoveCategories;
using ExpenseManager.Application.Categories.Commands.UpdateCategory;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Categories.Queries.SearchCategories;
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

        config.NewConfig<(UpdateCategoryRequest Request, Guid Id, Guid UserId), UpdateCategoryCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(RemoveCategoriesRequest Request, Guid UserId), RemoveCategoriesCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(SearchCategoriesRequest Request, Guid UserId), SearchCategoriesQuery>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
    }
}