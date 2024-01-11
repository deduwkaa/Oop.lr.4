﻿using Lab4.DB.Service;

namespace Lab4
{
    abstract class Game
    {
        public GameAccount player1 { get; set; }
        public GameAccount player2 { get; set; }
        public int playRating { get; set; }
        public string winner { get; set; }
        public GameService _service { get; set; }
        public Game(GameAccount player1, GameAccount player2, GameService service)
        {
            this.player1 = player1;
            this.player2 = player2;
            _service = service;
        }
        public void PlayGame(GameAccount Gamer1, GameAccount Gamer2)
        {
            Console.WriteLine("Введіть рейтинг на який грають:");
            playRating = int.Parse(Console.ReadLine());
            while (playRating <= 0 || Gamer1.CurrentRating < playRating || Gamer2.CurrentRating < playRating)
            {
                Console.WriteLine("Рейтинг не може бути меньше 0, або рейтинг одного із гравців менше ніж заданий рейтинг, введіть інший рейтинг");
                playRating = int.Parse(Console.ReadLine());
            }

            Random random = new Random();
            int gameIndex = player1.GamesCount;
            Console.WriteLine("Введіть число між 1 і 10: ");
            int a = Convert.ToInt32(Console.ReadLine());
            int b = random.Next(1, 11);
            if (a > b)
            {
                Console.WriteLine("Гравець 1 виграв");
                player1.WinGame(this, Gamer2.UserName, gameIndex);
                Console.WriteLine("Гравець 2 програв");
                player2.LoseGame(this, Gamer1.UserName, gameIndex);
                _service.Create(this);
            }
            if (a < b)
            {
                Console.WriteLine("Гравець 2 виграв");
                player2.WinGame(this, Gamer1.UserName, gameIndex);
                Console.WriteLine("Гравець 1 програв");
                player1.LoseGame(this, Gamer2.UserName, gameIndex);
                _service.Create(this);
            }
            if (a == b)
            {
                Console.WriteLine("Нічия");
                player1.DrawGame(this, Gamer2.UserName, gameIndex, Gamer1.CurrentRating);
                player2.DrawGame(this, Gamer1.UserName, gameIndex, Gamer2.CurrentRating);
                _service.Create(this);
            }
        }
        public virtual int getPlayRating(GameAccount player) { return playRating; }
    }
}
