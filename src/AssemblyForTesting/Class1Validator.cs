using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace AssemblyForTesting
{
    public class Class1InputModel
    {
        public string Name { get; set; }
    }

    public class Class1Validator : AbstractValidator<Class1InputModel>
    {
    }
}
