using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen
{
    public enum Operator
    {
        KleinerDan,
        GelijkAan,
        KleinerDanOfGelijkAan,
        GroterDan,
        GroterDanOfGelijkAan,
        NietGelijkAan,
    }
}
