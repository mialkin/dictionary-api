using System.Reflection;
using FluentAssertions;

namespace Dictionary.Api.Domain.UnitTests;

public sealed class ErrorTests
{
    [Fact]
    public void All_application_error_codes_must_be_unique()
    {
        var methods = typeof(Errors)
            .GetNestedTypes()
            .Select(x => x
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(y => y.ReturnType == typeof(Error))
                .ToList())
            .SelectMany(x => x)
            .ToList();

        var numberOfUniqueCodes = methods.Select(GetErrorCode)
            .Distinct()
            .Count();

        numberOfUniqueCodes.Should().Be(methods.Count);
    }

    private static string GetErrorCode(MethodInfo method)
    {
        var parameters = method.GetParameters()
            .Select<ParameterInfo, object>(x =>
            {
                if (x.ParameterType == typeof(string))
                {
                    return string.Empty;
                }

                if (x.ParameterType == typeof(Guid?))
                {
                    return Guid.Empty;
                }

                throw new Exception();
            })
            .ToArray();

        var error = (Error)method.Invoke(null, parameters)!;
        return error.Code;
    }
}
