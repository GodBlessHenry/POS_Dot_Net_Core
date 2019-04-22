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

        // This is a conversion operator, pass in the builder<T> object
        // and convert to a T object using below logic
        public static implicit operator T(Builder<T> builder)
        {
            if ((object)builder.Creation == null)
                throw new Exception($"Creation of {(object) typeof(T)} is null, it probably shouldn't be.");
            return builder.Creation;
        }
    }
}