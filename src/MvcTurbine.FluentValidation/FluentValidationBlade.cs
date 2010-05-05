using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FluentValidation.Mvc;
using MvcTurbine.Blades;

namespace MvcTurbine.FluentValidation
{
    public class FluentValidationBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            StopMvcFromRequiringAllNonNullFields();
            AddAFluentValidationModelValidatorProvider(context);
        }

        private static void StopMvcFromRequiringAllNonNullFields()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        private static void AddAFluentValidationModelValidatorProvider(IRotorContext context)
        {
            var fluentValidationModelValidatorProvider = CreateFluentValidationModelValidatorProvider(context);
            ModelValidatorProviders.Providers.Add(fluentValidationModelValidatorProvider);
        }

        private static FluentValidationModelValidatorProvider CreateFluentValidationModelValidatorProvider(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;
            var validatorFactory = new ServiceLocatorValidatorFactory(serviceLocator);

            var retriever = new ValidatorRetriever();
            foreach (var type in retriever.GetAllMessageHandlerTypes())
                validatorFactory.AddValidatorToBeResolved(type);

            return new FluentValidationModelValidatorProvider(validatorFactory);
        }

        private class ValidatorRetriever
        {
            public IEnumerable<Type> GetAllMessageHandlerTypes()
            {
                var list = new List<Type>();

                foreach (var assembly in GetAllAssemblies())
                    list.AddRange(GetAllMessageHandlersInThisAssembly(assembly));

                return list;
            }

            private static IEnumerable<Type> GetAllMessageHandlersInThisAssembly(Assembly assembly)
            {

                return assembly.GetTypes()
                    .Where(x => x.GetInterfaces() != null && x.GetInterfaces().Any(i => (i.FullName ?? string.Empty).StartsWith("FluentValidation.IValidator`1")));
            }

            private static IEnumerable<Assembly> GetAllAssemblies()
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.FullName.StartsWith("FluentValidation,") == false)
                    .ToList();
            }
        }
    }
}