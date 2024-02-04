using Tennis.Game.Enums;
using Tennis.Game.Extensions;
using Tennis.Game.Models;

namespace Tennis.Game
{
    public class TennisGame : ITennisGame
    {
        private readonly IReadOnlyDictionary<string, Player> _players;

        public TennisGame(string player1Name, string player2Name)
        {
            _players = new Dictionary<string, Player>()
            {
                { player1Name, new Player(player1Name) },
                { player2Name, new Player(player2Name) }
            };
        }

        private bool GameOver => _players.Any(x => x.Value.Score >= Score.Won);
        private Player WinningPlayer => _players.FirstOrDefault(x => x.Value.Score >= Score.Won).Value;
        private bool AnyPlayerAtAdvantage => _players.Any(x => x.Value.Score == Score.Advantage);
        private Player PlayerAtAdvantage => _players.FirstOrDefault(x => x.Value.Score == Score.Advantage).Value;
        private bool ScoresAreEqual => _players.GroupBy(x => x.Value.Score).Count() == 1;
        private Player OpposingPlayer(Player player) => _players.FirstOrDefault(x => x.Value.Name != player.Name).Value;

        public void WonPoint(string playerName)
        {
            if (!GameOver)
            {
                _players.TryGetValue(playerName, out Player? playerPointWinner);
                if (playerPointWinner != null)
                {
                    var opposingPlayer = OpposingPlayer(playerPointWinner);
                    if (opposingPlayer.Score == Score.Advantage)
                    {
                        opposingPlayer.Score--;
                    }
                    else
                    {
                        playerPointWinner.Score++;
                        if (playerPointWinner.Score == Score.Advantage && opposingPlayer.Score < Score.Forty)
                        {
                            playerPointWinner.Score++;
                        }
                    }
                }
                else
                {
                    throw new Exception("Could not find player");
                }
            }
        }

        public string GetScore()
        {
            string score = "";
            if (ScoresAreEqual)
            {
                score = _players.Any(x => x.Value.Score == Score.Forty)
                    ? Constants.Deuce
                    : $"{_players.FirstOrDefault().Value.Score.GetDescription()}{Constants.Separator}{Constants.All}";
            }
            else if (AnyPlayerAtAdvantage)
            {
                score = $"{PlayerAtAdvantage.Score.GetDescription()} {PlayerAtAdvantage.Name}";
            }
            else if (GameOver)
            {
                score = $"{Constants.WinFor} {WinningPlayer.Name}";
            }
            else
            {
                foreach (var player in _players)
                {
                    score += $"{(score.Length > 0 ? Constants.Separator : null)}{player.Value.Score.GetDescription()}";
                }
            }
            return score;
        }
    }
}
