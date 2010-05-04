using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.FluentValidation
{
    public class ServiceLocatorValidatorFactory : IValidatorFactory
    {
        private readonly IList<ValidatorMapping> validatorMappings;
        private readonly IServiceLocator serviceLocator;

        public ServiceLocatorValidatorFactory(IServiceLocator serviceLocator)
        {
            validatorMappings = new List<ValidatorMapping>();
            this.serviceLocator = serviceLocator;
        }

        public void AddValidatorToBeResolved(Type validatorType)
        {
            var fluentValidationIValidatorType = validatorType.GetInterfaces()
                .Where(x => x.IsGenericType && x.FullName.StartsWith("FluentValidation.IValidator`1"))
                .FirstOrDefault();

            if (fluentValidationIValidatorType == null)
                throw new ArgumentException("May only pass IValidator<T> to AddValidatorToBeResolved.");

            var typeToValidate = fluentValidationIValidatorType.GetGenericArguments()[0];
            validatorMappings.Add(new ValidatorMapping{
                                                          TypeToValidate = typeToValidate,
                                                          ValidatorType = validatorType
                                                      });
        }

        public IValidator<T> GetValidator<T>()
        {
            return GetValidator(typeof (T)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
            var validatorType = GetTheValidatorForThisType(type);

            ThrowInvalidExceptionIfNoValidatorHasBeenRegistered(type, validatorType);

            return serviceLocator.Resolve(validatorType) as IValidator;
        }

        private void ThrowInvalidExceptionIfNoValidatorHasBeenRegistered(Type type, Type validatorType)
        {
            if (TheValidatorHasNotBeenAdded(validatorType))
                throw new ArgumentException(string.Format("The {0} type was not registered with the validator factory.", type.Name));
        }

        private bool TheValidatorHasNotBeenAdded(Type validatorType)
        {
            return validatorType == null;
        }

        private Type GetTheValidatorForThisType(Type type)
        {
            return validatorMappings
                .Where(x => x.TypeToValidate == type)
                .Select(x => x.ValidatorType)
                .FirstOrDefault();
        }

        private class ValidatorMapping
        {
            public Type TypeToValidate { get; set; }
            public Type ValidatorType { get; set; }
        }
    }
}