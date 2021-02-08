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
    public class DocumentBLL
    {

        public static string UploadFile(HttpPostedFileBase file, string path)
        {

            string _path = "";


            if (file.ContentLength > 0)
            {
                string _fileName = Path.GetFileName(file.FileName);

                _path = Path.Combine(path, _fileName);

                file.SaveAs(_path);
            }
            else
            {
                throw new Exception("Not valid file upload!!");
            }

            return _path;
        }

        public static List<DocumentVM> GetDocuments()
        {
            return DocumentDAL.GetDocuments();
        }
        public static List<DocumentVM> GetDocumentsByFilter(IDictionary<string, string> filters)
        {
            return DocumentDAL.GetDocumentsByFilter( filters);
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
