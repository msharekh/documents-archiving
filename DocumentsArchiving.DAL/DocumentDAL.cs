using DocumentsArchiving.BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsArchiving.DAL
{
    public class DocumentDAL
    {

        //private DocDBEntities db = new DocDBEntities();
        public static void InsertDocument(DocumentAddVM document)
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

        public static List<DocumentVM> GetDocuments()
        {
            using (var repo = new GenericRepository<Document>())
            {
                var list = repo.GetAll().ToList();
                List<DocumentVM> documentVMs = new List<DocumentVM>();
                foreach (var item in list)
                {
                    documentVMs.Add(
                        new DocumentVM()
                        {
                            Subject = item.Subject,
                            SerialNumber = item.SerialNumber,
                            DocumentDate = item.DocumentDate,
                            //Details = item.Details,
                            DocumentTypeId = item.DocumentTypeId,
                            Path = item.Path,
                            DocumentType = new DocumentTypeVM() { DocumentTypeId = item.DocumentType.DocumentTypeId, DocumentTypeDesc = item.DocumentType.DocumentTypeDesc }

                        }
                     );
                }
                return documentVMs;
                ;
            }
        }

        public static List<DocumentTypeVM> GetDocumentTypes()
        {
            using (var repo = new GenericRepository<DocumentType>())
            {
                var list = repo.GetAll().ToList();
                List<DocumentTypeVM> documentTypeVMs = new List<DocumentTypeVM>();
                foreach (var item in list)
                {
                    documentTypeVMs.Add(
                        new DocumentTypeVM()
                        {
                            DocumentTypeId = item.DocumentTypeId,
                            DocumentTypeDesc = item.DocumentTypeDesc
                        }
                     );
                }
                return documentTypeVMs;

            }
        }


        public static List<DocumentVM> GetDocumentsByFilter(IDictionary<string, string> _filters)
        {

            using (var repo = new GenericRepository<Document>())
            {

                int? RowsCount = 0;
                int? pagesCount = 0;

                List<Expression<Func<Document, bool>>> filters = new List<Expression<Func<Document, bool>>>();
                foreach (var f in _filters)
                {
                    if (!string.IsNullOrEmpty(f.Value))
                    {
                        switch (f.Key)
                        {
                            case "Subject":
                                filters.Add(x => x.Subject.Contains(f.Value));
                                break;
                            case "SerialNumber":
                                filters.Add(x => x.SerialNumber.Contains(f.Value));
                                break;
                            case "DocumentTypeId":
                                filters.Add(x => x.DocumentTypeId.ToString().Contains(f.Value));
                                break;
                            default:
                                break;
                        }

                    }
                    else
                    {
                        filters.Add(x => true);
                    }
                }


                var documents = repo.GetAll(filters).Select(x => new BI.DocumentVM
                {
                    Subject = x.Subject,
                    SerialNumber = x.SerialNumber,
                    DocumentDate = x.DocumentDate,
                    //Details = item.Details,
                    DocumentTypeId = x.DocumentTypeId,
                    Path = x.Path,
                    DocumentType = new DocumentTypeVM() { DocumentTypeId = x.DocumentType.DocumentTypeId, DocumentTypeDesc = x.DocumentType.DocumentTypeDesc }
                }).ToList().OrderByDescending(x => x.DocumentDate).ToList();



                return documents;

            }


        }
    }
}
