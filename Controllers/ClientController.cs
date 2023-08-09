using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirst.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    [HttpGet]
    public async Task<List<Database.Client>> Get(){
        using(var db = new DatabaseFirst.Database.BroadcastContext()){
            List<DatabaseFirst.Database.Client> list = db.Clients.ToList();
            return list;
        }
    }
    
}
