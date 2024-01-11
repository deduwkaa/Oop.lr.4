using Lab4.DB.Service;

namespace Lab4.Commands
{
    internal class StartGameCommand : ICommand
    {
        private readonly GameAccountService _gameAccountService;
        private readonly GameService _gameService;

        public StartGameCommand(GameAccountService gameAccountService, GameService gameService)
        {
            _gameAccountService = gameAccountService;
            _gameService = gameService;
        }
        public void Execute()
        {
            GameAccount player1;
            GameAccount player2;

            Console.WriteLine("Введіть ID гравців щоб почати гру:");
            int player1ID = int.Parse(Console.ReadLine());
            int player2ID = int.Parse(Console.ReadLine());

            player1 = _gameAccountService.ReadById(player1ID);
            player2 = _gameAccountService.ReadById(player2ID);

            string answer;
            do
            {
                Game game = GameType(player1, player2, _gameService);
                game.PlayGame(player1, player2);
                Console.WriteLine("Зіграти ще раз? (Y/N)");
                answer = Console.ReadLine();
            } while (answer == "Y" || answer == "y");
        }
        private Game GameType(GameAccount player1, GameAccount player2, GameService service)
        {
            Console.WriteLine("Виберіть тип гри: \n1.StandartGame \n2.HalfGame \n3.DoubleRatingGame");
            int choose = int.Parse(Console.ReadLine());
            GameFactory factory = new GameFactory();
            switch (choose)
            {
                case 1:
                    return factory.CreateStandartGame(player1, player2, service);
                case 2:
                    return factory.CreateHalfGame(player1, player2, service);
                case 3:
                    return factory.CreateDoubleRatingGame(player1, player2, service);
                default:
                    Console.WriteLine("Невірно. Введіть число між 1-3");
                    return GameType(player1, player2, service);
            }
        }
    }
}
