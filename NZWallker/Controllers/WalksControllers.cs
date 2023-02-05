using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWallker.API.Models.Domain;
using NZWallker.API.Reposetories;
using System.Data;

namespace NZWallker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksControllers : Controller
    {
        private readonly IWalkReposetory walkReposetory;
        private readonly IMapper mapper;

        public WalksControllers(IWalkReposetory walkReposetory, IMapper mapper)
        {
            this.walkReposetory = walkReposetory;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await walkReposetory.getAllAsync();
            var res = mapper.Map<List<Models.DTO.Walk>>(walks); // need to change

            return Ok(res);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walkReposetory.getAsync(id);

            if (walk == null)
            {
                return NotFound();
            }
            else
            {
                var res = mapper.Map<Models.DTO.Walk>(walk);
                return Ok(res);
            }


        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Request(DTO) to Domain modal
            var walk = new Models.Domain.Walk()
            {
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
                RegionId = addWalkRequest.RegionId,
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
            };

            // pass details to Reposetory
            walk = await walkReposetory.addAsync(walk);

            // Convert back to DTO
            var res = new Models.DTO.Walk()
            {
                Id = walk.Id,
                WalkDifficultyId = walk.WalkDifficultyId,
                RegionId = walk.RegionId,
                Length = walk.Length,
                Name = walk.Name,
            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = res.Id }, res);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeletWalkById(Guid id)
        {
            // Get Region from Db
            var walk = await walkReposetory.deleteAsync(id);

            // if null NotFound
            if (walk == null)
            {
                return NotFound();
            }

            // Convert res to DTO
            var res = mapper.Map<Models.DTO.Walk>(walk);

            // Return Ok response
            return Ok(res);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalk updateWalknRequest)
        {
            // Convert DTO to Domain model
            var walk = new Models.Domain.Walk()
            {
                WalkDifficultyId = updateWalknRequest.WalkDifficultyId,
                RegionId = updateWalknRequest.RegionId,
                Length = updateWalknRequest.Length,
                Name = updateWalknRequest.Name,
            };

            // Update Region using reposetory
            walk = await walkReposetory.updateAsync(id, walk);

            // if null return NotFound
            if (walk == null)
            {
                return NotFound();
            }

            // Convert to DTO
            var res = mapper.Map < Models.DTO.Walk>(walk);

            // return Ok response
            return Ok(res);
        }

    }
}
