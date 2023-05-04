namespace Cart.Exceptions;

public class ShootInFootException : Exception
{
    public ShootInFootException() : base("Ouch! You just shot yourself in the foot!")
    {
    }
}