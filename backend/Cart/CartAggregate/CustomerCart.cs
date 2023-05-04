namespace Cart.CartAggregate;

public class CustomerCart
{
    public Guid Id { get; init; }
    public int UserId { get; init; }
    public List<CartItem> Items { get; set; }
    public DateTime CreatedOn { get; init; }
    public DateTime UpdatedOn { get; set; }

    public bool IsCheckedOut { get; private set; }
    
    public CustomerCart(
        Guid id, 
        int userId, 
        List<CartItem> items, 
        DateTime createdOn)
    {
        Id = id;
        UserId = userId;
        Items = items;
        CreatedOn = createdOn;
    }

    public void Checkout()
    {
        IsCheckedOut = true;
    }

    public void Update(List<CartItem> items, DateTime updatedOn)
    {
        Items = items;
        UpdatedOn = updatedOn;
    }
};