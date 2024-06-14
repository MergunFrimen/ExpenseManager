using ExpenseManager.Application.Categories.Commands.CreateCategory;
using ExpenseManager.Application.Categories.Commands.RemoveCategory;
using ExpenseManager.Application.Categories.Commands.UpdateCategory;
using ExpenseManager.Application.Categories.Queries.FindCategories;
using ExpenseManager.Application.Categories.Queries.GetCategory;
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
            value => Ok(mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCategoryRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var command = mapper.Map<UpdateCategoryCommand>((request, userId.Value));
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
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var command = mapper.Map<RemoveCategoryCommand>((request, userId.Value));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<CategoryResponse>(value)),
            Problem
        );
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> Search(string name)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var query = new FindCategoriesQuery(userId.Value, name);
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
}