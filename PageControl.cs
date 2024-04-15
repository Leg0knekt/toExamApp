using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toExamApp
{
    public class PageControl
    {
        private static PageAuth pAuth;
        private static PageCatalog pCatalog;
        public static PageAuth PAuth
        {
            get
            {
                if (pAuth == null) { pAuth = new PageAuth(); }
                return pAuth;
            }
        }
        public static PageCatalog PCatalog
        {
            get
            {
                if (pCatalog == null) { pCatalog = new PageCatalog(); }
                return pCatalog;
            }
        }
    }
}
