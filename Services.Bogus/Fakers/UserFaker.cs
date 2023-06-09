﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakers
{
    public class UserFaker : EntityFaker<User>
    {
        public UserFaker(string language) : base(language) {
            RuleFor(x => x.Username, x => x.Internet.UserName());
            RuleFor(x => x.Password, x => x.Internet.Password());
            RuleFor(x => x.Age, x => DateTime.Now.Year - x.Person.DateOfBirth.Year);

        }
    }
}
