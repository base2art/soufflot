namespace Base2art.Soufflot.Http.Owin
{
    using System;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Routing.Functional;
    using Base2art.Soufflot.Samples.Session;

    using NUnit.Framework;

    public class AppBaseFeature
    {
        private RoutedExecutionManager manager;

        public string CommonSalt
        {
            get { return "salt"; }
        }

        public RoutedExecutionManager Manager
        {
            get
            {
                if (this.manager == null)
                {
                    this.manager = CreateManager(this.AppMode);
                }

                return this.manager;
            }
        }

        protected void ClearManager()
        {
            this.manager = null;
        }

        [SetUp]
        public void SetUpManager()
        {
            this.ClearManager();
        }

        protected virtual ApplicationMode AppMode
        {
            get { return ApplicationMode.Test; }
        }

        private static RoutedExecutionManager CreateManager(ApplicationMode applicationMode)
        {
            IApplication app = new Application(applicationMode, Environment.CurrentDirectory, null);
            var m = new RoutedExecutionManager(
                app,
                new FunctionalRouter(new[] { new FunctionalRenderingControllerSearchDelegate(MapPath) }, null));
            return m;
        }

        private static Type MapPath(IHttpRequest arg)
        {
            if (arg.Path == "/session-set")
            {
                return typeof(SessionWriterController);
            }

            if (arg.Path == "/session-get")
            {
                return typeof(SessionReaderController);
            }

            if (arg.Path == "/flash-set")
            {
                return typeof(FlashWriterController);
            }

            if (arg.Path == "/flash-set-with-redirect")
            {
                return typeof(FlashWriterWithRedirectController);
            }

            if (arg.Path == "/flash-get")
            {
                return typeof(FlashReaderController);
            }

            if (arg.Path == "/redirect")
            {
                return typeof(RedirectingController);
            }

            if (arg.Path == "/exception")
            {
                return typeof(ExceptionThrowingController);
            }

            if (arg.Path == "/print-user")
            {
                return typeof(UserReaderController);
            }

            return null;
        }
    }
}
