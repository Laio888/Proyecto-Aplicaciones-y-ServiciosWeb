[ApiController]
[Route("api/[controller]")]
public class UniversidadController : ControllerBase {
    private readonly UniversidadService _service;

    public UniversidadController(UniversidadService service) {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Universidad universidad) {
        await _service.Add(universidad);
        return Ok("Universidad creada");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Universidad universidad) {
        universidad.Id = id;
        await _service.Update(universidad);
        return Ok("Universidad actualizada");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await _service.Delete(id);
        return Ok("Universidad eliminada");
    }
}