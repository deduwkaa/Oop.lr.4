﻿using Lab4.DB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Account
{
    class BoostedAccount : GameAccount
    {
        public BoostedAccount(GameAccountService service, int Id, string userName, int gamesCount = 0) : base(service, Id, userName, gamesCount) { }
        public override int RatingCalc(int rating)
        {
            return rating * 10;
        }
    }
}
