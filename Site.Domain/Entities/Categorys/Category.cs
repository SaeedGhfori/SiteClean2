﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.Entities.Categorys
{
    [Auditable]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
