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
        public async Task<ActionResult<Personaje>> GetPersonajeID(int id)
        {
            var response = await _client.GetAsync($"https://hp-api.onrender.com/api/characters/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"Error en la API externa: {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var personaje = JsonConvert.DeserializeObject<Personaje>(content);
            return Ok(personaje);
        }

        [HttpGet("casa/{houseName}")]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonajesCasa(string houseName)
        {
            var response = await _client.GetAsync($"https://hp-api.onrender.com/api/characters/house/{houseName}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"Error en la API externa: {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var personajes = JsonConvert.DeserializeObject<IEnumerable<Personaje>>(content);
            return Ok(personajes);
        }

    }
}
