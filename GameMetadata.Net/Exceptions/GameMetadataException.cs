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
}