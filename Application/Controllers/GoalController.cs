using Application.ViewModels;
using Application.ViewModels.User;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : HomeController
    {
        private readonly IGoalService _goalService;
        private readonly IMapper _mapper;
        public GoalController(IGoalService goalService, IMapper mapper)
        {
            _goalService = goalService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<GoalResponse>> PostGoal([FromBody] GoalRequest req)
        {
            var goal = _mapper.Map<Goal>(req);
            var createdGoal = await _goalService.Create(goal);
            if (createdGoal == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<GoalResponse>(createdGoal);

            return Ok(result);

        }

        [HttpGet("GoalId")]
        public async Task<ActionResult<GoalResponse>> GetGoal(Guid GoalId)
        {
            var goal = await _goalService.GetGoal(GoalId);
            if (goal == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GoalResponse>(goal);
            return Ok(result);
        }

        [HttpPut("GoalId")]
        public async Task<ActionResult<GoalResponse>> PutGoal(Guid GoalId, [FromBody] GoalRequest req)
        {
            var goal = _mapper.Map<Goal>(req);
            var updatedGoal = await _goalService.Update(GoalId, goal);
            if (updatedGoal == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GoalResponse>(updatedGoal);
            return Ok(result);
        }

        [HttpDelete("GoalId")]
        public async Task<ActionResult<GoalResponse>> DeleteGoal(Guid GoalId)
        {
            var goal = await _goalService.Delete(GoalId);
            if (goal == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GoalResponse>(goal);
            return Ok(result);
        }

        [HttpGet("ByUser/{UserId:Guid}")]
        public async Task<ActionResult<List<GoalResponse>>> GetGoalByUser(Guid UserId)
        {
            var goal = await _goalService.GetGoalByUser(UserId);
            var result = _mapper.Map<GoalResponse>(goal);

            return Ok(result);
        }
    }
}
