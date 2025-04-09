using Ardalis.Result;

namespace Geneirodan.MediatR;

internal static class DynamicResults
{
    public static T Invalid<T>(ValidationError[] errors) where T : class, IResult
    {
        var type = typeof(T);
        const string methodName = nameof(Result.Invalid);

        if (type.IsGenericResult())
            return type.CreateGenericResult<T>(methodName, [typeof(IEnumerable<ValidationError>)], [errors]);

        if (type.IsResult())
            return Result.Invalid(errors) as T ??  throw new MissingMethodException(type.Name, methodName);

        throw new InvalidOperationException("Validatable requests should return 'Result' or 'Result<T>'.");
    }

    public static T Forbidden<T>() where T : class, IResult
    {
        var type = typeof(T);
        const string methodName = nameof(Result.Forbidden);

        if (type.IsGenericResult())
            return type.CreateGenericResult<T>(methodName, Type.EmptyTypes, []);

        if (type.IsResult())
            return Result.Forbidden() as T ??  throw new MissingMethodException(type.Name, methodName);

        throw new InvalidOperationException("Authorized requests should return 'Result' or 'Result<T>'.");
    }

    public static T Unauthorized<T>() where T : class, IResult
    {
        var type = typeof(T);
        const string methodName = nameof(Result.Unauthorized);

        if (type.IsGenericResult())
            return type.CreateGenericResult<T>(methodName, Type.EmptyTypes, []);

        if (type.IsResult())
            return Result.Unauthorized() as T ??  throw new MissingMethodException(type.Name, methodName);

        throw new InvalidOperationException("Authorized requests should return 'Result' or 'Result<T>'.");
    }

    private static T CreateGenericResult<T>(this Type type, string methodName, Type[] types, object?[] parameters) 
        where T : class, IResult =>
        type.GetGenericTypeDefinition()
            .MakeGenericType(type.GetGenericArguments())
            .GetMethod(methodName, types)?
            .Invoke(null, parameters) as T
        ??  throw new MissingMethodException(type.Name, methodName);
    
    private static bool IsResult(this Type type) => type == typeof(Result);

    private static bool IsGenericResult(this Type type) => 
        type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Result<>);
}