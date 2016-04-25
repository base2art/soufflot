namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;

    using System.Reflection;
    using Base2art.Soufflot.Mvc;
    using Base2art.Validation;

    public abstract class ExpressiveRouteValidatorBase<TController>
        where TController : IRenderingController
    {

        protected RouteExpressionTree VerifyNonFunctionalExpression(
            Expression func)
        {
            var lambda = func.Validate().IsA<LambdaExpression>().And();

            MethodCallExpression method = this.ValidateLambda(lambda.Value);

            this.ValidateLambdaInputParamsArePassedToInnerCallAsFirstTwoParams(
                lambda.Value.Parameters.Skip(1).ToArray(),
                method.Arguments);

            List<FunctionalRouteExpressionParameter> lambdaParamMaps = new List<FunctionalRouteExpressionParameter>();
            this.ValidateLambdaAdditionalInputParameters(lambdaParamMaps, lambda.Value.Parameters.Skip(3).ToArray());

            List<RouteExpressionParameter> childParamMaps = new List<RouteExpressionParameter>();
            this.VerifyAndAddNonDefaultPassthroughParams(lambdaParamMaps, childParamMaps, method.Arguments.Skip(2));

            return new RouteExpressionTree(
                method.Method.Name,
                lambdaParamMaps.ToArray(),
                childParamMaps.ToArray());
        }

        protected virtual void ValidateLambdaAdditionalInputParameters(
            List<FunctionalRouteExpressionParameter> paramMaps,
            ParameterExpression[] toArray)
        {
        }

        protected virtual void VerifyAndAddNonDefaultPassthroughParams(
            List<FunctionalRouteExpressionParameter> parentList,
            List<RouteExpressionParameter> paramMaps,
            IEnumerable<Expression> arguments)
        {
            foreach (var remainingArg in arguments)
            {
                object val = null;
                var constantExpression = remainingArg as ConstantExpression;
                if (constantExpression != null)
                {
                    val = constantExpression.Value;
                }
                
                var memberExpression = remainingArg as MemberExpression;
                // http://stackoverflow.com/a/2616980/390632
                if (memberExpression != null)
                {
                    var objectMember = Expression.Convert(memberExpression, typeof(object));
                    var getterLambda = Expression.Lambda<Func<object>>(objectMember);
                    var getter = getterLambda.Compile();
                    val = getter();
                }
                
                paramMaps.Add(new ConstantRouteExpressionParameter(val));
            }
        }

        protected void ValidateLambdaInputParamsArePassedToInnerCallAsFirstTwoParams(ParameterExpression[] parms, ReadOnlyCollection<Expression> arguments)
        {
            arguments.Count.Validate().GreaterThanOrEqualTo(parms.Length);

            for (int i = 0; i < 2; i++)
            {
                var param = arguments[i].Validate().IsA<ParameterExpression>().Value;
                param.Name.Validate().Is(parms[i].Name);
            }
        }

        protected virtual MethodCallExpression ValidateLambda(LambdaExpression lambda)
        {
            var method = lambda.Body.Validate().IsA<MethodCallExpression>().Value;

            method.Object.Validate()
                .IsA<ParameterExpression>()
                .And(x => x.Name)
                .IsNotNull()
                .And()
                .Is(lambda.Parameters[0].Name);
            return method;
        }
    }
}

/*

            //            var remainingArgs = arguments.Skip(2);
            //
            //            List<RouteExpressionParameter> paramMaps = new List<RouteExpressionParameter>();
            //            foreach (var remainingArg in remainingArgs)
            //            {
            //                paramMaps.Add(new ConstantRouteExpressionParameter(((ConstantExpression)remainingArg).Value));
            //            }
            //

            for (int i = 2; i < arguments.Count; i++)
            {
                var param = arguments[i].Validate().IsA<ParameterExpression>().Value;
                param.Name.Validate().Is(parms[i].Name);
            }
 
 
            var names = new HashSet<string>();
            if (parameters != null)
            {
                parameters.GetGroupNames().Skip(1).ForAll(x => names.Add(x));
            }

 
 * 
 * 

            if (names.Count > 0)
            {
                var errorMessage = string.Format(
                    "The regex you have has named capture groups that are not params in the route: '{0}'",
                    parameters);

                throw new InvalidOperationException(errorMessage);
            }
 */