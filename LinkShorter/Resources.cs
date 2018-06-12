using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkShorter
{
    public  class Resources
    {
        private static readonly Resources _resources = new Resources();

        public string Link { get; set; }

        public static Resources GetResources()
        {
            return _resources;
        }
    }
}
