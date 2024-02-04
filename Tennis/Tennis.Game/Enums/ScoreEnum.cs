using System.ComponentModel;
using Tennis.Game.Models;

namespace Tennis.Game.Enums
{
    public enum Score
    {
        [Description(Constants.Love)]
        Love = 0,
        [Description(Constants.Fifteen)]
        Fifteen = 1,
        [Description(Constants.Thirty)]
        Thirty = 2,
        [Description(Constants.Forty)]
        Forty = 3,
        [Description(Constants.Advantage)]
        Advantage = 4,
        [Description(Constants.Won)]
        Won = 5
    }
}
