using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UserTasksAPI.Models;
using UserTasksAPI.Repositories;

namespace UserTasksAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>();


        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.ID == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            task.ID = tasks.Count + 1;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.ID }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.ID == id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Assignee = updatedTask.Assignee;
            task.DueDate = updatedTask.DueDate;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.ID == id);
            if (task == null) return NotFound();

            tasks.Remove(task);
            return NoContent();
        }



        [HttpGet("expired")]
        public async Task<IActionResult> GetExpiredTasks()
        {
            var tasks = await _taskRepository.GetExpiredTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveTasks()
        {
            var tasks = await _taskRepository.GetActiveTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("fromdate/{date}")]
        public async Task<IActionResult> GetTasksFromDate(DateTime date)
        {
            var tasks = await _taskRepository.GetTasksFromDateAsync(date);
            return Ok(tasks);
        }
    }
}
