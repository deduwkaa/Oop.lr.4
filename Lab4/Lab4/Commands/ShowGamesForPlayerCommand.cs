using Lab4.DB.Service;

namespace Lab4.Commands
{
    internal class ShowGamesForPlayerCommand : ICommand
    {
        private readonly GameAccountService _gameAccountService;
        public ShowGamesForPlayerCommand(GameAccountService gameAccountService)
        {
            _gameAccountService = gameAccountService;
        }

        public void Execute()
        {
            Console.WriteLine("Введіть ID гравців, гри котрих ви хочете побачити:");
            int playerID = int.Parse(Console.ReadLine());
            GameAccount player = _gameAccountService.ReadById(playerID);
            Console.WriteLine($"Інформація про ігри, гравця {player.UserName}:");
            List<GameResult> games = _gameAccountService.GameResults(player);
            foreach (GameResult game in games)
            {
                Console.WriteLine($"Проти {game.Opponent}, {game.Winner}, Рейтинг: {game.CurrentRating}, Номер гри #{game.GameIndex}");
            }
        }
    }
}
