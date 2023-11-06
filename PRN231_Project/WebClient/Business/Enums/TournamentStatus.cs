using System.ComponentModel;

namespace CoFAB.Business.Enums
{
    public enum TournamentStatus
    {
        [Description("UpComing")]
        UpComing = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("End")]
        End = 3
    }
}
