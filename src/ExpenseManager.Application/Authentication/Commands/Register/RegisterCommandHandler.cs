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

        // Hash the password
        var hashedPassword = passwordHasher.Hash(command.Password);

        // Create the user
        var newUser = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            hashedPassword
        );

        // Add the user to the database
        var createdUser = await userRepository.AddAsync(newUser, cancellationToken);
        if (createdUser.IsError)
            return createdUser.Errors;

        // Generate a token for the user
        var token = tokenGenerator.GenerateToken(createdUser.Value);

        // Create default categories for the user
        await CreateDefaultCategories(createdUser.Value, cancellationToken);

        return new AuthenticationResult(createdUser.Value, token);
    }

    private async Task CreateDefaultCategories(User user, CancellationToken cancellationToken)
    {
        List<Category> categories =
        [
            Category.Create(null, "Food", user),
            Category.Create(null, "Transport", user)
            // Name.Create(null, "Entertainment", user),
            // Name.Create(null, "Health", user),
            // Name.Create(null, "Clothing", user),
            // Name.Create(null, "Rent", user),
            // Name.Create(null, "Other", user)
        ];

        foreach (var category in categories) await categoryRepository.AddAsync(category, cancellationToken);
    }
}