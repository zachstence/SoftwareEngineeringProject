using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chicken.WebTests
{
    public class MockHttpSession : HttpSessionStateBase
    {
        Dictionary<string, object> m_SessionStorage = new Dictionary<string, object>();

        public override object this[string name]
        {
            get { return m_SessionStorage[name]; }
            set { m_SessionStorage[name] = value; }
        }
    }


}

