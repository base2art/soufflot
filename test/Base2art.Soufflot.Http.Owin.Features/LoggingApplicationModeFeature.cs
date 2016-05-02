namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using System.Linq;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;

    using FluentAssertions;

    using Microsoft.Owin;

    using NUnit.Framework;

    [TestFixture]
    public class LoggingApplicationModeFeature : AppBaseFeature
    {
        private ApplicationMode applicationMode = ApplicationMode.Dev;

        protected override ApplicationMode AppMode
        {
            get
            {
                return this.applicationMode;
            }
        }

        [Test]
        public void ShouldExecuteControllerAndHaveLoggingForExceptions()
        {
            this.applicationMode = ApplicationMode.Dev;
            OwinContext context = OwinExtender.CreateRequestForPath("/exception");
            var inMemoryLogger = new InMemoryLogger(LogLevels.Always);
            var rezult = context.ProcessRequest(this.Manager, null, this.CommonSalt, inMemoryLogger);
            inMemoryLogger.Messages.Length.Should().BeGreaterOrEqualTo(2);
            var message = inMemoryLogger.Messages.FirstOrDefault(x => x.Message.Contains("Oh Boy!"));
            message.Should().NotBeNull();
            message.ToString().Should().Contain("[ApplicationError]");
            message.ToString().Should().Contain("Op Ex");
//            rezult.Content.Body.Should().Contain("[ApplicationError]");
            rezult.Content.BodyAsString.Should().Contain("Op Ex");
            rezult.Content.BodyAsString.Should().Contain("Oh Boy!");
        }

        [Test]
        public void ShouldExecuteControllerAndHaveLoggingForExceptionsInProd()
        {
            this.applicationMode = ApplicationMode.Prod;
            OwinContext context = OwinExtender.CreateRequestForPath("/exception");
            var inMemoryLogger = new InMemoryLogger(LogLevels.Always);
            
            new Action(()=>context.ProcessRequest(this.Manager, null, this.CommonSalt, inMemoryLogger))
                .ShouldThrow<NotImplementedException>();

            inMemoryLogger.Messages.Length.Should().BeGreaterThan(0);
        }
    }
}
