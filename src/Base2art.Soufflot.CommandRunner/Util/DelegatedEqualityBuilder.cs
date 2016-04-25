
namespace Base2art.Soufflot.CommandRunner.Util
{
    using System;
    using System.Collections.Generic;

    public class DelegatedEqualityBuilder<T>
    {
        public IEqualityComparer<T> Build<TProp>(Func<T, TProp> getter)
            where TProp : IEquatable<TProp>
        {
            return new InternalComparer<TProp>(getter);
        }
		
        
        private class InternalComparer<TProp> : IEqualityComparer<T>
            where TProp : IEquatable<TProp>
        {
            private readonly Func<T, TProp> getter;

            public InternalComparer(Func<T, TProp> getter)
            {
                this.getter = getter;
            }
            
            bool IEqualityComparer<T>.Equals(T x, T y)
            {
                IEquatable<TProp> tProp = this.getter(x);
                return tProp.Equals(this.getter(y));
            }
            
            int IEqualityComparer<T>.GetHashCode(T obj)
            {
                IEquatable<TProp> tProp = this.getter(obj);
                return tProp.GetHashCode();
            }
        }
    }
}


