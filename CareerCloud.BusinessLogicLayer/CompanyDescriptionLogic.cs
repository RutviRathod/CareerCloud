using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic: BaseLogic<CompanyDescriptionPoco>{
		public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }
		public override void Add(CompanyDescriptionPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(CompanyDescriptionPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (string.IsNullOrEmpty(item.CompanyDescription) || item.CompanyDescription.Length <= 2)
                {
                 validationExceptionList.Add(new ValidationException(107, "CompanyDescription must be greater than 2 characters"));

                }

			if (string.IsNullOrEmpty(item.CompanyName) || item.CompanyName.Length <= 2)
                {
                 validationExceptionList.Add(new ValidationException(106, "CompanyName must be greater than 2 characters"));

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