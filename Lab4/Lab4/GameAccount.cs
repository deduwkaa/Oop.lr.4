﻿using Lab4.DB.Service;

namespace Lab4
{
    internal class GameAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int CurrentRating { get; set; }
        public int GamesCount { get; set; }
        public List<GameResult> GameResults { get; set; } = new List<GameResult>();
        public GameAccountService Service { get; set; }

        public GameAccount(GameAccountService service, int ID, string userName, int gamesCount = 0)
        {
            Service = service;
            UserName = userName;
            CurrentRating = 100;
            GamesCount = gamesCount;
            Id = ID;
        }
        public void WinGame(Game game, string player, int gameIndex)
        {
            int rating = RatingCalc(game.getPlayRating(this));
            CurrentRating += rating;
            GamesCount++;
            GameResults.Add(new GameResult(player, "Виграв", rating, gameIndex, CurrentRating));
            Service.Update(this);
        }
        public void LoseGame(Game game, string player, int gameIndex)
        {
            int rating = RatingCalc(game.getPlayRating(this));
            if (CurrentRating > 1)
            {
                CurrentRating -= rating;
                if (CurrentRating < 1)
                {
                    CurrentRating = 1;
                }
            }
            GamesCount++;
            GameResults.Add(new GameResult(player, "Програв", rating, gameIndex, CurrentRating));
            Service.Update(this);
        }
        public void DrawGame(Game game, string player, int gameIndex, int currentRating)
        {
            GamesCount++;
            int rating = RatingCalc(game.getPlayRating(this));
            GameResults.Add(new GameResult(player, "Нічия", rating, gameIndex, currentRating));
            Service.Update(this);
        }

        public void GetStats()
        {
            if (GameResults == null)
            {
                Console.WriteLine($"Ім'я:{UserName}, Id: {Id}");
                return;
            }

            Console.WriteLine($"Ім'я:{UserName}, Id: {Id}");
            for (int i = 0; i < GameResults.Count; i++)
            {
                var result = Service.GameResults(this)[i];
                Console.WriteLine($"Проти {result.Opponent}, {result.Winner}, Рейтинг: {result.CurrentRating}, Номер гри #{result.GameIndex}");
            }
            Console.WriteLine($"Поточний рейтинг для гравця {UserName}: {CurrentRating}\n" + $"Ігор зіграно: {GamesCount}\n");
        }
        public virtual int RatingCalc(int rating)
        {
            return rating;
        }
    }
}
