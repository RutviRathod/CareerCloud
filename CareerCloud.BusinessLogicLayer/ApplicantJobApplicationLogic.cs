using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic: BaseLogic<ApplicantJobApplicationPoco>{
		public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository) : base(repository)
        {
        }
		public override void Add(ApplicantJobApplicationPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(ApplicantJobApplicationPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (item.ApplicationDate > DateTime.Today)
                {
                validationExceptionList.Add(new ValidationException(110, "ApplicationDate cannot be greater than today"));

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