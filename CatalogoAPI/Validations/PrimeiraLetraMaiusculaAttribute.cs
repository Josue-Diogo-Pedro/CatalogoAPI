using System.ComponentModel.DataAnnotations;

namespace CatalogoAPI.Validations;

public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value is null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }
        
        var primeiraLetra = value.ToString()[0].ToString();
        if(primeiraLetra != primeiraLetra.ToUpper())
        {
            return new ValidationResult("A primeira letra do nome do produto deve ser maiúscula");
        }

        return ValidationResult.Success;
    }
}
