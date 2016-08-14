using System.Diagnostics.CodeAnalysis;

namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.Logging
{
	/// <summary>
	/// Define a log interface.
	/// </summary>
    public interface ILog
    {
		/// <summary>
		/// Writes a log message in a debug level.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
        void Debug(string message, params object[] args);

		/// <summary>
		///  Writes a log message in a warning level.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		void Warning(string message, params object[] args);

		/// <summary>
		///  Writes a log message in a error level.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error")]
        void Error(string message, params object[] args);
    }
}
