using System;
using System.Collections.Generic;
using AssemblyForTesting;
using MvcTurbine.ComponentModel;
using MvcTurbine.FluentValidation.Helpers;
using NUnit.Framework;

namespace MvcTurbine.FluentValidation.Tests.Helpers
{
    [TestFixture]
    public class ServiceLocatorValidatorFactoryTests
    {
        [Test]
        public void Can_resolve_a_validator_after_adding_the_type()
        {
            var serviceLocator = new TestServiceLocator();
            ServiceLocatorManager.SetLocatorProvider(() => serviceLocator);
            var factory = new ServiceLocatorValidatorFactory();

            var validatorType = typeof(Class2Validator);
            var typeToValidate = typeof (Class2InputModel);

            factory.GetValidator(typeToValidate);

            Assert.AreEqual(validatorType, serviceLocator.TypeThatWasResolved);
        }

        
        public class TestServiceLocator : IServiceLocator
        {
            public Type TypeThatWasResolved { get; set; }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                TypeThatWasResolved = typeof (T);
                return null;
            }

            public T Resolve<T>(string key) where T : class
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>(Type type) where T : class
            {
                throw new NotImplementedException();
            }

            public object Resolve(Type type)
            {
                TypeThatWasResolved = type;
                return null;
            }

            public IList<T> ResolveServices<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public IList<object> ResolveServices(Type type)
            {
                throw new NotImplementedException();
            }

            public IServiceRegistrar Batch()
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Type implType) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>() where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>(string key) where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register(string key, Type type)
            {
                throw new NotImplementedException();
            }

            public void Register(Type serviceType, Type implType)
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Interface instance) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Func<Interface> factoryMethod) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Release(object instance)
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public TService Inject<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }

            public void TearDown<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }
        }
    }
}