﻿using System;

namespace bugLog.Models
{
    public partial class Bug : BaseEntity
    {
        public int BugId { get; set; }
        public int AssignedToTeamId { get; set; }
        public string BugReportedBy { get; set; }
        public string BugTitle { get; set; }
        public string BugDescription { get; set; }
        public int BugAssignedTo { get; set; }
        public string BugStatus { get; set; }
        public string BugSeverity { get; set; }
        public int BugCreatedBy { get; set; }
        public DateTime BugCreatedOn { get; set; }
        public int? BugClosedBy { get; set; }
        public DateTime? BugClosedOn { get; set; }
        public string BugResolutionSummary { get; set; }

        public virtual UserProfile BugAssignedToNavigation { get; set; }
#nullable enable
        public virtual UserProfile? BugClosedByNavigation { get; set; }
#nullable disable
        public virtual UserProfile BugCreatedByNavigation { get; set; }
    }
}
