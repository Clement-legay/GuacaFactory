using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GuacaFactory.Shared.Models;

public class Administrator
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Token { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public string? EmployeeUsername { get; set; }
}

public class AdministratorRegistryDto
{
    [Required] public int? EmployeeId { get; set; }

    [Required] [EmailAddress] public string? Email { get; set; }

    [Required]
    [SecuredPassword(ErrorMessage =
        "Password must contain at least 8 characters, 1 uppercase, 1 lowercase, 1 number and 1 special character.")]
    public string? Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string? ConfirmPassword { get; set; }
}

public class AdministratorUpdatePasswordDto
{
    [Required]
    [SecuredPassword(ErrorMessage =
        "Password must contain at least 8 characters, 1 uppercase, 1 lowercase, 1 number and 1 special character.")]
    public string? Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string? ConfirmPassword { get; set; }
}

public class AdministratorUpdateEmailDto
{
    [Required] [EmailAddress] public string? Email { get; set; }
}

public class AdministratorLoginDto
{
    [Required] [EmailAddress] public string? Email { get; set; }

    [Required] public string? Password { get; set; }
}

public class AdministratorPersistDto
{
    [Required] public string? Token { get; set; }
}

public class SecuredPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = (string?)value;
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
        var isValidated = password != null && hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) &&
                          hasLowerChar.IsMatch(password) && hasSymbols.IsMatch(password) && password.Length >= 8;
        return !isValidated ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
    }
}