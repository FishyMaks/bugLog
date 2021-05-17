using System.Collections.Generic;

namespace bugLog.Models
{
    public partial class Team : BaseEntity
    {
        public Team()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamMotto { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
