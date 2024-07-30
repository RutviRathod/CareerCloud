using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("Time_Stamp")]
        public byte[]? TimeStamp { get; set; }

        [Column("Login")]
        public Guid Login { get; set; }

        [Column("Role")]
        public Guid Role { get; set; }

        public virtual SecurityLoginPoco SecurityLogin { get; set; }

        public virtual SecurityRolePoco SecurityRole { get; set; }

        public virtual SecurityLoginsLogPoco SecurityLoginsLog { get; set; }

        
    }
}
