using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentsArchiving.BI
{
    public class DocumentAdd
    {
        [Required]

        public string Subject { get; set; }

        [Required]

        public int DocumentTypeId { get; set; }

        public System.DateTime DocumentDate { get; set; }

        [Required]

        public string SerialNumber { get; set; }

        public string Details { get; set; }


        public HttpPostedFileBase File { get; set; }

        public string Path { get; set; }

    }
}
