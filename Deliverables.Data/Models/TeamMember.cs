using Deliverables.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverables.Data
{
    public class TeamMember : BaseEntity
    {
        [Key]
        public int TeamMemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public virtual ICollection<TechnicalSkill> TechnicalSkills { get; set; }
    }
}
