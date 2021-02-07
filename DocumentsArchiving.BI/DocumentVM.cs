using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentsArchiving.BI
{
    public class DocumentVM
    {
        public int DocumentId { get; set; }

        public string Subject { get; set; }

        public int DocumentTypeId { get; set; }

        [Display(Name = "Document-Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DocumentDate { get; set; }

        [Display(Name = "Serial-Number ")]
            public string SerialNumber { get; set; }
 
        public string Path { get; set; }

        public DocumentTypeVM DocumentType { get; set; }
    }
}
