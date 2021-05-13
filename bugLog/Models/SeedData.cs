using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace bugLog.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(BugDbContext context)
        {
            context.Database.Migrate();
            if (context.Bugs.Count() == 0 && context.Teams.Count() == 0 
                && context.UserAccesses.Count() == 0 && context.UserProfiles.Count() ==0)
            {

                context.UserProfiles.AddRange(
                    new UserProfile
                    {
                        UserId = 1,
                        TeamId = 1,
                        UserName = "admin",
                        UserPassword = "password",
                        DisplayName = "Coolest Admin",
                        EmailAddress = "admin@buglog.com",
                        AllowEmailNotification = true,
                        IsActive = false
                    },
                    new UserProfile
                    {
                        UserId = 2,
                        TeamId = 1,
                        UserName = "user2",
                        UserPassword = "password",
                        DisplayName = "User 2",
                        EmailAddress = "user2@buglog.com",
                        AllowEmailNotification = true,
                        IsActive = false
                    },
                    new UserProfile
                    {
                        UserId = 3,
                        TeamId = 2,
                        UserName = "user3",
                        UserPassword = "password",
                        DisplayName = "User 3",
                        EmailAddress = "user3@buglog.com",
                        AllowEmailNotification = true,
                        IsActive = false
                    }
                    ); ;
                context.UserAccesses.AddRange(
                    new UserAccess
                    {
                        TeamId = 1,
                        UniqueKey = Guid.NewGuid(),
                        FromDate = DateTime.Now
                    },
                    new UserAccess
                    {
                        TeamId = 2,
                        UniqueKey = Guid.NewGuid(),
                        FromDate = DateTime.Now
                    }
                    );
                context.Teams.AddRange(
                    new Team 
                    {
                        TeamId = 1,
                        TeamName = "Alpha",
                        TeamMotto = "Lead The Way!"
                    },
                    new Team
                    {
                        TeamId = 2,
                        TeamName = "Bravo",
                        TeamMotto = "Best In The Market!"
                    }
                    );
                context.Bugs.AddRange(
                    new Bug 
                    {
                        BugId = 1,
                        AssignedToTeamId = 1,
                        BugReportedBy = "Sam Torres",
                        BugTitle = "Homepage login freezing",
                        BugDescription = "When trying to login, homepage freezes.",
                        BugAssignedTo = 1,
                        BugStatus = "Open",
                        BugSeverity = "Critical",
                        BugCreatedBy = 1,
                        BugCreatedOn = DateTime.Now,
                        BugClosedBy = null,
                        BugClosedOn = null,
                        BugResolutionSummary = "In progress."
                    },
                    new Bug
                    {
                        BugId = 2,
                        AssignedToTeamId = 1,
                        BugReportedBy = "Sara Conner",
                        BugTitle = "Locked account",
                        BugDescription = "Password is not accepted by website.",
                        BugAssignedTo = 2,
                        BugStatus = "Open",
                        BugSeverity = "Minor",
                        BugCreatedBy = 1,
                        BugCreatedOn = DateTime.Now,
                        BugClosedBy = null,
                        BugClosedOn = null,
                        BugResolutionSummary = "Bug Reported."
                    },
                    new Bug
                    {
                        BugId = 3,
                        AssignedToTeamId = 2,
                        BugReportedBy = "Chad Williams",
                        BugTitle = "Account details",
                        BugDescription = "Can't change username.",
                        BugAssignedTo = 3,
                        BugStatus = "Open",
                        BugSeverity = "Minor",
                        BugCreatedBy = 1,
                        BugCreatedOn = DateTime.Now,
                        BugClosedBy = null,
                        BugClosedOn = null,
                        BugResolutionSummary = "Bug Reported."
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
