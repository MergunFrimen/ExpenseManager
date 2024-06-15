using ExpenseManager.Application.Categories.Commands.CreateCategory;
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

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request)
    // {
    //     var userId = GetUserId();
    //     if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
    //         return Problem(statusCode: StatusCodes.Status401Unauthorized,
    //             title: userId.FirstError.Description);
    //
    //     var command = mapper.Map<UpdateCategoryCommand>((request, id, userId.Value));
    //     var result = await mediatr.Send(command);
    //
    //     return result.Match(
    //         onValue: value => Ok(mapper.Map<CategoryResponse>(value)),
    //         onError: Problem
    //     );
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Remove(Guid id)
    // {
    //     var userId = GetUserId();
    //     if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
    //         return Problem(statusCode: StatusCodes.Status401Unauthorized,
    //             title: userId.FirstError.Description);
    //
    //     var command = new RemoveCategoriesCommand(id, userId.Value);
    //     var result = await mediatr.Send(command);
    //
    //     return result.Match(
    //         onValue: value => Ok(mapper.Map<CategoryResponse>(value)),
    //         onError: Problem
    //     );
    // }
    //
    // [HttpGet("search")]
    // public async Task<IActionResult> Search(string name)
    // {
    //     var userId = GetUserId();
    //     if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
    //         return Problem(statusCode: StatusCodes.Status401Unauthorized,
    //             title: userId.FirstError.Description);
    //
    //     var query = new SearchCategoriesQuery(userId.Value, name);
    //     var result = await mediatr.Send(query);
    //
    //     return result.Match(
    //         onValue: value => Ok(mapper.Map<List<CategoryResponse>>(value)),
    //         onError: Problem
    //     );
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<IActionResult> Get(Guid id)
    // {
    //     var userId = GetUserId();
    //     if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
    //         return Problem(statusCode: StatusCodes.Status401Unauthorized,
    //             title: userId.FirstError.Description);
    //
    //     var query = new GetCategoryQuery(id, userId.Value);
    //     var result = await mediatr.Send(query);
    //
    //     return result.Match(
    //         onValue: value => Ok(mapper.Map<CategoryResponse>(value)),
    //         onError: Problem
    //     );
    // }
}