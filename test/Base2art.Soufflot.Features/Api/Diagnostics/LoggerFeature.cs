namespace Base2art.Soufflot.Api.Diagnostics
{
    using System.IO;
	using System.Linq;
    using Base2art.Soufflot.Api.Diagnostics;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class LoggerFeature
    {
        [Test]
        public void ShouldLog()
        {
            InMemoryLogger logger = new InMemoryLogger(LogLevels.Always);
            logger.Log("Test", LogLevels.DeveloperFinest);
            logger.Messages.Length.Should().Be(1);

            logger = new InMemoryLogger(LogLevels.Off);
            logger.Log("Test", LogLevels.DeveloperFinest);
            logger.Messages.Length.Should().Be(0);

            logger = new InMemoryLogger(LogLevels.Off);
            logger.Log("Test", LogLevels.Off);
            logger.Messages.Length.Should().Be(1);

            logger = new InMemoryLogger(LogLevels.SystemAlertEvent);
            logger.Log("Test", LogLevels.SystemEmergencyEvent);
            logger.Messages.Length.Should().Be(1);

            logger = new InMemoryLogger(LogLevels.SystemAlertEvent);
            logger.Log("Test", LogLevels.SystemSevereEvent);
            logger.Messages.Length.Should().Be(0);
        }
        
        [Test]
        public void ShouldLogConsole()
        {
            var oldVariable = System.Console.Out;
            
            LogLevels.Always.Name.Should().Be(LogLevels.Always.DisplayName);
            
            ILogger logger = this.Create(LogLevels.Always);
            logger.Log("Test", LogLevels.DeveloperFinest);
            this.Messages(logger).Length.Should().Be(1);

            logger = this.Create(LogLevels.Off);
            logger.Log("Test", LogLevels.DeveloperFinest);
            this.Messages(logger).Length.Should().Be(0);

            logger = this.Create(LogLevels.Off);
            logger.Log("Test", LogLevels.Off);
            this.Messages(logger).Length.Should().Be(1);

            logger = this.Create(LogLevels.SystemAlertEvent);
            logger.Log("Test", LogLevels.SystemEmergencyEvent);
            this.Messages(logger).Length.Should().Be(1);

            logger = this.Create(LogLevels.SystemAlertEvent);
            logger.Log("Test", LogLevels.SystemSevereEvent);
            this.Messages(logger).Length.Should().Be(0);
            
            System.Console.SetOut(oldVariable);
        }

        private StringWriter par;
        private string[] Messages(ILogger logger)
        {
//			var textWriterLogger = (TextWriterLogger)logger;
//            var writer = textWriterLogger.Writer;

            return this.par.GetStringBuilder()
                .ToString()
                .Split('\n')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
        
        private ILogger Create(LogLevel level)
        {
            this.par = new StringWriter();
            System.Console.SetOut(par);
            return new ConsoleLoggerFactory().Create(level);
            
        }
    }
}
