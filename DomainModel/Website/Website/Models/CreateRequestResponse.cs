using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class CreateRequestResponse
    {
        public string PackageDescription { get; set; }

        public string PackageDimensions { get; set; }

        public string PackageWeight { get; set; }

        public string OriginPlace { get; set; }

        public string OriginDate { get; set; }

        public string DestinationPlace { get; set; }

        public string DestinationDate { get; set; }
    }
}
