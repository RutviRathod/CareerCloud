using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityLoginsRoleLogic: BaseLogic<SecurityLoginsRolePoco>{
		public SecurityLoginsRoleLogic(IDataRepository<SecurityLoginsRolePoco> repository) : base(repository)
        {
        }
		public override void Add(SecurityLoginsRolePoco[] pocos)
        {

            Verify(pocos);
            base.Add(pocos);
        }
		public override void Update(SecurityLoginsRolePoco[] pocos)
        {

            Verify(pocos);
            base.Update(pocos);
        }
		 protected override void Verify(SecurityLoginsRolePoco[] pocos)
        {
            
            var validationExceptionList = new HashSet<ValidationException>();
            foreach (var item in pocos)
            {
			}
			if (validationExceptionList.Count > 0)
            {
                throw new AggregateException(validationExceptionList);
                
            }
            base.Verify(pocos); 
		}

      

    }
}