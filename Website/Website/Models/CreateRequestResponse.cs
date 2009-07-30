using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

using System.Web.Mvc;
using DomainModel;

namespace Website.Models
{
    public class CreateRequestResponse : IDataErrorInfo
    {
        public string PackageDescription { get; set; }

        public string PackageDimensions { get; set; }

        public string PackageWeight { get; set; }

        public string OriginPlace { get; set; }

        public string DestinationPlace { get; set; }

        public string DestinationDate { get; set; }

        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
        }

        public string this[string propName]
        {
            get
            {
                DateTime datetime;
                if ((propName == "PackageDescription") && string.IsNullOrEmpty(PackageDescription))
                    return "Please enter a description for your package.";
                if ((propName == "PackageDimensions") && string.IsNullOrEmpty(PackageDimensions))
                    return "Please enter the dimensions for your package.";
                if ((propName == "PackageWeight") && string.IsNullOrEmpty(PackageWeight))
                    return "Please enter the weight of your package.";
                if ((propName == "OriginPlace") && string.IsNullOrEmpty(OriginPlace))
                    return "Please enter the origin station.";
                if ((propName == "DestinationDate") && 
                    ((!DateTime.TryParse(DestinationDate, out datetime)) || (datetime < DateTime.Today.Date))
                   )
                    return "Please enter a valid Date.";
                if ((propName == "DestinationPlace") && string.IsNullOrEmpty(DestinationPlace))
                    return "Please enter the destination station.";

                return null;
            }
        }

        #endregion
    }
}
