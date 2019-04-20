using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSubstitute;

namespace POS.Core.Tests.Helpers
{
    public class AutoMocker
    {
        private readonly IDictionary<Type, object> _dependencies;

        public AutoMocker()
        {
            this._dependencies = (IDictionary<Type, object>)new Dictionary<Type, object>();
        }

        public TSystemUnderTest Create<TSystemUnderTest>() where TSystemUnderTest : class
        {
            ConstructorInfo[] constructors = typeof(TSystemUnderTest).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (constructors.Length != 1)
                throw new Exception(string.Format("Automocker currently only supports one constructor but found {0}", (object)constructors.Length));
            ConstructorInfo constructorInfo = constructors[0];
            List<object> parameters = new List<object>();
            ((IEnumerable<ParameterInfo>)constructorInfo.GetParameters()).ToList<ParameterInfo>().ForEach((Action<ParameterInfo>)(parameter =>
            {
                if (this._dependencies.ContainsKey(parameter.ParameterType))
                    parameters.Add(this._dependencies[parameter.ParameterType]);
                else if (parameter.ParameterType.IsInterface)
                {
                    object obj = Substitute.For(new Type[1]
                    {
                        parameter.ParameterType
                    }, new object[0]);
                    this._dependencies[parameter.ParameterType] = obj;
                    parameters.Add(obj);
                }
                else
                {
                    if (!parameter.HasDefaultValue)
                        return;
                    parameters.Add(parameter.DefaultValue);
                }
            }));
            return (TSystemUnderTest)constructorInfo.Invoke(parameters.ToArray());
        }

        public TDependency Mock<TDependency>()
        {
            Type key = typeof(TDependency);
            if (!this._dependencies.ContainsKey(key))
                this._dependencies[key] = Substitute.For(new Type[1]
                {
                    key
                }, new object[0]);
            return (TDependency)this._dependencies[key];
        }

        public void Register<T>(T dependancy)
        {
            this._dependencies[typeof(T)] = (object)dependancy;
        }
    }
}