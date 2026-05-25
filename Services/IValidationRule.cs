namespace Task2_Modules.Services;

public interface IValidationRule
{
    bool Validate(object data, out string? error);
}

public class NonEmptyNameRule : IValidationRule
{
    public bool Validate(object data, out string? error)
    {
        var name = data?.GetType().GetProperty("Name")?.GetValue(data) as string;
        if (string.IsNullOrWhiteSpace(name))
        {
            error = "Имя не может быть пустым";
            return false;
        }
        error = null;
        return true;
    }
}