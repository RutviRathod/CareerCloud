using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic: BaseLogic<SystemLanguageCodePoco>{
		public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) : base(repository)
        {
        }
		public override void Add(SystemLanguageCodePoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(SystemLanguageCodePoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(SystemLanguageCodePoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (string.IsNullOrEmpty(item.LanguageID))
                {
 validationExceptionList.Add(new ValidationException(1000, "Cannot be empty"));

}

			if (string.IsNullOrEmpty(item.Name))
                {
 validationExceptionList.Add(new ValidationException(1001, "Cannot be empty"));

}

			if (string.IsNullOrEmpty(item.NativeName))
                {
 validationExceptionList.Add(new ValidationException(1002, "Cannot be empty"));

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