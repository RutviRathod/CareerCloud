using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic: BaseLogic<ApplicantEducationPoco>{
		public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }
		public override void Add(ApplicantEducationPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(ApplicantEducationPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (!string.IsNullOrEmpty(item.Major) && item.Major.Length <= 3)
                {
                validationExceptionList.Add(new ValidationException(107, "Cannot be empty or less than 3 characters"));

                }

			if (item.StartDate >= DateTime.Today)
                {
                 validationExceptionList.Add(new ValidationException(108, "Cannot be greater than today"));

                }

			if (item.CompletionDate <= item.StartDate)
                {
                 validationExceptionList.Add(new ValidationException(109, "CompletionDate cannot be earlier than StartDate"));

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