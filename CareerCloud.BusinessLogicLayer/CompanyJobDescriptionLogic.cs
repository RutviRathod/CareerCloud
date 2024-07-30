using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic: BaseLogic<CompanyJobDescriptionPoco>{
		public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
        {
        }
		public override void Add(CompanyJobDescriptionPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(CompanyJobDescriptionPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (string.IsNullOrEmpty(item.JobName))
                {
                 validationExceptionList.Add(new ValidationException(300, "JobName cannot be empty"));

                }

			if (string.IsNullOrEmpty(item.JobDescriptions))
                {
                 validationExceptionList.Add(new ValidationException(301, "JobDescriptions cannot be empty"));

                }

			if (string.IsNullOrEmpty(item.JobName))
                {
                 validationExceptionList.Add(new ValidationException(300, "JobName cannot be null"));

                }

			if (string.IsNullOrEmpty(item.JobDescriptions))
                {
                 validationExceptionList.Add(new ValidationException(301, "JobDescriptions cannot be null"));

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