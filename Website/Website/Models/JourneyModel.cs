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
    public class JourneyModel : IDataErrorInfo
    {
        public string OriginPlace { get; set; }

        public string OriginDate { get; set; }

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
                DateTime originDate;
                DateTime destinationDate;
                if ((propName == "OriginPlace") && string.IsNullOrEmpty(OriginPlace))
                    return "Please enter the origin station.";
                if ((propName == "OriginDate") && 
                    ((!DateTime.TryParse(OriginDate, out originDate)) || (originDate < DateTime.Today.Date))
                   )
                    return "Please enter a valid departure date.";
                if ((propName == "DestinationPlace") && string.IsNullOrEmpty(DestinationPlace))
                    return "Please enter the destination station.";
                if (propName == "DestinationDate")
                {
                    if(!DateTime.TryParse(DestinationDate, out destinationDate))
                        return "Please enter a valid arrival date.";
                    if(!(DateTime.TryParse(OriginDate, out originDate) && destinationDate >= originDate))
                        return "Arrival date cannot be less than departure date.";
                }

                return null;
            }
        }

        #endregion
    }
}
