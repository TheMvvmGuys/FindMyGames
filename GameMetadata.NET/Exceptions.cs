using System;

namespace GameMetadata.Net
{
    public abstract class GameMetadataException : Exception
    {
        protected GameMetadataException()
        {
        }

        protected GameMetadataException(string message) : base(message)
        {
        }

        protected GameMetadataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    public sealed class GameDataNotFoundException : GameMetadataException
    {
        internal GameDataNotFoundException(string query = "that query", string message = null) 
            : base(message ?? $"We couldn't find any game metadata about {query}.")
        {
            
        }
    }
}