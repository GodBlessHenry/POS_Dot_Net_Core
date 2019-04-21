using System;

namespace POS.Core.Tests.Builders
{
    public class Builder<T> where T : class
    {
        public Builder()
        {
        }

        public Builder(T creation)
        {
            this.Creation = creation;
        }

        public T Creation { get; protected set; }

        public static implicit operator T(Builder<T> builder)
        {
            if ((object)builder.Creation == null)
                throw new Exception(string.Format("Creation of {0} is null, it probably shouldn't be.", (object)typeof(T)));
            return builder.Creation;
        }
    }
}