using Microsoft.AspNetCore.Mvc;
using ApiOrdinario.Model;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ApiOrdinario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonajeController : Controller
    {
        private readonly HttpClient _client;

        public PersonajeController(HttpClient client)
        {
            _client = client;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonajes()
        {
            var response = await _client.GetAsync("https://hp-api.onrender.com/api/characters");
            var content = await response.Content.ReadAsStringAsync();
            var personajes = JsonConvert.DeserializeObject<IEnumerable<Personaje>>(content);

            return Ok(personajes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>> GetPersonajeID(string id)
        {
            var response = await _client.GetAsync($"https://hp-api.onrender.com/api/character/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var personaje = JsonConvert.DeserializeObject<IEnumerable<Personaje>>(content);
            return Ok(personaje);
        }

        [HttpGet("casa/{houseName}")]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonajesCasa(string houseName)
        {
            var response = await _client.GetAsync($"https://hp-api.onrender.com/api/characters/house/{houseName}");
            var content = await response.Content.ReadAsStringAsync();
            var personaje = JsonConvert.DeserializeObject<IEnumerable<Personaje>>(content);
            return Ok(personaje);
        }

    }
}
