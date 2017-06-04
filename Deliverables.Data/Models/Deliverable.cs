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
    public class Deliverable: BaseEntity
    {
        [Key]
        public int DeliverableID { get; set; }

        public int TypeId { get; set; }

        public string Desciption { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }

        public virtual ICollection<TechnicalSkill> TechnicalSkills { get; set; }
    }
}
