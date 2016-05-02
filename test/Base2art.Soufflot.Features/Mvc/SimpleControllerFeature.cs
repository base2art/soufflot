namespace Base2art.Soufflot.Mvc
{
    using System;
    using System.Collections.Generic;

    using System.Diagnostics;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class SimpleControllerFeature
    {
        [Test]
        public void ShouldLoadRenderingController()
        {
            IRenderingRouted controller = new TestRenderingController();
            controller.Execute(new TestHttpContext(), new List<PositionedResult>()).Should().NotBeNull();
            controller.NonRenderingRoutedItems.Should().BeEmpty();
            controller.RenderingRoutedItems.Should().BeEmpty();
        }
        
        [Test]
        public void ShouldLoadNonRenderingController()
        {
            INonRenderingRouted controller = new TestNonRenderingController();
            controller.Execute(new TestHttpContext());
            controller.NonRenderingRoutedItems.Should().BeEmpty();
        }
        
        [Test]
        public void ShouldLoadNullRenderingController()
        {
            IRenderingRouted controller = new NullRenderingController();
            controller.Execute(new TestHttpContext(), new List<PositionedResult>()).Should().NotBeNull();
            controller.NonRenderingRoutedItems.Should().BeEmpty();
            controller.RenderingRoutedItems.Should().BeEmpty();
        }
        
        [Test]
        public void ShouldLoadNullNonRenderingController()
        {
            INonRenderingRouted controller = new NullNonRenderingController();
            controller.Execute(new TestHttpContext());
            controller.NonRenderingRoutedItems.Should().BeEmpty();
        }

        [Test]
        public void ShouldLoadChildNonRenderingController()
        {
            INonRenderingRouted controller = new TestChildNonRenderingController();
            controller.NonRenderingRoutedItems.Length.Should().Be(2);
        }

        [Test]
        public void ShouldLoadChildRenderingController()
        {
            IRenderingRouted controller = new TestChildRenderingController();
            
            var nonRenderingControllers = controller.NonRenderingRoutedItems;
            nonRenderingControllers.Length.Should().Be(2);
            
            var testHttpContext = new TestHttpContext();
            nonRenderingControllers[0].Execute(testHttpContext);
            var messages = ((InMemoryLogger)testHttpContext.Logger).Messages;
            messages.Length.Should().Be(1);
            messages[0].Message.Should().Be("Test");
            
            nonRenderingControllers[1].Execute(new TestHttpContext());
            
            var renderingControllers = controller.RenderingRoutedItems;
            renderingControllers.Length.Should().Be(3);
            renderingControllers[0].RenderingRoutedItem.Execute(new TestHttpContext(), new List<PositionedResult>())
                .Content.BodyAsString.Should().Be("Here You Go");
            renderingControllers[1].RenderingRoutedItem.Execute(new TestHttpContext(), new List<PositionedResult>())
                .Content.BodyAsString.Should().BeEmpty();
            renderingControllers[2].RenderingRoutedItem.Execute(new TestHttpContext(), new List<PositionedResult>())
                .Content.BodyAsString.Should().BeEmpty();
        }

        private class TestRenderingController : SimpleRenderingController
        {
            protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
            {
                return httpContext.Ok();
            }
        }

        private class TestNonRenderingController : SimpleNonRenderingController
        {
            protected override void ExecuteMain(IHttpContext httpContext)
            {
            }
        }

        private class TestChildRenderingController : SimpleRenderingController
        {
            protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
            {
                return httpContext.Ok();
            }
            
            protected override IEnumerable<INonRenderingRouted> NonRenderingControllers
            {
                get
                {
                    yield return this.CreateNonRenderingController(x => x.Logger.Log("Test", LogLevels.DeveloperTrace));
                    yield return this.CreateNonRenderingController(null);
                }
            }
            
            protected override IEnumerable<IPositionedRenderingRouted> RenderingControllers
            {
                get
                {
                    yield return this.CreateRenderingController(0, 0, (x, y) => x.Ok(new SimpleContent{ BodyContent = "Here You Go" }));
                    yield return this.CreateRenderingController(0, 0, (IRenderingRouted)null);
                    yield return this.CreateRenderingController(0, 0, (Func<IHttpContext, List<PositionedResult>, IResult>)null);
                }
            }
        }

        private class TestChildNonRenderingController : SimpleNonRenderingController
        {
            protected override void ExecuteMain(IHttpContext httpContext)
            {
            }
            
            protected override IEnumerable<INonRenderingRouted> NonRenderingControllers
            {
                get
                {
                    yield return this.CreateNonRenderingController(x => Debug.WriteLine(x));
                    yield return this.CreateNonRenderingController(null);
                }
            }
        }
    }
}
