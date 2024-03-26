using FluentValidation;
using FluentValidation.Results;

using MediatR;

using RoboControle.Application.Common.Behaviors;
using RoboControle.Application.Users.Commands.CreateUser;
using RoboControle.Application.Users.Common;

using TestCommon.Users;

namespace RoboControle.Application.UnitTests.Common.Behaviors;

public class ValidationBehaviorTests
{
    private readonly ValidationBehavior<CreateUserCommand, ErrorOr<UserResult>> _validationBehavior;
    private readonly IValidator<CreateUserCommand> _mockValidator;
    private readonly RequestHandlerDelegate<ErrorOr<UserResult>> _mockNextBehavior;

    public ValidationBehaviorTests()
    {
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<UserResult>>>();
        _mockValidator = Substitute.For<IValidator<CreateUserCommand>>();

        _validationBehavior = new(_mockValidator);
    }

    [Fact]
    public async Task InvokeValidationBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
    {
        // Arrange
        var createUserCommand = UserCommandFactory.CreateUserCommand();
        var user = UserFactory.CreateUserResult();

        _mockValidator
            .ValidateAsync(createUserCommand, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _mockNextBehavior.Invoke().Returns(user);

        // Act
        var result = await _validationBehavior.Handle(createUserCommand, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task InvokeValidationBehavior_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
    {
        // Arrange
        var createUserCommand = UserCommandFactory.CreateUserCommand();
        List<ValidationFailure> validationFailures = [new(propertyName: "foo", errorMessage: "bad foo")];

        _mockValidator
            .ValidateAsync(createUserCommand, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(validationFailures));

        // Act
        var result = await _validationBehavior.Handle(createUserCommand, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("foo");
        result.FirstError.Description.Should().Be("bad foo");
    }

    [Fact]
    public async Task InvokeValidationBehavior_WhenNoValidator_ShouldInvokeNextBehavior()
    {
        // Arrange
        var createUserCommand = UserCommandFactory.CreateUserCommand();
        var validationBehavior = new ValidationBehavior<CreateUserCommand, ErrorOr<UserResult>>();

        var userResult = UserFactory.CreateUserResult();
        _mockNextBehavior.Invoke().Returns(userResult);

        // Act
        var result = await validationBehavior.Handle(createUserCommand, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(userResult);
    }
}