using FluentValidation;

namespace MvcTurbine.FluentValidation.Tests
{
    public class TestItem
    {
    }

    public class SecondTestItem{}
    

    public class TestItemValidator : AbstractValidator<TestItem>
    {
    }

    public class SecondTestItemValidator : AbstractValidator<SecondTestItem>
    {
    }
}