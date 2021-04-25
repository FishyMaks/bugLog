using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bugLog.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            BugsBugAssignedToNavigation = new HashSet<Bug>();
            BugsBugClosedByNavigation = new HashSet<Bug>();
            BugsBugCreatedByNavigation = new HashSet<Bug>();
        }

        public int UserId { get; set; }
        public int TeamId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public bool AllowEmailNotification { get; set; }

        public virtual Team Team { get; set; }
        public virtual UserAccess TeamNavigation { get; set; }
        public virtual ICollection<Bug> BugsBugAssignedToNavigation { get; set; }
        public virtual ICollection<Bug> BugsBugClosedByNavigation { get; set; }
        public virtual ICollection<Bug> BugsBugCreatedByNavigation { get; set; }
    }
}
