namespace Base2art.Soufflot.Api.Routing.Expressive
{
    public class ConstantRouteExpressionParameter : RouteExpressionParameter
    {
        private readonly object value;

        public ConstantRouteExpressionParameter(object value)
        {
            this.value = value;
        }

        public object Value
        {
            get
            {
                return this.value;
            }
        }
    }
}