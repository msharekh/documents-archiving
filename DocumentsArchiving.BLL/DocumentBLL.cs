using DocumentsArchiving.BI;
using DocumentsArchiving.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentsArchiving.BLL
{
    public class DocumentBLL{

        public static string UploadFile(HttpPostedFileBase file, string path){

            string _fileName = Path.GetFileName(file.FileName);

            string _path = Path.Combine(path, _fileName);


            if (file.ContentLength > 0){

                file.SaveAs(_path);
            }
            else
            {
                throw new Exception("file has 0 size");
            }

            return _path;
        }

        public static List<DocumentVM> GetDocuments()
        {
            return DocumentDAL.GetDocuments();
        }

        public static void InsertDocument(DocumentAddVM document)
        {
            DocumentDAL.InsertDocument(document);
        }

        public static List<DocumentTypeVM> GetDocumentTypes()
        {
            return DocumentDAL.GetDocumentTypes();
        }
    }
}
