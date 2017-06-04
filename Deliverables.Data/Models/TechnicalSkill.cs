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
    public class TechnicalSkill : BaseEntity
    {
        [Key]
        public int TechnicalSkillId { get; set; }

        public string Name { get; set; }

        public int LevelId { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }

        public virtual ICollection<Deliverable> Deliverables { get; set; }
    }
}
