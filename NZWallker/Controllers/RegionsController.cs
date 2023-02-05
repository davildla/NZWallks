using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWallker.API.Models.DTO;
using NZWallker.API.Reposetories;

namespace NZWallker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionReposetory regionReposetory;
        private readonly IMapper mapper;

        public RegionsController(IRegionReposetory regionReposetory, IMapper mapper)
        {
            this.regionReposetory = regionReposetory;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "reader")]

        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionReposetory.getAllAsync();
            var res = mapper.Map<List<Models.DTO.Region>>(regions); // regionsDTO

            return Ok(res);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "reader")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionReposetory.getAsync(id);

            if (region == null) 
            {
                return NotFound();
            } else
            {
                var res = mapper.Map<Models.DTO.Region>(region);
                return Ok(res);
            }

            
        }

        [HttpPost]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // Request(DTO) to Domain modal
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,
            };

            // pass details to Reposetory
            region = await regionReposetory.addAsync(region);

            // Convert back to DTO
            var res = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = res.Id }, res);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult> DeleteRegionById(Guid id)
        {
            // Get Region from Db
            var region = await regionReposetory.deleteAsync(id);

            // if null NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert res to DTO
            var res = mapper.Map<Models.DTO.Region>(region);

            // Return Ok response
            return Ok(res);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegion updateRegionRequest)
        {
            // Convert DTO to Domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population,
            };

            // Update Region using reposetory
            region = await regionReposetory.updateAsync(id, region);   

            // if null return NotFound
            if(region == null)
            {
                return NotFound();
            }

            // Convert to DTO
            var res = mapper.Map<Models.DTO.Region>(region);

            // return Ok response
            return Ok(res);
        }
    }
}
