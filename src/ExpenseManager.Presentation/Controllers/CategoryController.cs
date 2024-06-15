using ExpenseManager.Application.Categories.Commands.CreateCategory;
using ExpenseManager.Application.Categories.Commands.RemoveCategories;
using ExpenseManager.Application.Categories.Commands.UpdateCategory;
using ExpenseManager.Application.Categories.Queries.GetCategory;
using ExpenseManager.Application.Categories.Queries.SearchCategories;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Presentation.Contracts.Categories;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("api/v{v:apiVersion}/categories")]
public class CategoryController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var command = mapper.Map<CreateCategoryCommand>((request, userId.Value));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Created($"api/v1/categories/{value.Category.Id}", mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var command = mapper.Map<UpdateCategoryCommand>((request, id, userId.Value));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(RemoveCategoriesRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var command = mapper.Map<RemoveCategoriesCommand>((request, userId.Value));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<List<CategoryResponse>>(value)),
            Problem
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var query = new GetCategoryQuery(id, userId.Value);
        var result = await mediatr.Send(query);

        return result.Match(
            value => Ok(mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> Search(SearchCategoriesRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var query = mapper.Map<SearchCategoriesQuery>((request, userId.Value));
        var result = await mediatr.Send(query);

        return result.Match(
            value => Ok(mapper.Map<List<CategoryResponse>>(value)),
            Problem
        );
    }
}