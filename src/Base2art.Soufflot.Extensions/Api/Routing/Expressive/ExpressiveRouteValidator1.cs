namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class ExpressiveRouteValidator1<TController> : ExpressiveRouteValidatorBase<TController>
        where TController : IRenderingController
    {
        private readonly Regex targetedParams;

        public ExpressiveRouteValidator1(Regex targetedParams)
        {
            this.targetedParams = targetedParams;
        }

        public RouteExpressionTree ValidateExpression<T1>(
            Expression<Func<TController, IHttpContext, List<PositionedResult>, T1, IResult>> func)
        {
            return this.VerifyNonFunctionalExpression(func);
        }

        protected override void ValidateLambdaAdditionalInputParameters(
            List<FunctionalRouteExpressionParameter> paramMaps,
            ParameterExpression[] arguments)
        {
            base.ValidateLambdaAdditionalInputParameters(paramMaps, arguments);
            var set = new HashSet<string>(this.targetedParams.GetGroupNames());
            foreach (var parameterExpression in arguments)
            {
                var paramName = parameterExpression.Name;
                if (set.Contains(paramName))
                {
                    paramMaps.Add(new FunctionalRouteExpressionParameter(paramName));
                    /*,
                            this.targetedParams,
                            parameterExpression.Type*/
                }
            }
        }

        protected override void VerifyAndAddNonDefaultPassthroughParams(
            List<FunctionalRouteExpressionParameter> parentList,
            List<RouteExpressionParameter> paramMaps,
            IEnumerable<Expression> arguments)
        {
            var set = new HashSet<string>(parentList.Select(x => x.Name));
            foreach (var remainingArg in arguments)
            {
                var contanstExpr = remainingArg as ConstantExpression;
                if (contanstExpr != null)
                {
                    paramMaps.Add(new ConstantRouteExpressionParameter(contanstExpr.Value));
                }
                else
                {
                    var param = ((ParameterExpression)remainingArg);
                    if (!set.Contains(param.Name))
                    {
                        throw new ArgumentException("All of your variables must come as input parameters");
                    }

                    // , this.targetedParams, param.Type
                    paramMaps.Add(new FunctionalRouteExpressionParameter(param.Name));
                }
            }
        }
    }

    public class ExpressiveReverseRouteValidator1<TController> : ExpressiveRouteValidatorBase<TController>
        where TController : IRenderingController
    {
        public ExpressiveReverseRouteValidator1()
        {
        }

        public RouteExpressionTree ValidateExpression<T1>(
            Expression<Func<TController, IHttpContext, List<PositionedResult>, T1, IResult>> func)
        {
            return this.VerifyNonFunctionalExpression(func);
        }

        protected override void ValidateLambdaAdditionalInputParameters(
            List<FunctionalRouteExpressionParameter> paramMaps,
            ParameterExpression[] arguments)
        {
            base.ValidateLambdaAdditionalInputParameters(paramMaps, arguments);
            foreach (var parameterExpression in arguments)
            {
                var paramName = parameterExpression.Name;
                paramMaps.Add(new FunctionalRouteExpressionParameter(paramName));
            }
        }

        protected override void VerifyAndAddNonDefaultPassthroughParams(
            List<FunctionalRouteExpressionParameter> parentList,
            List<RouteExpressionParameter> paramMaps,
            IEnumerable<Expression> arguments)
        {
            var set = new HashSet<string>(parentList.Select(x => x.Name));
            foreach (var remainingArg in arguments)
            {
                var contanstExpr = remainingArg as ConstantExpression;
                if (contanstExpr != null)
                {
                    paramMaps.Add(new ConstantRouteExpressionParameter(contanstExpr.Value));
                }
                else
                {
                    var param = ((ParameterExpression)remainingArg);
                    if (!set.Contains(param.Name))
                    {
                        throw new ArgumentException("All of your variables must come as input parameters");
                    }

                    // , this.targetedParams, param.Type
                    paramMaps.Add(new FunctionalRouteExpressionParameter(param.Name));
                }
            }
        }
    }
}