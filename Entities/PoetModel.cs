using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PoetModel
    {
        public string? UId { get; set; }
        public string? NickName { get; set; }
        public string? PoetryStyle { get; set; }
        public string? Biography { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? BecamePoetAt { get; set; }
        public string? Email { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}