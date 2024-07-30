using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic: BaseLogic<CompanyProfilePoco>{
		public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }
		public override void Add(CompanyProfilePoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(CompanyProfilePoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(CompanyProfilePoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (string.IsNullOrEmpty(item.CompanyWebsite) ||
              !(item.CompanyWebsite.EndsWith(".ca") || item.CompanyWebsite.EndsWith(".com") || item.CompanyWebsite.EndsWith(".biz")))
                {
 validationExceptionList.Add(new ValidationException(600, "Valid websites must end with the following extensions – .ca, .com, .biz"));

}

			if (string.IsNullOrEmpty(item.ContactPhone) || !Regex.IsMatch(item.ContactPhone, @"^\d{3}-\d{3}-\d{4}$"))
                {
                 validationExceptionList.Add(new ValidationException(601, "Must correspond to a valid phone number (e.g. 416-555-1234)"));

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