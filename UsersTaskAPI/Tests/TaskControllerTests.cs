using Microsoft.AspNetCore.Mvc;
using Moq;
using UserTasksAPI.Controllers;
using UserTasksAPI.Repositories;
using UserTasksAPI.Models;
using Xunit;

namespace UsersTaskAPI.Controllers
{
    public class TaskControllerTests: ControllerBase
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _controller = new TaskController(_mockTaskRepository.Object);
        }

        [Fact]
        public async Task GetExpiredTasks_ReturnsOkResult()
        {
           
            var tasks = new List<TaskItem>
        {
            new TaskItem { Title = "Expired Task", DueDate = DateTime.Now.AddDays(-1) }
        };
            _mockTaskRepository.Setup(repo => repo.GetExpiredTasksAsync()).ReturnsAsync(tasks);

           
            var result = await _controller.GetExpiredTasks();

           
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Task>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetActiveTasks_ReturnsOkResult()
        {
           
            var tasks = new List<TaskItem>
        {
            new TaskItem { Title = "Active Task", DueDate = DateTime.Now.AddDays(1) }
        };
            _mockTaskRepository.Setup(repo => repo.GetActiveTasksAsync()).ReturnsAsync(tasks);

           
            var result = await _controller.GetActiveTasks();

           
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Task>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetTasksFromDate_ReturnsOkResult()
        {
           
            var date = DateTime.Now.AddDays(-1);
            var tasks = new List<TaskItem>
        {
            new TaskItem { Title = "Task from Date", DueDate = DateTime.Now.AddDays(1) }
        };
            _mockTaskRepository.Setup(repo => repo.GetTasksFromDateAsync(date)).ReturnsAsync(tasks);

           
            var result = await _controller.GetTasksFromDate(date);

           
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Task>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }

}
