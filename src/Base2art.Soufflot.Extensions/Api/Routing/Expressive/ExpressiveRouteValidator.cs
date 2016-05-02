namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class ExpressiveRouteValidator<TController> : ExpressiveRouteValidatorBase<TController>
        where TController : IRenderingRouted
    {
        public RouteExpressionTree ValidateExpression(
            Expression<Func<TController, IHttpContext, List<PositionedResult>, IResult>> func)
        {
            return this.VerifyNonFunctionalExpression(func);
        }

        public RouteExpressionTree ValidateNonFunctionalExpression(Expression func)
        {
            return this.VerifyNonFunctionalExpression(func);
        }
    }
}
