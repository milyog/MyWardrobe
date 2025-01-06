namespace MyWardrobe.ErrorHandling
{
    public sealed record Error(string Code, string description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
