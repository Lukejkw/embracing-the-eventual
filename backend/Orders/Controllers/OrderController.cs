using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Orders.Controllers;

[Route("[controller]")]

public class OrderController : Controller
{
    private readonly IMongoClient mongoClient;

    public OrderController(IMongoClient mongoClient)
    {
        this.mongoClient = mongoClient;
    }
    
    /// <summary>
    /// Gets all the order. Performance!
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<Models.Order>>> Get(CancellationToken cancellationToken)
    {
        // Obviously, this is a minimal example missing DTOs, abstraction etc
        var orders = await mongoClient
            .GetDatabase("order")
            .GetCollection<Models.Order>("orders")
            .FindAsync(
                Builders<Models.Order>.Filter.Empty, 
                new FindOptions<Models.Order>(), 
                cancellationToken);
        return await orders.ToListAsync(cancellationToken);
    }
}