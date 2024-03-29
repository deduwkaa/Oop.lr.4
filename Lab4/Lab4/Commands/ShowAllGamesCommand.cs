﻿using Lab4.DB.Service;

namespace Lab4.Commands
{
    internal class ShowAllGamesCommand : ICommand
    {
        private readonly GameAccountService _gameAccountService;
        public ShowAllGamesCommand(GameAccountService gameAccountService)
        {
            _gameAccountService = gameAccountService;
        }
        public void Execute()
        {
            Console.WriteLine("Інформація про ігри:");
            foreach (GameAccount thisPlayer in _gameAccountService.ReadAll())
            {
                List<GameResult> games = _gameAccountService.GameResults(thisPlayer);
                foreach (GameResult game in games)
                {
                    Console.WriteLine($"Проти {game.Opponent}, {game.Winner}, Рейтинг: {game.CurrentRating}, Номер гри #{game.GameIndex}");
                }
            }
        }
    }
}
