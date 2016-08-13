using Giacomelli.Unity.Metadata.Infrastructure.Framework.Logging;

namespace Giacomelli.Unity.Metadata.Infrastructure.Unity
{
    public class ConsoleLog : ILog
    {
        public void Debug(string message, params object[] args)
        {
            UnityEngine.Debug.LogFormat(message, args);
        }

        public void Warning(string message, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(message, args);
        }

        public void Error(string message, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(message, args);
        }        
    }
}
