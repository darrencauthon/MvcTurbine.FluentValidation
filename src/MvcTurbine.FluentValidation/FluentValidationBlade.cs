using System.Web.Mvc;
using FluentValidation.Mvc;
using MvcTurbine.Blades;
using MvcTurbine.FluentValidation.Helpers;

namespace MvcTurbine.FluentValidation
{
    public class FluentValidationBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            var fluentValidationModelValidatorProvider = new FluentValidationModelValidatorProvider(new ServiceLocatorValidatorFactory()) { AddImplicitRequiredValidator = false };
            ModelValidatorProviders.Providers.Add(fluentValidationModelValidatorProvider);  
        }
    }
}