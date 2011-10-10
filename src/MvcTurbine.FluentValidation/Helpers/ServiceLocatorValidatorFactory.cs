using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.FluentValidation.Helpers
{
    public class ServiceLocatorValidatorFactory : IValidatorFactory
    {
        private readonly Dictionary<Type, Type> modelValidatorMap;

        public ServiceLocatorValidatorFactory()
        {
            var types = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                        where (assembly != typeof(AbstractValidator<>).Assembly)
                        from type in assembly.GetTypes()
                        select type;

            modelValidatorMap = (from type in types
                                 where typeof(IValidator).IsAssignableFrom(type)
                                 where type.BaseType != null && type.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)
                                 select type).ToDictionary(type => type.BaseType.GetGenericArguments()[0], type => type);
        }

        public IValidator CreateInstance(Type validatorType)
        {
            return ServiceLocatorManager.Current.Resolve(validatorType) as IValidator;
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            if (modelValidatorMap.ContainsKey(type))
                return (IValidator)ServiceLocatorManager.Current.Resolve(modelValidatorMap[type]);

            return null;
        }
    }
}
