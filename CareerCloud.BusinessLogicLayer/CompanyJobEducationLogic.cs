using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic: BaseLogic<CompanyJobEducationPoco>{
		public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
        {
        }
		public override void Add(CompanyJobEducationPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(CompanyJobEducationPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (string.IsNullOrEmpty(item.Major) || item.Major.Length <= 2)
                {
                 validationExceptionList.Add(new ValidationException(200, "Major must be at least 2 characters"));

                }

			if (item.Importance < 0)
                {
                 validationExceptionList.Add(new ValidationException(201, "Importance cannot be less than 0"));

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