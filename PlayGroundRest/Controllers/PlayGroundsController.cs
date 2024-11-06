using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PlayGroundLib;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlayGroundRest.Controllers
{
    [Route("api/PlayGrounds")]
    [ApiController]
    public class PlayGroundsController : ControllerBase
    {
        private PlayGroundsRepository _playGroundsRepository;

        public PlayGroundsController(PlayGroundsRepository playGroundsRepository)
        {
            _playGroundsRepository = playGroundsRepository;
        }

        // GET: api/<PlayGroundsController>
        [HttpGet]
        [EnableCors("allowAll")]
        public ActionResult<IEnumerable<PlayGround>> Get()
        {
            return _playGroundsRepository.GetAll();
        }

        // GET api/<PlayGroundsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        [EnableCors("allowAll")]
        public ActionResult<PlayGround> Get(int id)
        {
            PlayGround? foundPlayGround = _playGroundsRepository.GetById(id);
            if (foundPlayGround == null)
            {
                return NotFound("No such class, id:" + id);
            }
            else
            {
                return Ok(foundPlayGround);
            }
        }

        // POST api/<PlayGroundsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [EnableCors("allowAll")]
        public ActionResult<PlayGround> Post([FromBody] PlayGround newPlayGround)
        {
            PlayGround createdPlayGround = _playGroundsRepository.Add(newPlayGround);
            if (createdPlayGround == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("PlayGround object Created", createdPlayGround);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT api/<PlayGroundsController>/5
        [HttpPut("{id}")]
        [EnableCors("allowAll")]
        public ActionResult<PlayGround> Put(int id, [FromBody] PlayGround updatedPlayGround)
        {
            PlayGround? foundPlayGround = _playGroundsRepository.Update(id, updatedPlayGround);
            if (foundPlayGround != null)
            {
                return Ok(foundPlayGround);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
