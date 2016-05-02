//namespace Base2art.Soufflot.Api
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Net.Http;
//
//    using Base2art.Soufflot.Api;
//    using Base2art.Soufflot.Mvc;
//
//    using FluentAssertions;
//
//    using Base2art.Soufflot.Http;
//
//    using NUnit.Framework;
//
//    [TestFixture]
//    public class RequestHandlerFeature
//    {
//        [Test]
//        public void ShouldFindControllersRoutes()
//        {
//            var routeHandler =
//                new Router(
//                    new IRenderingControllerSearchDelegate[]
//                    {
//                        new FunctionalRenderingControllerSearchDelegate(x => x.Path == "/admin" ? typeof(MyController) : null),
//                        new FunctionalRenderingControllerSearchDelegate(null)
//                    }, null);
//
//            var controllerType = routeHandler.FindRenderingControllerType(new OwinRequest
//            {
//                Method = "GET",
//                Host = new HostString("www.scottyoungblut.com"),
//                Path = new PathString("/admin")
//            });
//
//            controllerType.Should().NotBeNull();
//
//            controllerType = routeHandler.FindRenderingControllerType(
//                new OwinRequest { Method = "GET" }
//                .WithUri(new Uri("http://www.scottyoungblut.com/admin/test")));
//            controllerType.Should().BeNull();
//        }
//
//
//        [Test]
//        public void ShouldFindSubcontrollersRoutes()
//        {
//            var controllerFindingDelegates = new IRenderingControllerSearchDelegate[]
//                                             {
//                                                 new FunctionalRenderingControllerSearchDelegate(
//                                                     x => x.Path.Value == "/admin" ? typeof(MyController) : null)
//                                             };
//            var subcontrollerFindingDelegates = new INonRenderingControllerSearchDelegate[]
//                                             {
//                                                 new FunctionalNonRenderingControllerSearchDelegate(
//                                                     x => x.Path.Value == "/admin" ? typeof(MyController) : null),
//                                                 new FunctionalNonRenderingControllerSearchDelegate(null)
//                                             };
//            var routeHandler = new Router(controllerFindingDelegates, subcontrollerFindingDelegates);
//
//            var controllerType =
//                routeHandler.FindNonRenderingControllerTypes(
//                    new OwinRequest { Method = HttpMethod.Get.Method }
//                    .WithUri(new Uri("http://www.scottyoungblut.com/admin")));
//
//            controllerType.Should().NotBeNull();
//
//            controllerType =
//                routeHandler.FindNonRenderingControllerTypes(
//                new OwinRequest { Method = HttpMethod.Get.Method }
//                .WithUri(new Uri("http://www.scottyoungblut.com/admin/test")));
//            controllerType.Should().BeEmpty();
//        }
//
//        private class MyController : IRenderingController
//        {
//
//            public IPositionedRenderingController[] RenderingControllers
//            {
//                get
//                {
//                    return new IPositionedRenderingController[0];
//                }
//            }
//
//            public INonRenderingController[] NonRenderingControllers
//            {
//                get
//                {
//                    return new INonRenderingController[0];
//                }
//            }
//
//            public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
//            {
//                return new SimpleResult();
//            }
//        }
//    }
//}
