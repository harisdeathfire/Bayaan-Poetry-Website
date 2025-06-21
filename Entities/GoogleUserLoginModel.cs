using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GoogleUserLoginModel
    {
        public int userId { get; set; }
        public string? userName { get; set; }
        public string? userEmail { get; set; }
        public string? userRole { get; set; }
        public string? Profile_Id { get; set; }
    }
}
