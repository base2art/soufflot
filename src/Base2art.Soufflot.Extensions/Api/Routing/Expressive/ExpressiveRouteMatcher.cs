namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Text.RegularExpressions;

    using Base2art.Validation;

    public static class ExpressiveRouteMatcher
    {
        public static bool IsMatch(string requestValue, string routeData, StringComparison comparer)
        {
            if (!string.IsNullOrWhiteSpace(routeData))
            {
                if (!string.Equals(requestValue, routeData, comparer))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsMatchRegex(string requestValue, Regex routeData)
        {
            if (routeData != null)
            {
                if (requestValue == null)
                {
                    return false;
                }

                if (!routeData.IsMatch(requestValue))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsMatch<T>(T? requestValue, T? routeData)
            where T : struct
        {
            if (!routeData.HasValue)
            {
                return true;
            }

            if (!requestValue.HasValue)
            {
                return false;
            }

            return requestValue.Value.Equals(routeData.Value);
        }

        public static bool IsMatch(RouteExpressionParameter[] arr1, object[] arr2)
        {
            arr1.Validate().IsNotNull();
            arr2.Validate().IsNotNull();

            if (arr1.Length != arr2.Length)
            {
                return false;
            }

            for (int i = 0; i < arr1.Length; i++)
            {
                var item1Param = arr1[i];
                object item1 = null;
                var constantRouteExpressionParameter = item1Param as ConstantRouteExpressionParameter;
                if (constantRouteExpressionParameter != null)
                {
                    item1 = constantRouteExpressionParameter.Value;
                }

                var functionalRouteExpressionParameter = item1Param as FunctionalRouteExpressionParameter;
                if (functionalRouteExpressionParameter != null)
                {
                    continue;
                }

                var item2 = arr2[i];

                if (!IsItemMatch(item1, item2))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsItemMatch(object item1, object item2)
        {
            if (item1 == null ^ item2 == null)
            {
                return false;
            }

            if (item1 == null)
            {
                return true;
            }

            if (!item1.Equals(item2))
            {
               return false;
            }

            return true;
        }
    }
}
