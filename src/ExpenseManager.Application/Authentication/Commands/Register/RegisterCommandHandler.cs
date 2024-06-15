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
        // Check if a user with the same email already exists
        var user = await userRepository.FindAsync(u => u.Email == command.Email, cancellationToken);
        if (user.IsError)
            return user.Errors;
        if (user.Value.Count != 0)
            return Errors.User.DuplicateEmail;

        var hashedPassword = passwordHasher.Hash(command.Password);

        var newUser = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            hashedPassword
        );
        
        var createdUser = await userRepository.AddAsync(newUser, cancellationToken);
        if (createdUser.IsError)
            return createdUser.Errors;

        var token = tokenGenerator.GenerateToken(createdUser.Value);

        return new AuthenticationResult(createdUser.Value, token);
    }

    // private List<Category> DefaultCategories(User user)
    // {
    //     return
    //     [
    //         Category.Create(null, "Salary", user),
    //         Category.Create(null, "Food", user),
    //         Category.Create(null, "Entertainment", user),
    //     ];
    // }
}

