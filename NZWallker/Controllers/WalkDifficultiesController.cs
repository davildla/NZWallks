using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWallker.API.Models.DTO;
using NZWallker.API.Reposetories;
using System.Data;

namespace NZWallker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyReposetory walkDifficultyReposetory;
        private readonly IMapper mapper;
        public WalkDifficultiesController(IWalkDifficultyReposetory walkDifficultyReposetory, IMapper mapper)
        {
            this.walkDifficultyReposetory = walkDifficultyReposetory;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllGetWalkDiffcultiesAsync()
        {
            var walkDifficulties = await walkDifficultyReposetory.getAllAsync();
            var res = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulties); // regionsDTO

            return Ok(res);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "reader")]
        [ActionName("GetWalkDiffculty")]
        public async Task<IActionResult> GetWalkDiffculty(Guid id)
        {
            var walkDiifculty = await walkDifficultyReposetory.getAsync(id);

            if (walkDiifculty == null)
            {
                return NotFound();
            }
            else
            {
                var res = mapper.Map<Models.DTO.WalkDifficulty>(walkDiifculty);
                return Ok(res);
            }


        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficulty addWalkDifficulty)
        {
            // Request(DTO) to Domain modal
            var walkDiffculty = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficulty.Code,
            };

            // pass details to Reposetory
            walkDiffculty = await walkDifficultyReposetory.addAsync(walkDiffculty);

            // Convert back to DTO
            var res = new Models.DTO.WalkDifficulty()
            {
                Id = walkDiffculty.Id,
                Code = walkDiffculty.Code,

            };

            return CreatedAtAction(nameof(GetWalkDiffculty), new { id = res.Id }, res);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkDifficultyById(Guid id)
        {
            // Get Region from Db
            var walkDifficulty = await walkDifficultyReposetory.deleteAsync(id);

            // if null NotFound
            if (walkDifficulty == null)
            {
                return NotFound();
            }

            // Convert res to DTO
            var res = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            // Return Ok response
            return Ok(res);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.AddWalkDifficulty updateWalkDifficultyRequest)
        {
            // Convert DTO to Domain model
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };

            // Update Region using reposetory
            walkDifficulty = await walkDifficultyReposetory.updateAsync(id, walkDifficulty);

            // if null return NotFound
            if (walkDifficulty == null)
            {
                return NotFound();
            }

            // Convert to DTO
            var res = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            // return Ok response
            return Ok(res);
        }
    }
}
