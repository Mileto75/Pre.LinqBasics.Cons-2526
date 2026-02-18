using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre.LinqBasics.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public DateTime Published_date { get; set; }
        public string Type { get; set; }
        public string Platforms { get; set; }
        public string[] PlatformsList { get; set; }
        public int Users { get; set; }
        public string Status { get; set; }
    }
}
