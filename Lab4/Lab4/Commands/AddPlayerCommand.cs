using Lab4.DB.Service;
using Lab4.Account;

namespace Lab4.Commands
{
    internal class AddPlayerCommand : ICommand
    {
        private readonly GameAccountService _gameAccountService;
        public AddPlayerCommand(GameAccountService gameAccountService)
        {
            _gameAccountService = gameAccountService;
        }
        public void Execute()
        {
            Console.WriteLine("Ім'я нового гравця: ");
            string name = Console.ReadLine();
            GameAccount player = AccountChose(_gameAccountService, name);
            _gameAccountService.Create(player);
        }

        private GameAccount AccountChose(GameAccountService service, string userName)
        {
            Console.WriteLine("Виберіть тип аккаунту: \n1.StandartAccount \n2.UnrankedAccount \n3.BoostedAccount");
            int choose = int.Parse(Console.ReadLine());
            var Id = service.ReadAll().Count();
            switch (choose)
            {
                case 1:
                    var standartGameAccount = new StandartAccount(service, Id, userName);
                    return standartGameAccount;
                case 2:
                    var unrankedAccount = new UnrankedAccount(service, Id, userName);
                    return unrankedAccount;
                case 3:
                    var BoostedAccount = new BoostedAccount(service, Id, userName);
                    return BoostedAccount;
                default:
                    Console.WriteLine("Невірно. Введіть число між 1-3");
                    return AccountChose(service, userName);
            }
        }
    }
}
