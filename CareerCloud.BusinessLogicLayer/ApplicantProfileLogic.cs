using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic: BaseLogic<ApplicantProfilePoco>{
		public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
        }
		public override void Add(ApplicantProfilePoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(ApplicantProfilePoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (item.CurrentSalary < 0)
                {
                     validationExceptionList.Add(new ValidationException(111, "CurrentSalary cannot be negative"));

                    }

			if (item.CurrentRate < 0)
                {
                 validationExceptionList.Add(new ValidationException(112, "CurrentRate cannot be negative"));

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