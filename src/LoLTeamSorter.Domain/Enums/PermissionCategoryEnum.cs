using System.ComponentModel;

namespace LoLTeamSorter.Domain.Enums
{
    public enum PermissionCategoryEnum
    {
        [Description("Player")]
        PLAYER,
        [Description("Matchmaking")]
        MATCHMAKING,
        [Description("User")]
        USER,
        [Description("Group")]
        GROUP,
        [Description("Hangfire")]
        HANGFIRE
    }
}
