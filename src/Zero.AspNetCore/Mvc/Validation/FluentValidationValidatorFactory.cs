namespace Zero.AspNetCore.Mvc.Validation
{
#pragma warning disable CS0618
    public class FluentValidationValidatorFactory : ValidatorFactoryBase
#pragma warning restore CS0618 
    {
        private readonly IServiceProvider _serviceProvider;
        public FluentValidationValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public override IValidator CreateInstance(Type validatorType)
        {
            return _serviceProvider.GetService(validatorType) as IValidator;
        }
    }
}
