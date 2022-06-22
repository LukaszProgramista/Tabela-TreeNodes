using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using System.Linq;
using Tabela_TreeNodes.UI;

[assembly: Worker(typeof(TreeNodesPozytronExtender))]
namespace Tabela_TreeNodes.UI
{
    public class TreeNodesPozytronExtender
    {
        [Context]
        public Session Session { get; set; }

        [Context]
        public Kontrahent Contractor { get; set; }

        public View Localizations() =>
           Contractor.Lokalizacje.CreateView(); 

        public View Devices()
            =>
            Contractor.Urzadzenia.CreateView();                    

        public View Contracts()
        {
            var hm = Session.GetHandel();
            var contracts = hm.DokHandlowe.WgDaty
                .Where(d =>
                    (d.Definicja == hm.DefDokHandlowych.UmowaCykliczna ||
                    d.Definicja == hm.DefDokHandlowych.AneksCyklicznej) &&
                    d.Kontrahent == Contractor
                ).ToList();
            return new View(hm.DokHandlowe.PrimaryKey, contracts);
        }
    }
}
