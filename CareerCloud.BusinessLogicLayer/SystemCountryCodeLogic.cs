using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic: BaseLogic<SystemCountryCodePoco>{
		public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository) : base(repository)
        {
        }
		public override void Add(SystemCountryCodePoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(SystemCountryCodePoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(SystemCountryCodePoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (string.IsNullOrEmpty(item.Code))
                {
 validationExceptionList.Add(new ValidationException(900, "Cannot be empty"));

}

			if (string.IsNullOrEmpty(item.Name))
                {
 validationExceptionList.Add(new ValidationException(901, "Cannot be empty"));

}
			}
			if (validationExceptionList.Count > 0)
            {
                throw new AggregateException(validationExceptionList);
                
            }
            base.Verify(pocos); 
		}

      

    }
}