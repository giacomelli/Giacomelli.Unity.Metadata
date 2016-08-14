using Giacomelli.Unity.Metadata.Infrastructure.Framework.Logging;

namespace Giacomelli.Unity.Metadata.Infrastructure.Unity
{
	/// <summary>
	/// Console log.
	/// </summary>
    public class ConsoleLog : ILog
    {
		/// <summary>
		/// Writes a log message in a debug level.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Debug(string message, params object[] args)
        {
            UnityEngine.Debug.LogFormat(message, args);
        }

		/// <summary>
		///  Writes a log message in a warning level.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Warning(string message, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(message, args);
        }

		/// <summary>
		///  Writes a log message in a error level.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Error(string message, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(message, args);
        }        
    }
}
