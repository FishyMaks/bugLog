using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace bugLog.Models
{
    public partial class UserProfile : BaseEntity
    {
        public UserProfile()
        {
            BugsBugAssignedToNavigation = new HashSet<Bug>();
            BugsBugClosedByNavigation = new HashSet<Bug>();
            BugsBugCreatedByNavigation = new HashSet<Bug>();
        }
        public int UserId { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string UserPassword { get; set; }
        [Required]
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
