using Kolokwium01.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public MedicamentsController(IDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpGet("{idMedicament}")]
        public IActionResult GetMedicament(int idMedicament)
        {
            return Ok(_dbService.GetMedicament(idMedicament));
        }


        [HttpDelete("{idPatient}")]
        public IActionResult DeletePatient(int idPatient)
        {
            if (string.IsNullOrEmpty(idPatient.ToString()))
            {
                return BadRequest();
            }
            else
            {
                _dbService.DeletePatient(idPatient);
                return Ok($"Pacjent o id {idPatient} został usunięty z bazy danych");
            }

        }












    }
}
