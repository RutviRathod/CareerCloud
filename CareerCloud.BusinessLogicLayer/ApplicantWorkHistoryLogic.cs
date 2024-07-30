using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic: BaseLogic<ApplicantWorkHistoryPoco>{
		public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
        {
        }
		public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (item.CompanyName.Length <= 2)
                {
                 validationExceptionList.Add(new ValidationException(105, "Must be greater then 2 characters"));

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