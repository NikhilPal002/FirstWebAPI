using System.Globalization;
using System.Net.Http.Headers;
using FirstWebAPI.Data;
using FirstWebAPI.Models.Domain;
using FirstWebAPI.Models.DTO;
using FirstWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext context;
        private readonly IRegionRepository regionRepository;

        public RegionsController(WalksDbContext context, IRegionRepository regionRepository)
        {
            this.context = context;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - domain model
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map domain model to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // Return DTOs
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // var region = context.Regions.Find(id);       // it can only be used on id or primary key
            var regionsDomain = await regionRepository.GetByIdAsync(id);   // this can be used on any parameter.

            if (regionsDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto){

            // Map DTO to domain model
            var regionDomainModel = new Region{
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain model to create region 
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            await context.SaveChangesAsync();

            // Map domain model back to dto
            var regionDto = new RegionDto{
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto );   
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody]  UpdateRegionRequestDto updateRegionRequestDto){

            // Map DTO to domain model
            var regionsDomain = new Region{
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            
            regionsDomain = await regionRepository.UpdateAsync(id,regionsDomain);
            if (regionsDomain == null)
            {
                return NotFound();
            } 

            // Convert Domain model to DTO
            var regionDto = new RegionDto{
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id){

            var region = await regionRepository.DeleteAsync(id);

            if(region == null){
                return NotFound();
            }

            var regionDto = new RegionDto{
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}