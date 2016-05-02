namespace Base2art.Soufflot.Routing.Expressive
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Base2art.Soufflot.Api.Routing.Expressive;

    using FluentAssertions;

    using NUnit.Framework;
    using Base2art.Soufflot.Fixtures;

    [TestFixture]
    public class ExpressiveRouteValidatorFeature
    {
        [Test]
        public void ShouldParseExpression()
        {
            var expression = new ExpressiveRouteValidator<CustomController>().ValidateExpression(
                 (ctrlr, ctx, cld) => ctrlr.Execute(ctx, cld, 3));

            expression.MethodName.Should().Be("Execute");
//            expression.Parameters.Should().BeEquivalentTo(new object[] { 3 });
            expression.Parameters.Length.Should().Be(1);
            expression.Parameters.First().As<ConstantRouteExpressionParameter>().Value.Should().Be(3);
        }

        [Test]
        public void ShouldParseExpressionNamedIndex()
        {
            var expression = new ExpressiveRouteValidator<CustomController>().ValidateExpression(
                 (ctrlr, ctx, cld) => ctrlr.Edit(ctx, cld, 3, "h"));

            expression.MethodName.Should().Be("Edit");
//            expression.Parameters.Should().BeEquivalentTo(new object[] { 3, "h" });

            expression.Parameters.Length.Should().Be(2);
            expression.Parameters.First().As<ConstantRouteExpressionParameter>().Value.Should().Be(3);
            expression.Parameters.Skip(1).First().As<ConstantRouteExpressionParameter>().Value.Should().Be("h");
        }

        [Test]
        public void ShouldValidateBy()
        {
            new Action(
                () =>
                {
                    new ExpressiveRouteValidator<CustomController>().ValidateExpression(
                        (ctrlr, ctx, cld) => ctrlr.Execute(ctx, null, 3));
                }).ShouldThrow<ArgumentException>();

            new Action(
                () =>
                {
                    new ExpressiveRouteValidator<CustomController>().ValidateExpression(
                        (ctrlr, ctx, cld) => new CustomController().Execute(ctx, cld, 3));
                })
                .ShouldThrow<ArgumentException>();

//            new Action(
//                () =>
//                {
//                    ExpressiveRouteValidator.ValidateExpression<CustomController>(
//                        (ctrlr, ctx, cld) => ctrlr.NotExecute(ctx, cld, 3));
//                }).ShouldThrow<ArgumentException>();

            new Action(
                () =>
                {
                    new ExpressiveRouteValidator<CustomController>().ValidateExpression(
                        (ctrlr, ctx, cld) => ctrlr.Execute(ctx, cld, 3));
                }).ShouldNotThrow();
        }

        [Test]
        public void ShouldValidateComplex()
        {
            var validator = new ExpressiveRouteValidator1<CustomController>(new Regex("/public/(?<path>.*)"));
            var tree = validator.ValidateExpression<string>(
                    (ctrlr, ctx, cld, path) => ctrlr.ExecuteWithParams(ctx, cld, 3, path, 7));
            tree.MethodName.Should().Be("ExecuteWithParams");
            tree.Parameters.Length.Should().Be(3);
            tree.Parameters[0].As<ConstantRouteExpressionParameter>().Value.Should().Be(3);
            tree.Parameters[1].As<FunctionalRouteExpressionParameter>().Name.Should().Be("path");
            tree.Parameters[2].As<ConstantRouteExpressionParameter>().Value.Should().Be(7);
        }

        [Test]
        public void ShouldValidateComplexThrowsErrorOnBadNames()
        {
            var validator = new ExpressiveRouteValidator1<CustomController>(new Regex("/public/(?<test>.*)"));
            
             new Action(
                () =>
                {
                    validator.ValidateExpression<string>(
                    (ctrlr, ctx, cld, path) => ctrlr.ExecuteWithParams(ctx, cld, 3, path, 7));

                })
                .ShouldThrow<ArgumentException>();
        }
    }
}
