using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Core.Models
{
    public class ImageModel
    {
        public int ImageId { get; set; }
        public int PropertyId { get; set; }

        public string UrlPath { get; set; }
    }
}
