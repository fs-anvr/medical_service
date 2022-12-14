using Domain;

namespace UnitTests;

public class ScheduleServiceTests
{
    private readonly ScheduleService _scheduleService;
    private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;

    public ScheduleServiceTests()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _scheduleService = new ScheduleService(_scheduleRepositoryMock.Object);
    }

    [Fact]
    public void GetScheduleOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        var date = new DateOnly(1, 1, 1);
        int doctorID = 54;
        _scheduleRepositoryMock.Setup(repository => repository.GetSchedule(doctorID, date))
            .Returns(() => new Schedule(doctorID, date, new TimeOnly(1, 1, 1),
            new TimeOnly(1, 1, 1)));
        
        //Act
        var result = _scheduleService.GetSchedule(doctorID, date);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void GetScheduleNotFound_ShouldFail()
    {
        //Arrange
        string expected_error = "Schedule not found";
        var date = new DateOnly(1, 1, 1);
        int doctorID = 54;
        _scheduleRepositoryMock.Setup(repository => repository.GetSchedule(doctorID, date))
            .Returns(() => null);
        
        //Act
        var result = _scheduleService.GetSchedule(doctorID, date);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void AddScheduleOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        int doctorID = 54;
        var date = new DateOnly(1, 1, 1);
        var dayStart = new TimeOnly(1, 1, 1);
        var dayEnd = new TimeOnly(1, 1, 1);
        var form = new ScheduleForm(doctorID, date, dayStart, dayEnd);
        _scheduleRepositoryMock.Setup(repository => repository.AddSchedule(form))
            .Returns(() => new Schedule(doctorID, date, dayStart, dayEnd));
        
        //Act
        var result = _scheduleService.AddSchedule(form);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void AddScheduleOtherError_ShouldFail()
    {
        //Arrange
        string expected_error = "Failed to add schedule";
        int doctorID = 54;
        var date = new DateOnly(1, 1, 1);
        var dayStart = new TimeOnly(1, 1, 1);
        var dayEnd = new TimeOnly(1, 1, 1);
        var form = new ScheduleForm(doctorID, date, dayStart, dayEnd);
        _scheduleRepositoryMock.Setup(repository => repository.AddSchedule(form))
            .Returns(() => null);
        
        //Act
        var result = _scheduleService.AddSchedule(form);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void ChangeScheduleOk_ShouldOk()
    {
        //Arrange
        string expected_error = string.Empty;
        int doctorID = 123;
        DateOnly actualDate = new DateOnly(2022, 2, 22);
        DateOnly recentDate = new DateOnly(450, 1, 1);
        TimeOnly start = new TimeOnly(0, 0, 0);
        TimeOnly end = new TimeOnly(23, 59, 01);
        var actual = new ScheduleForm(doctorID, actualDate, start, end);
        var recent = new ScheduleForm(doctorID, recentDate, start, end);
        _scheduleRepositoryMock.Setup(repository => repository.ChangeSchedule(actual, recent))
            .Returns(() => new Schedule(doctorID, recentDate, start, end));
        
        //Act
        var result = _scheduleService.ChangeSchedule(actual, recent);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal(expected_error, result.Error);
    }

    [Fact]
    public void ChangeScheduleOtherError_ShouldFail()
    {
        //Arrange
        string expected_error = "Failed to change schedule";
        int doctorID = 123;
        DateOnly actualDate = new DateOnly(2022, 2, 22);
        DateOnly recentDate = new DateOnly(450, 1, 1);
        TimeOnly start = new TimeOnly(0, 0, 0);
        TimeOnly end = new TimeOnly(23, 59, 01);
        var actual = new ScheduleForm(doctorID, actualDate, start, end);
        var recent = new ScheduleForm(doctorID, recentDate, start, end);
        _scheduleRepositoryMock.Setup(repository => repository.ChangeSchedule(actual, recent))
            .Returns(() => null);
        
        //Act
        var result = _scheduleService.ChangeSchedule(actual, recent);
        
        //Assert
        Assert.True(result.IsFail);
        Assert.Equal(expected_error, result.Error);
    }
}