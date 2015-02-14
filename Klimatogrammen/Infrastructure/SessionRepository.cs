using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Klimatogrammen.Infrastructure {
    public class SessionRepository : ISessionRepository {
        private HttpContextBase _httpContext;

        public object this[string sleutel] {
            get { return _httpContext.Session[sleutel]; }
            set { _httpContext.Session[sleutel] = value; }
        }

        public bool BestaatSleutel(string sleutel) {
            return _httpContext.Session[sleutel] != null;
        }


        public void VerwijderSleutel(string sleutel) {
            _httpContext.Session.Remove(sleutel);
        }

        public SessionRepository() {
            _httpContext = new HttpContextWrapper(HttpContext.Current);
        }
    }
}