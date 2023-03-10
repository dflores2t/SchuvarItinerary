using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SchuvarItinerary.Infrastructure;
[AttributeUsage(AttributeTargets.Class)]

public class CustomDate : Attribute, IModelValidator
{
  public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
  {
    if (Convert.ToDateTime(context.Model) <= DateTime.Now)
    {
      return new List<ModelValidationResult> {
                    new ModelValidationResult("", "Date should be greather than now")
                };
    }
    else
    {
      return Enumerable.Empty<ModelValidationResult>();
    }
  }
}