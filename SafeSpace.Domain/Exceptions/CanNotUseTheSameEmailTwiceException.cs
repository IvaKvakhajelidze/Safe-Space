namespace SafeSpace.Domain.Exceptions
{
    public class CanNotUseTheSameEmailTwiceException : Exception
    {
        public CanNotUseTheSameEmailTwiceException(string email) : base($"Email '{email}' has already been used!")
        { }
    }
}
