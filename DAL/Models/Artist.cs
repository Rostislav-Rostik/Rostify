using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Followers { get; set; }
        public string Popularity { get; set; }
        public string Name_Genre { get; set; }
    }
}
