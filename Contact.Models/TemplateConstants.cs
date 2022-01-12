using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Models
{
    public class TemplateConstants
    {
        private const string PLH_RAW_LINK = "$RAW_LINK";
        private const string PLH_COMPANY_EMAIL = "$COMPANY_MAIL";
        private const string PLH_NAME = "$NAME";
        private const string PLH_FULLNAME = "$FULLNAME";

        private const string COMPANY_EMAIL = "fcedaivi@gmail.com";

        public string Raw_Link { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}
