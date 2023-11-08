using System.ComponentModel;

namespace WebAPI.Business.Enums
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
