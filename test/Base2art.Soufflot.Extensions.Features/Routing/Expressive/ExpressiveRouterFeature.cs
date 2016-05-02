namespace Base2art.Soufflot.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Base2art.Converters;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Routing.Expressive;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    using FluentAssertions;

    using NUnit.Framework;
    using Base2art.Soufflot.Fixtures;

    [TestFixture]
    public class ExpressiveRouterFeature : ExpressiveRouterBaseFeature
    {
        [Test]
        public void ShouldFindControllerWhenItHasExpression()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html",
                (ctrlr, ctx, cld) => ctrlr.Execute(ctx, cld, 3));

            var request = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/user/edit.html");
            router.FindRenderingControllerType(request).Type.Should().Be(typeof(CustomController));
        }

        [Test]
        public void ShouldFindControllerWhenItHasExpressionNewApu()
        {
            var router = new ExpressiveRouter();
            router.Register("/user/edit.html")
                .WithMethod(HttpMethod.Get)
                .To<CustomController>()
                .Method((ctrlr, ctx, cld) => ctrlr.Execute(ctx, cld, 3));

            var request = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/user/edit.html");
            router.FindRenderingControllerType(request).Type.Should().Be(typeof(CustomController));
        }

        [Test]
        public void ShouldFindRouteByExpression()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html",
                (ctrlr, ctx, cld) => ctrlr.Execute(ctx, cld, 3));

            var request = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/user/edit.html");
            router.FindRenderingControllerType(request).Type.Should().Be(typeof(CustomController));
        }

        [Test]
        public void ShouldFindRoute()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html");

            var route = router.FindRoute<CustomController>();
            route.Explode().Should().Be("/user/edit.html");
        }

        [Test]
        public void ShouldFindRouteThatUsesTraditionalExpression()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html",
                (a, b, c) => a.Execute(b, c));

            var route = router.FindRoute<CustomController>();
            route.Explode().Should().Be("/user/edit.html");
        }

        [Test]
        public void ShouldNotFindRouteThatUsesNonTraditionalExpression()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html",
                (a, b, c) => a.Execute(b, c, 1));

            router.FindRoute<CustomController>().Should().BeNull();
        }

        [Test]
        public void ShouldFindRouteThatUsesExpressionWithParameters()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html",
                (a, b, c) => a.Execute(b, c, 1));

            router.FindRoute<CustomController>().Should().BeNull();
            router.FindRouteWith<CustomController>(2).Should().BeNull();
            router.FindRouteWith<CustomController>(1).Explode().Should().Be("/user/edit.html");
        }

        [Test]
        public void ShouldNotFindRouteThatUsesExpressionWithParametersNotNamedExecute()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html",
                (a, b, c) => a.Edit(b, c, 1, "d"));

            router.FindRoute<CustomController>().Should().BeNull();
            router.FindRouteWith<CustomController>(2).Should().BeNull();
            router.FindRouteWith<CustomController>(1, "d").Should().BeNull();
        }

        [Test]
        public void ShouldNotFindRouteThatUsesExpressionNamed()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html",
                (a, b, c) => a.Index(b, c));

            router.FindRoute<CustomController>().Should().BeNull();
            router.FindRouteWith<CustomController>(2).Should().BeNull();
            router.FindRouteWith<CustomController>(1, "s").Should().BeNull();
            router.FindNamedRoute<CustomController>("Index").Explode().Should().Be("/user/edit.html");
        }

        [Test]
        public void ShouldNotFindRouteThatUsesExpressionNamedWithParameters()
        {
            var router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(
                HttpMethod.Get,
                null,
                "/user/edit.html",
                (a, b, c) => a.Edit(b, c, 3, "s"));

            router.FindRoute<CustomController>().Should().BeNull();
            router.FindRouteWith<CustomController>(2).Should().BeNull();
            router.FindRouteWith<CustomController>(1, "s").Should().BeNull();
            router.FindNamedRoute<CustomController>("Edit").Should().BeNull();
            router.FindNamedRouteWith<CustomController>("Edit", 3, "s").Explode().Should().Be("/user/edit.html");
        }

        [Test]
        public void ShouldFindControllerWhenItHasRegularExpression()
        {
            var router = new ExpressiveRouter();
            //            Console.WriteLine("groupNames: " + string.Join(", ", regex.GetGroupNames()));

            router.Register(new Regex("/assets/(?<path>.*)"), "/assets/{path}")
                .WithMethod(HttpMethod.Get)
                .To<AssetsController>()
                .Method<string>((ctrlr, ctx, cld, path) => ctrlr.At(ctx, cld, "public", path));

            var request = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/assets/nothing.png");
            router.FindRenderingControllerType(request).Type.Should().Be(typeof(AssetsController));
            var request2 = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/non-match/nothing.png");
            router.FindRenderingControllerType(request2).Should().BeNull();
        }

        [Test]
        public void ShouldFindControllerWhenItHasRegularExpressionUsingTypeConversion()
        {
            var router = new ExpressiveRouter();
            router.Register(new Regex("/api/person/(?<id>\\d+)/.*"), "/api/person/{id}/")
                .WithMethod(HttpMethod.Get)
                .To<PersonController>()
                .Method<int>((ctrlr, ctx, cld, id) => ctrlr.GetUser(ctx, cld, id));

            var request = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/api/person/42/scott-youngblut");
            router.FindRenderingControllerType(request).Type.Should().Be(typeof(PersonController));

            var request2 = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/api/person/abc/scott-youngblut");
            router.FindRenderingControllerType(request2).Should().BeNull();
        }

        [Test]
        public void ShouldFindRouteThatUsesRegexExpressionNamedWithParameters()
        {
            var router = new ExpressiveRouter();
            router.Register(new Regex("/api/person/(?<id>\\d+)/.*"), "/api/person/{id}/")
                .WithMethod(HttpMethod.Get)
                .To<PersonController>()
                .Method<int>((ctrlr, ctx, cld, id) => ctrlr.GetUser(ctx, cld, id));

            router.FindRoute<PersonController>().Should().BeNull();
            router.FindRouteWith<PersonController>(2).Should().BeNull();
            router.FindRouteWith<PersonController>(1, "test").Should().BeNull();
            router.FindNamedRoute<PersonController>("GetUser").Should().BeNull();

            router.For<PersonController>()
                .With<int>((x, y, z, id) => x.GetUser(y, z, 3))
                .Explode().Should().Be("/api/person/3/");

            int i = 2;
            
            var tickCount = Environment.TickCount;
//            Console.WriteLine(tickCount);
            
            if (tickCount != 0)
            {
                i += 2;
            }
            
            router.For<PersonController>()
                            .With((x, y, z) => x.GetUser(y, z, i))
                            .Explode().Should().Be("/api/person/4/");

            // Correctly fails to Compile
            // router.For<PersonController>()
            //                             .With((x, y, z) => x.GetUser(y, z, "sdf"))
            //                             .Explode().Should().Be("/api/person/4/");
            // 
            // Was:
            // router.FindNamedDynamicRouteWith<PersonController>("GetUser", new { Name = "Scott" }).Should().BeNull();
        }

        [Test]
        public void ShouldFindRouteThatUsesRegexExpressionNamedWithParametersAndConstants()
        {
            var router = new ExpressiveRouter();
            router.Register(new Regex("/api/person/(?<id>\\d+)/.*"), "/api/person/{id}/")
                .WithMethod(HttpMethod.Get)
                .To<PersonController>()
                .Method<int>((ctrlr, ctx, cld, id) => ctrlr.GetUserWithName(ctx, cld, id, "Scott Youngblut"));

            router.FindRoute<PersonController>().Should().BeNull();
            router.FindRouteWith<PersonController>(2).Should().BeNull();
            router.FindRouteWith<PersonController>(1, "test").Should().BeNull();
            router.FindNamedRoute<PersonController>("GetUser").Should().BeNull();

            router.For<PersonController>()
                .With<int>((x, y, z, id) => x.GetUserWithName(y, z, 3, "Other"))
                .Explode().Should().BeNull();

            router.For<PersonController>()
                .With<int>((x, y, z, id) => x.GetUserWithName(y, z, 3, "Scott Youngblut"))
                .Explode().Should().Be("/api/person/3/");

            router.For<PersonController>()
                .OnDomain("FakeDomain")
                .With<int>((x, y, z, id) => x.GetUserWithName(y, z, 3, "Scott Youngblut"))
                .Explode().Should().Be("/api/person/3/");
        }

        [Test]
        public void ShouldFindRouteThatUsesRegexExpressionNamedWithParametersAndConstantsRespectingDomain()
        {
            var router = new ExpressiveRouter();
            router.Register(new Regex("/api/person/(?<id>\\d+)/.*"), "/api/person/{id}/")
                .WithMethod(HttpMethod.Get)
                .OnDomain("www.base2art.com")
                .To<PersonController>()
                .Method<int>((ctrlr, ctx, cld, id) => ctrlr.GetUserWithName(ctx, cld, id, "Scott Youngblut"));

            router.FindRoute<PersonController>().Should().BeNull();
            router.FindRouteWith<PersonController>(2).Should().BeNull();
            router.FindRouteWith<PersonController>(1, "test").Should().BeNull();
            router.FindNamedRoute<PersonController>("GetUser").Should().BeNull();

            router.For<PersonController>()
                .With<int>((x, y, z, id) => x.GetUserWithName(y, z, 3, "Other"))
                .Explode().Should().BeNull();

            router.For<PersonController>()
                .With<int>((x, y, z, id) => x.GetUserWithName(y, z, 3, "Scott Youngblut"))
                .Explode().Should().BeNull();

            router.For<PersonController>()
                .OnDomain("www.base2art.com")
                .With<int>((x, y, z, id) => x.GetUserWithName(y, z, 3, "Scott Youngblut"))
                .Explode().Should().Be("/api/person/3/");
        }

        [Test]
        public void ShouldFindControllerWhenItHasRegularExpressionUsingTypeConversionUnknownType()
        {
            var router = new ExpressiveRouter(true);

            new Action(() =>
            {
                router.Register(new Regex("/api/person/(?<id>[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12})/.*"), "/api/person/{id}/")
                    .WithMethod(HttpMethod.Get)
                    .To<PersonController>()
                    .Method<Guid>((ctrlr, ctx, cld, id) => ctrlr.GetUserByGuid(ctx, cld, id));
            }).ShouldThrow<ArgumentOutOfRangeException>();

            var request = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/api/person/2102F977-B6DA-4247-8B9D-F2F520A55EB5/scott-youngblut");
            router.FindRenderingControllerType(request).Should().BeNull();

            var request2 = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/api/person/abc/scott-youngblut");
            router.FindRenderingControllerType(request2).Should().BeNull();
        }

        [Test]
        public void ShouldFindControllerWhenItHasRegularExpressionUsingTypeConversionKnownType()
        {
            var router = new ExpressiveRouter();

            router.Register(new Regex("/api/person/(?<id>[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12})/.*"), "/api/person/{id}/")
                .WithMethod(HttpMethod.Get)
                .To<PersonController>()
                .Method<Guid>((ctrlr, ctx, cld, id) => ctrlr.GetUserByGuid(ctx, cld, id));

            var request = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/api/person/2102F977-B6DA-4247-8B9D-F2F520A55EB5/scott-youngblut");
            router.FindRenderingControllerType(request).Type.Should().Be<PersonController>();
        }

        [Test]
        public void ShouldFindControllerWhenItHasRegularExpressionUsingTypeConversionKnownTypeWithAdditions()
        {
            var router = new ExpressiveRouter(true);
            router.AddTypeConverter(new GuidParser());

            router.Register(new Regex("/api/person/(?<id>[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12})/.*"), "/api/person/{id}/")
                .WithMethod(HttpMethod.Get)
                .To<PersonController>()
                .Method<Guid>((ctrlr, ctx, cld, id) => ctrlr.GetUserByGuid(ctx, cld, id));

            var request = this.CreateRequestFor(HttpMethod.Get, "Somehost.com", "/api/person/2102F977-B6DA-4247-8B9D-F2F520A55EB5/scott-youngblut");
            router.FindRenderingControllerType(request).Type.Should().Be<PersonController>();
        }

        public class PersonController : SimpleRenderingController
        {
            protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
            {
                return httpContext.NoContent();
            }

            public IResult GetUser(IHttpContext httpContext, List<PositionedResult> childResults, int id)
            {
                return httpContext.NoContent();
            }

            public IResult GetUserByGuid(IHttpContext httpContext, List<PositionedResult> childResults, Guid id)
            {
                return httpContext.NoContent();
            }

            public IResult GetUserWithName(IHttpContext httpContext, List<PositionedResult> childResults, int id, string name)
            {
                return httpContext.NoContent();
            }
        }
    }
}

/*
 
        //
            //            var requestNotFound = A.Fake<IHttpRequest>();
            //            A.CallTo(() => requestNotFound.Path).Returns("/user/edit.html");
            //            r.FindRenderingControllerType(requestNotFound).Type.Should().Be(typeof(CustomController));
            //
            //            var route1 = r.FindRoute<CustomController>();
            //            route1.Explode().Should().Be("/user/edit.html");
 */
