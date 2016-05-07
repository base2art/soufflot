namespace Base2art.Soufflot.Api.Routing.Expressive
{
    public class RouteExpressionTree
    {
        private readonly string methodName;

        private readonly FunctionalRouteExpressionParameter[] inputParameters;

        private readonly RouteExpressionParameter[] parameters;

        public RouteExpressionTree(
            string methodName,
            FunctionalRouteExpressionParameter[] inputParameters, 
            RouteExpressionParameter[] parameters)
        {
            this.methodName = methodName;
            this.inputParameters = inputParameters;
            this.parameters = parameters;
        }

        public string MethodName
        {
            get { return this.methodName; }
        }

        public FunctionalRouteExpressionParameter[] InputParameters
        {
            get { return this.inputParameters ?? new FunctionalRouteExpressionParameter[0]; }
        }

        public RouteExpressionParameter[] Parameters
        {
            get { return this.parameters ?? new RouteExpressionParameter[0]; }
        }
    }
}
