using Tennis.Game.Enums;

namespace Tennis.Game.Models
{
    public record Player
    {
        public string Name { get; set; }
        public Score Score { get; set; }
        public Player(string playerName)
        {
            Name = playerName;
        }
    }
}
