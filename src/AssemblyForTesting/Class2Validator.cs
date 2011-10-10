using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace AssemblyForTesting
{
    public class Class2InputModel
    {
        public string Name { get; set; }
    }

    public class Class2Validator : AbstractValidator<Class2InputModel>
    {
    }
}
