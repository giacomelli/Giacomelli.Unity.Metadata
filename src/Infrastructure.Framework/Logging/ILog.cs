using System.Diagnostics.CodeAnalysis;

namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.Logging
{
    public interface ILog
    {
        void Debug(string message, params object[] args);

        void Warning(string message, params object[] args);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error")]
        void Error(string message, params object[] args);
    }
}
