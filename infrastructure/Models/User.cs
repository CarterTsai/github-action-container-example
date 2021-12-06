using System;
using System.Collections.Generic;

namespace infrastructure.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}
