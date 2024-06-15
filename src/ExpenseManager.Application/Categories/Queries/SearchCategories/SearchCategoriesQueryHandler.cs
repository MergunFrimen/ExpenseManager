// using ErrorOr;
// using ExpenseManager.Application.Categories.Common;
// using ExpenseManager.Application.Common.Interfaces.Cqrs;
// using ExpenseManager.Application.Common.Interfaces.Persistence;
//
// namespace ExpenseManager.Application.Categories.Queries.SearchCategories;
//
// public class SearchCategoriesQueryHandler(ICategoryRepository categoryRepository)
//     : IQueryHandler<SearchCategoriesQuery, List<CategoryResult>>
// {
//     public async Task<ErrorOr<List<CategoryResult>>> Handle(SearchCategoriesQuery query,
//         CancellationToken cancellationToken)
//     {
//         var categories = await categoryRepository.FindAsync(
//             category => category.UserId == query.UserId &&
//                         category.Name.ToLower().Contains(query.Name.ToLower()), cancellationToken);
//
//         return categories.Match(
//             value => value.Select(category => new CategoryResult(category)).ToList(),
//             ErrorOr<List<CategoryResult>>.From
//         );
//     }
// }

