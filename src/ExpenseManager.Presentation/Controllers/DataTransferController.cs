// using ExpenseManager.Domain.Common.Errors;
// using ExpenseManager.Presentation.Contracts.DataTransfer;
// using MapsterMapper;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ExpenseManager.Presentation.Controllers;
//
// [Route("api/v{v:apiVersion}/data-transfer")]
// public class DataTransferController(ISender mediatr, IMapper mapper) : ApiController
// {
//     [HttpGet("export")]
//     public async Task<IActionResult> Export()
//     {
//         var userId = GetUserId();
//         if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
//             return Problem(statusCode: StatusCodes.Status401Unauthorized,
//                 title: userId.FirstError.Description);
//
//         throw new NotImplementedException();
//     }
//
//     [HttpPost("import")]
//     public async Task<IActionResult> Import(ImportRequest request)
//     {
//         throw new NotImplementedException();
//     }
// }

