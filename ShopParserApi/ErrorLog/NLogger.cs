using NLog;
using NLog.Config;
using NLog.Targets;

namespace ShopParserApi.ErrorLog
{
    public class NLogger
    {
        public static Logger LogWrite()
        {
            Logger log;
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            consoleTarget.Layout = @"${date:format=yyyy-MM-ddTHH\:mm\:ss} ${logger} ${message}";
            fileTarget.FileName = "${basedir}/ErrorLog/Logs/errors.txt";
            fileTarget.Layout = @"--------------------- ${level}(${longdate})${windows-identity:domain=false}-------------------- ${newline}      
            Controller/ Method Name: ${callsite}${newline}
            Exception Type: ${exception:format=Type}${newline}  
            Message: ${exception:format=Message}${newline}
            Stack Trace: ${exception:format=Stack Trace}${newline}    
            User Info: ${message}${newline}";

            var rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);
            var rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);
            LogManager.Configuration = config;
            log = LogManager.GetLogger("TestWebLog");
            return log;
        }
    }
}