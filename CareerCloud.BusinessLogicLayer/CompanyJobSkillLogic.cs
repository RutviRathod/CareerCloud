using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobSkillLogic: BaseLogic<CompanyJobSkillPoco>{
		public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repository) : base(repository)
        {
        }
		public override void Add(CompanyJobSkillPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(CompanyJobSkillPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(CompanyJobSkillPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (item.Importance < 0)
                {
 validationExceptionList.Add(new ValidationException(400, "Importance cannot be less than 0"));

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