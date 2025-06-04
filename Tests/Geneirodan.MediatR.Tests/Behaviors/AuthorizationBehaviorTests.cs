using Ardalis.Result;
using Geneirodan.Abstractions.Domain;
using Geneirodan.MediatR.Behaviors;
using Geneirodan.SampleApi.Application.Commands;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace Geneirodan.MediatR.Tests.Behaviors;

[TestSubject(typeof(AuthorizationBehavior<,>))]
public sealed class AuthorizationBehaviorTests : PipelineTest
{
    private readonly Mock<IUser> _userMock;

    public AuthorizationBehaviorTests(ApiFactory factory) : base(factory) => 
        _userMock = Scope.ServiceProvider.GetRequiredService<Mock<IUser>>();

    [Fact]
    public async Task Send_ShouldReturnUnauthorized_WhenUserIsNotAuthenticated()
    {
        _userMock.Setup(x => x.Id).Returns(null as Guid?);
        _userMock.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);
        var command = new AuthorizedCommand(false);
        var result = await Sender.Send(command, TestContext.Current.CancellationToken);
        result.Status.ShouldBe(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task Send_ShouldReturnForbidden_WhenUserIsNotInRole()
    {
        _userMock.Setup(x => x.Id).Returns(Guid.NewGuid());
        _userMock.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);
        var command = new AdminCommand(false);
        var result = await Sender.Send(command, TestContext.Current.CancellationToken);
        result.Status.ShouldBe(ResultStatus.Forbidden);
    }

    [Fact]
    public async Task Send_ShouldReturnSuccess_WhenNoAuthorizationRequired()
    {
        _userMock.Setup(x => x.Id).Returns(null as Guid?);
        _userMock.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);
        var command = new Command(false);
        var result = await Sender.Send(command, TestContext.Current.CancellationToken);
        result.Status.ShouldBe(ResultStatus.Ok);
    }

    [Fact]
    public async Task Send_ShouldReturnSuccess_WhenUserAuthenticated()
    {
        _userMock.Setup(x => x.Id).Returns(Guid.NewGuid());
        _userMock.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);
        var command = new AuthorizedCommand(false);
        var result = await Sender.Send(command, TestContext.Current.CancellationToken);
        result.Status.ShouldBe(ResultStatus.Ok);
    }

    [Fact]
    public async Task Send_ShouldReturnSuccess_WhenUserInRole()
    {
        _userMock.Setup(x => x.Id).Returns(Guid.NewGuid());
        _userMock.Setup(x => x.IsInRole("Admin")).Returns(true);
        var command = new AdminCommand(false);
        var result = await Sender.Send(command, TestContext.Current.CancellationToken);
        result.Status.ShouldBe(ResultStatus.Ok);
    }
}