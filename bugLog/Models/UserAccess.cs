using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bugLog.Models
{
    public partial class UserAccess : BaseEntity
    {
        public UserAccess()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int TeamId { get; set; }
        public Guid UniqueKey { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
