using DocumentsArchiving.BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsArchiving.DAL
{
    public class DocumentDAL

    {

        //private DocDBEntities db = new DocDBEntities();



        public static void InsertDocument(DocumentAdd document)

        {

            using (var repo = new GenericRepository<Document>())

            {

                var doc = new Document()

                {

                    DocumentTypeId = document.DocumentTypeId,

                    Details = document.Details,

                    DocumentDate = document.DocumentDate,

                    SerialNumber = document.SerialNumber,

                    Subject = document.Subject,

                    Path = document.Path

                };



                //execute

                repo.Insert(doc);

                repo.SaveChanges();

            }



        }


    }
}
