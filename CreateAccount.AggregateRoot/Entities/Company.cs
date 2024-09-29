﻿using CreateAccount.AggregateRoot.Entities;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.AggregateRoot.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

