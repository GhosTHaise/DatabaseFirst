using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirst.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly Database.BroadcastContext context;
    public ClientController(Database.BroadcastContext _context)
    {
        context = _context;
    }
    [HttpGet]
    public async Task<ActionResult<List<Database.Client>>> Get(){
        try
        {
            List<DatabaseFirst.Database.Client> list = context.Clients.ToList();
            return Ok(list);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<string>> patch(int id){
        try
        {
                var isClientExist = context.Clients.Find(id);
                if(isClientExist is null){
                    return NoContent();
                }
                isClientExist.LastnameClient = "Changed";
                context.SaveChanges();
                return Ok("User Updated !");
        }
        catch (System.Exception)
        {
            return Forbid();
        }
    }
    
}
