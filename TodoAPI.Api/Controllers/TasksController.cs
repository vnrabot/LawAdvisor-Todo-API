using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoAPI.Application.Interfaces;
using TodoAPI.Application.Services;

namespace TodoAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int limit = 50)
        {
            var tasks = await _taskService.GetTaskAsync(page, limit);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string title, [FromQuery] string details)
        {
            var task = await _taskService.AddTaskAsync(title, details);
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] string title, [FromQuery] string details)
        {
            var task = await _taskService.UpdateTaskAsync(id, title, details);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/reorder")]
        public async Task<IActionResult> Reorder(int id, [FromQuery] string prevRank = "", [FromQuery] string nextRank = "")
        {
            var success = await _taskService.ReorderTaskAsync(id, prevRank, nextRank);
            if (!success) return NotFound();
            return Ok(new { Message = "Task reordered successfully" });
        }
    }
}