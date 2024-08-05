using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TarefasController : ControllerBase
{
    private readonly AppDbContext _context;

    public TarefasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Tarefa>> PostTarefa([FromBody] Tarefa tarefa)
    {
        if (tarefa == null)
        {
            return BadRequest("A tarefa não pode ser nula.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var usuarioExistente = await _context.Usuarios.FindAsync(tarefa.UsuarioId);
        var categoriaExistente = await _context.Categorias.FindAsync(tarefa.CategoriaId);

        if (usuarioExistente == null)
        {
            ModelState.AddModelError("UsuarioId", "Usuário não encontrado.");
        }

        if (categoriaExistente == null)
        {
            ModelState.AddModelError("CategoriaId", "Categoria não encontrada.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        tarefa.Usuario = usuarioExistente;
        tarefa.Categoria = categoriaExistente;

        if (_context.Entry(tarefa.Categoria).State == EntityState.Detached)
        {
            _context.Categorias.Attach(tarefa.Categoria);
        }

        if (_context.Entry(tarefa.Usuario).State == EntityState.Detached)
        {
            _context.Usuarios.Attach(tarefa.Usuario);
        }

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
    }



    // GET: api/Tarefas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
    {
        var tarefas = await _context.Tarefas
            .Include(t => t.Usuario)
            .Include(t => t.Categoria)
            .ToListAsync();
        return Ok(tarefas);
    }

    // GET: api/Tarefas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tarefa>> GetTarefa(int id)
    {
        var tarefa = await _context.Tarefas
            .Include(t => t.Usuario)
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tarefa == null)
        {
            return NotFound();
        }

        return Ok(tarefa);
    }


    // PUT: api/Tarefas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTarefa(int id, [FromBody] Tarefa tarefa)
    {
        if (id != tarefa.Id)
        {
            return BadRequest();
        }

        _context.Entry(tarefa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TarefaExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Tarefas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarefa(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null)
        {
            return NotFound();
        }

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TarefaExists(int id)
    {
        return _context.Tarefas.Any(e => e.Id == id);
    }
}
