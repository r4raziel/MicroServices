using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaltformService.Data;
using PaltformService.Dtos;
using PaltformService.Models;

namespace PaltformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private IPlatformRepo _repository;
        private IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("---> Getting Platforms...");

            var platformItem = _repository.GetAllPlatfroms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));

        }
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Console.WriteLine("--> Getting a Platform");

            var Platform = _repository.GetPlatformById(id);
            if (Platform != null)
            {

                return Ok(_mapper.Map<PlatformReadDto>(Platform));
            }

            return NotFound();


        }
        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {

            var platformModel = _mapper.Map<Platform>(platformCreateDto);

            Console.WriteLine("--> Creating a Platform");

            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);




        }

    }

}