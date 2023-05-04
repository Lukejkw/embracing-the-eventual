using Cart.CartAggregate;
using Cart.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Cart.Endpoints.Cart;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> logger;
    private readonly IPublishEndpoint publishEndpoint;
    private readonly IMongoCollection<CustomerCart> collection;

    public CartController(
        ILogger<CartController> logger,
        IMongoClient mongoClient,
        IPublishEndpoint publishEndpoint)
    {
        this.logger = logger;
        this.publishEndpoint = publishEndpoint;

        collection = mongoClient
            .GetDatabase("cart")
            .GetCollection<CustomerCart>("cart");
    }

    [HttpGet("{userId}")]
    public async Task<List<CustomerCart>> Get([FromRoute] int userId, CancellationToken cancellationToken) => await collection
        .Find(Builders<CustomerCart>.Filter.Where(c => c.UserId == userId))
        .ToListAsync(cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateCartRequest body)
    {
        var items = body.Items
            .Select(
                i => new CartItem(i.Sku, i.Name, i.Quantity))
            .ToList();

        var filter = Builders<CustomerCart>.Filter.Where(c => c.UserId == body.UserId);
        var existingCart = await collection
            .Find(filter)
            .FirstOrDefaultAsync();
        var cart = new CustomerCart(
            Guid.NewGuid(), 
            body.UserId, 
            items, 
            DateTime.UtcNow);
        
        if (existingCart is null)   
        {
            
            await collection.InsertOneAsync(
                cart,
                new InsertOneOptions());

            logger.LogInformation("Cart created for user {UserId} with {NumberOfItems} items", cart.UserId, cart.Items.Count);

            return Created(cart.Id.ToString(), cart.Id.ToString());
        }

        // For demo purposes
        await collection.DeleteOneAsync(Builders<CustomerCart>.Filter.Where(c => c.Id == existingCart.Id));
        
        await collection.InsertOneAsync(cart);
        return Ok(cart.Id.ToString());
    }

    [HttpPatch]
    public async Task<ActionResult> Checkout([FromBody] CheckoutCartRequest body)
    {
        var whereIsUserCart = Builders<CustomerCart>.Filter.Where(c => c.Id == body.CartId);
        var cart = await collection
            .Find(whereIsUserCart)
            .FirstOrDefaultAsync();

        if (cart is null)
        {
            return NotFound(body.CartId);
        }

        cart.Checkout();

        await collection.ReplaceOneAsync(
            Builders<CustomerCart>.Filter.Where(c => c.Id == cart.Id),
            cart);

        await publishEndpoint.Publish(new CheckoutCart(body.CartId, body.Address, body.CreditCardNumber));
        
        logger.LogInformation("Checkout out cart {CartId} created for user {UserId} with {NumberOfItems} items", cart.Id, cart.UserId, cart.Items.Count);

        return Accepted();
    }
    
    [HttpDelete("{cartId}")]
    public async Task<ActionResult> Checkout([FromRoute] Guid cartId)
    {
        var isCart = Builders<CustomerCart>.Filter.Where(c => c.Id == cartId);
        await collection.DeleteOneAsync(isCart);
        return Ok();
    }
}