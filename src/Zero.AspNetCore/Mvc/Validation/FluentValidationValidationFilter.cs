using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Zero.AspNetCore.Mvc.Validation
{
    public class FluentValidationValidationFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                await next();
                return;
            }
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }
#pragma warning disable CS0618 
            var validatorFactory = context.HttpContext.RequestServices.GetRequiredService<IValidatorFactory>();
#pragma warning restore CS0618 
            if (context.ActionArguments.Count > 0)
            {
                var validationErrors = new List<ValidationResult>();
                foreach (var arg in context.ActionArguments)
                {
                    if (arg.Value == null)
                        continue;
                    var validator = validatorFactory.GetValidator(arg.Value.GetType());
                    if (validator == null)
                        continue;
                    var validationContext = new ValidationContext<object>(arg.Value);
                    var validationResult = validator.Validate(validationContext);

                    if (!validationResult.IsValid)
                    {
                        var mappedValidationErrors = validationResult.Errors
                        .Select(e => new ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                        .ToList();
                        validationErrors.AddRange(mappedValidationErrors);
                    }
                }
                if (validationErrors.Count > 0)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    for (int i = 0; i < validationErrors.Count; i++)
                    {
                        messageBuilder.Append($"{i + 1}.{string.Join(' ', validationErrors[i].MemberNames)}:{validationErrors[i].ErrorMessage} ");
                    }
                    throw new ValidationException($"参数未验证通过，{messageBuilder}");
                }
            }
            await next();
        }
    }
}
