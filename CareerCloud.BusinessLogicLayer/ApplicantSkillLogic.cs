using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic: BaseLogic<ApplicantSkillPoco>{
		public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }
		public override void Add(ApplicantSkillPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(ApplicantSkillPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (item.StartMonth > 12)
                {
                 validationExceptionList.Add(new ValidationException(101, "Cannot be greater than 12"));

                }

			if (item.EndMonth > 12)
                {
                 validationExceptionList.Add(new ValidationException(102, "Cannot be greater than 12"));

                }

			if (item.StartYear < 1990)
                {
                 validationExceptionList.Add(new ValidationException(103, "Cannot be less then 1900"));

                }

			if (item.EndYear < item.StartYear)
                {
                 validationExceptionList.Add(new ValidationException(104, "Cannot be less then StartYear"));

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