namespace Base2art.Soufflot.CommandRunner.Util
{
    using System;
    using System.Collections.Generic;
    
    public static class DelegatedEqualityComparer
    {
        public static DelegatedEqualityBuilder<T> Builder<T>()
        {
            return new DelegatedEqualityBuilder<T>();
        }
        
        public static IEqualityComparer<T> Create<T, TProp>(Func<T, TProp> getter)
            where TProp : IEquatable<TProp>
        {
            return new DelegatedEqualityBuilder<T>().Build(getter);
        }
    }
}
