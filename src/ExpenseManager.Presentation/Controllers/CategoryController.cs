using ExpenseManager.Application.Categories.Commands.CreateCategory;
using ExpenseManager.Application.Categories.Commands.RemoveCategory;
using ExpenseManager.Application.Categories.Commands.UpdateCategory;
using ExpenseManager.Application.Categories.Queries;
using ExpenseManager.Application.Categories.Queries.GetCategory;
using ExpenseManager.Application.Categories.Queries.ListCategories;
using ExpenseManager.Presentation.Contracts.Categories;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("/categories")]
public class CategoryController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {
        var userId = GetUserId();
        var command = mapper.Map<CreateCategoryCommand>((request, userId));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCategoryRequest request)
    {
        var userId = GetUserId();
        var command = mapper.Map<UpdateCategoryCommand>((request, userId));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(RemoveCategoryRequest request)
    {
        var userId = GetUserId();
        var command = mapper.Map<RemoveCategoryCommand>((request, userId));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var userId = GetUserId();
        var query = new ListCategoriesQuery(userId);
        var result = await mediatr.Send(query);

        return result.Match(
            value => Ok(mapper.Map<List<CategoryResponse>>(value)),
            Problem
        );
    }
    
    [HttpGet("{:id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = GetUserId();
        var query = new GetCategoryQuery(id, userId);
        var result = await mediatr.Send(query);

        return result.Match(
            value => Ok(mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }
}