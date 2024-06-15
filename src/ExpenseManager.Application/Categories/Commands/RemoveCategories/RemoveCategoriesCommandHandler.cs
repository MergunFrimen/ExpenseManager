// using ErrorOr;
// using ExpenseManager.Application.Categories.Common;
// using ExpenseManager.Application.Common.Interfaces.Cqrs;
// using ExpenseManager.Application.Common.Interfaces.Persistence;
//
// namespace ExpenseManager.Application.Categories.Commands.RemoveCategories;
//
// public class RemoveCategoriesCommandHandler(ICategoryRepository categoryRepository)
//     : ICommandHandler<RemoveCategoriesCommand, List<CategoryResult>>
// {
//     public async Task<ErrorOr<List<CategoryResult>>> Handle(RemoveCategoriesCommand command, CancellationToken cancellationToken)
//     {
//         var categories = await categoryRepository.FindAsync(
//             category => command.CategoryIds.Contains(category.Id) && category.UserId == command.UserId,
//             cancellationToken);
//
//         if (categories.IsError)
//             return categories.Errors;
//         
//         var removedCategories = await categoryRepository.RemoveRangeAsync(categories.Value, cancellationToken);
//         
//         if (removedCategories.IsError)
//             return removedCategories.Errors;
//         
//         return removedCategories.Value.Select(category => new CategoryResult(category)).ToList();
//     }
// }

