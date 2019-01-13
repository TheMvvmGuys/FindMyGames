namespace GameMetadata.Net
{
    public sealed class GameDataNotFoundException : GameMetadataException
    {
        internal GameDataNotFoundException(
            string query = "that query",
            string message = null) 
            : base(
                message ?? $"We couldn't find any game metadata about {query}.")
        {
        }
    }
}