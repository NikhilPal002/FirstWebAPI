using AutoMapper;
using FirstWebAPI.CustomAction;
using FirstWebAPI.Data;
using FirstWebAPI.Models.Domain;
using FirstWebAPI.Models.DTO;
using FirstWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase{
        private readonly WalksDbContext context;
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(WalksDbContext context, IWalkRepository walkRepository ,IMapper mapper)
        {
            this.context = context;
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto ){
            
            // map dto to d omain model
            var walkDomain = mapper.Map<Walk>(addWalksRequestDto);

            await walkRepository.CreateAsync(walkDomain);

            // map domain to dto
            var  walkDto = mapper.Map<WalkDto>(walkDomain);          
            return Ok(walkDto);
        } 

        [HttpGet]
        public async Task<IActionResult> GetAll(){

            var walkDomain = await walkRepository.GetAllAsync();

            var walkDto = mapper.Map<List<WalkDto>>(walkDomain);

            return Ok(walkDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id){
            var walkDomain = await walkRepository.GetByIdAsync(id);
            if(walkDomain == null){
                return NotFound();
            }
            
            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkRequestDto updateWalkRequestDto){

            var walkDomain = mapper.Map<Walk>(updateWalkRequestDto);

            if(walkDomain == null){
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id){

            var deleteWalkDomain = await walkRepository.DeleteAsync(id);

            if(deleteWalkDomain == null){
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(deleteWalkDomain);

            return Ok(walkDto);
        }
    }
}