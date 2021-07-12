using System.Collections.Generic;

namespace Domain
{
    public class ElementalAttribute
    {
        public string AttributeName { get; set; }

        public List<string> WeakAgainst { get; set; }

        public List<string> StrongAgainst { get; set; }
        
    }
}