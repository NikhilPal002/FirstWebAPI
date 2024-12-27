using System.Globalization;
using System.Net.Http.Headers;
using AutoMapper;
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
        private readonly IMapper mapper;

        public RegionsController(WalksDbContext context, IRegionRepository regionRepository,IMapper mapper)
        {
            this.context = context;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - domain model
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map domain model to dto
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

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

           var regionDto = mapper.Map<RegionDto>(regionsDomain);

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto){

            // Map DTO to domain model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain model to create region 
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map domain model back to dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto );   
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody]  UpdateRegionRequestDto updateRegionRequestDto){

            // Map DTO to domain model
            var regionsDomain = mapper.Map<Region>(updateRegionRequestDto);
            
            regionsDomain = await regionRepository.UpdateAsync(id,regionsDomain);
            if (regionsDomain == null)
            {
                return NotFound();
            } 

            // Convert Domain model to DTO
            var regionDto = mapper.Map<RegionDto>(regionsDomain);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id){

            var regionDomain = await regionRepository.DeleteAsync(id);

            if(regionDomain == null){
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }
    }
}