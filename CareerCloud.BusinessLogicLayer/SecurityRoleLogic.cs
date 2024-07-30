using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityRoleLogic: BaseLogic<SecurityRolePoco>{
		public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository) : base(repository)
        {
        }
		public override void Add(SecurityRolePoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(SecurityRolePoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(SecurityRolePoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {

			if (string.IsNullOrEmpty(item.Role))
                {
                 validationExceptionList.Add(new ValidationException(800, "Cannot be empty"));

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