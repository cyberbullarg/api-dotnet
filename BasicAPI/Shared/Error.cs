namespace BasicAPI.Shared
{
    public sealed record Error(string Description, string? Code = null)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
