using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic: BaseLogic<CompanyLocationPoco>{
		public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }
		public override void Add(CompanyLocationPoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(CompanyLocationPoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(CompanyLocationPoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (string.IsNullOrEmpty(item.CountryCode))
                {
                 validationExceptionList.Add(new ValidationException(500, "CountryCode cannot be empty"));

                }

			if (string.IsNullOrEmpty(item.Province))
                {
 validationExceptionList.Add(new ValidationException(501, "Province cannot be empty"));

}

			if (string.IsNullOrEmpty(item.Street))
                {
 validationExceptionList.Add(new ValidationException(502, "Street cannot be empty"));

}

			if (string.IsNullOrEmpty(item.City))
                {
 validationExceptionList.Add(new ValidationException(503, "City cannot be empty"));

}

			if (string.IsNullOrEmpty(item.PostalCode))
                {
 validationExceptionList.Add(new ValidationException(504, "PostalCode cannot be empty"));

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