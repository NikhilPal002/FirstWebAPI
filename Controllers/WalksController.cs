using AutoMapper;
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
    }
}