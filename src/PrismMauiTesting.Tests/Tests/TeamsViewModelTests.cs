using System;
using Microsoft.Extensions.Logging;
using Prism.Navigation.Builder;
using PrismMauiTesting.Abstractions;
using PrismMauiTesting.Models;
using PrismMauiTesting.ViewModels;

namespace PrismMauiTesting.Tests;

public class TeamsViewModelTests : FixtureBase<TeamsViewModel>
{
    #region Setup

    private readonly Mock<ILogger<TeamsViewModel>> logger;
    private readonly Mock<INavigationService> navigationService;
    private readonly Mock<ITeamService> teamService;

    public TeamsViewModelTests()
    {
        logger = new Mock<ILogger<TeamsViewModel>>();
        navigationService = new Mock<INavigationService>();
        teamService = new Mock<ITeamService>();
    }

    protected override TeamsViewModel CreateSystemUnderTest()
    {
        return new TeamsViewModel(
            logger.Object,
            navigationService.Object,
            teamService.Object);
    }

    #endregion Setup

    #region Tests

    [Fact]
    public void Constructor_WhenCalled_ShouldGetTeams()
    {
        // Arrange
        teamService.Setup(m => m.GetLecTeams())
            .Returns(new List<LecTeam>());

        // Act
        _ = Sut;

        // Assert
        Assert.NotNull(Sut.Teams);
        Assert.NotNull(Sut.TeamSelectedCommand);
    }

    [Fact]
    public void TeamSelectedCommand_WhenTeamNull_ShouldNotNavigate()
    {
        // Arrange
        teamService.Setup(m => m.GetLecTeams())
            .Returns(new List<LecTeam>());

        // Act
        Sut.TeamSelectedCommand.Execute(null);

        // Assert
        logger.Verify(
            m => m.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Debug),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Team was null" && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact]
    public void TeamSelectedCommand_WhenTeamSelected_ShouldNavigateToTeamPage()
    {
        // Arrange
        teamService.Setup(m => m.GetLecTeams())
            .Returns(new List<LecTeam>());

        var skGaming = new LecTeam()
        {
            Name = "SK Gaming",
            Titles = 0,
            OperatingCountry = "Germany",
            LogoUrl = "https://pbs.twimg.com/profile_images/1636005067240222721/WZG2NIat_400x400.jpg",
            Joined = new DateTime(2013, 01, 01),
            IsActive = true,
        };

        var mockResult = new Mock<INavigationResult>();

        mockResult.SetupGet(m => m.Success)
            .Returns(true);

        navigationService.Setup(m => m.NavigateAsync(It.IsAny<Uri>(), It.IsAny<INavigationParameters>()))
            .ReturnsAsync(mockResult.Object);

        // Act
        Sut.TeamSelectedCommand.Execute(skGaming);

        // Assert
        navigationService.Verify(
            m => m.NavigateAsync(
                It.Is<string>(s => s.Equals("TeamPage")),
                It.Is<INavigationParameters>(np => np.ContainsKey("SelectedTeam"))),
            Times.Once());

        logger.Verify(
            m => m.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Debug),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Team was null" && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Never);
    }

    #endregion Tests
}

