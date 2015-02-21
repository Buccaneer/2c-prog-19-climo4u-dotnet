using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klimatogrammen.Infrastructure;

namespace Klimatogrammen.Tests.Controllers {
    class SessionRepositoryMock : ISessionRepository {
        private IDictionary<string, object> _data = new Dictionary<string, object>(); 
        public object this[string sleutel] {
            get {
                if (!_data.ContainsKey(sleutel)) {
                    return null;
                }
                return _data[sleutel];
            }
            set {
                if (!_data.ContainsKey(sleutel)) {
                    _data.Add(sleutel,value);
                    return;
                }
                _data[sleutel] = value;
            }
        }

        public bool BestaatSleutel(string sleutel) {
            return _data.ContainsKey(sleutel);
        }

        public void VerwijderSleutel(string sleutel) {
             _data.Remove(sleutel);
        }
    }
}
