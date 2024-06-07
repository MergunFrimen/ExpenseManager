using ErrorOr;
using ExpenseManager.Application.Authentication.Common;
using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    IJwtTokenGenerator tokenGenerator,
    IUserRepository userRepository,
    ICategoryRepository categoryRepository,
    IPasswordHasher passwordHasher)
    : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user is not null)
            return Errors.User.DuplicateEmail;

        var hashedPassword = passwordHasher.Hash(command.Password);

        var newUser = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            hashedPassword
        );
        
        await userRepository.AddAsync(newUser, cancellationToken);
        
        await AddDefaultCategories(newUser, cancellationToken);

        var token = tokenGenerator.GenerateToken(newUser);

        return new AuthenticationResult(newUser, token);
    }
    
    private async Task AddDefaultCategories(User user, CancellationToken cancellationToken)
    {
        var categories = new List<Category>
        {
            Category.Create(null,user.Id,"Salary"),
            Category.Create(null,user.Id,"Food"),
            Category.Create(null,user.Id,"Entertainment"),
        };

        foreach (var category in categories)
        {
            await categoryRepository.AddAsync(category, cancellationToken);
        }
    }
}