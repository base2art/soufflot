namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Text.RegularExpressions;

    public class FunctionalRouteExpressionParameter : RouteExpressionParameter
    {
        private readonly string name;

//        private readonly Regex parameters;

//        private readonly Type type;

        public FunctionalRouteExpressionParameter(string name)
        {
            //, Regex parameters, Type type
            this.name = name;
//            this.parameters = parameters;
//            this.type = type;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

//        public Regex Parameters
//        {
//            get
//            {
//                return this.parameters;
//            }
//        }

//        public Type Type
//        {
//            get
//            {
//                return this.type;
//            }
//        }
    }
}
