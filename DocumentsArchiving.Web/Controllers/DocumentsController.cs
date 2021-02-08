using DocumentsArchiving.BI;
using DocumentsArchiving.BLL;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace DocumentsArchiving.Web.Controllers
{
    public class DocumentsController : Controller
    {
        // GET: Documents
        public ActionResult Index(string sortOrder, string CurrentSort, string Subject, string SerialNumber, string DocumentTypeId, int? page)
        {
            List<DocumentVM> documents = null;
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            int skipCount = 1;
            bool enablePaging = true;

            /*filtering */
            IDictionary<string, string> filters = new Dictionary<string, string>();

            filters.Add("Subject", Subject);
            filters.Add("SerialNumber", SerialNumber);
            filters.Add("DocumentTypeId", DocumentTypeId);
                
            documents = DocumentBLL.GetDocumentsByFilter(filters);

           

            /*sorting*/
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Subject" : sortOrder;

            switch (sortOrder)
            {
                case "Subject":
                    if (sortOrder.Equals(CurrentSort))
                        documents = documents.OrderByDescending(x => x.Subject).ToList();
                    else
                        documents = documents.OrderBy(x => x.Subject).ToList();

                    break;
                case "SerialNumber":
                    if (sortOrder.Equals(CurrentSort))
                        documents = documents.OrderByDescending(x => x.SerialNumber).ToList();
                    else
                        documents = documents.OrderBy(x => x.SerialNumber).ToList();
                    break;
                default:
                    break;
            }

            List<DocumentTypeVM> documentTypes = DocumentBLL.GetDocumentTypes();
            ViewBag.Subject = Subject;
            ViewBag.SerialNumber = SerialNumber;
            ViewBag.DocumentTypeId = new SelectList(documentTypes, "DocumentTypeId", "DocumentTypeDesc", 0);
            ViewBag.CurrentSort = sortOrder;

            //paging;            
            IPagedList<DocumentVM> pagedDocuments = documents.ToPagedList(page ?? skipCount, pageSize);

            return View(pagedDocuments);
        }
        // GET: Documents/Create
        public ActionResult Create()
        {
            List<DocumentTypeVM> documentTypes = DocumentBLL.GetDocumentTypes();
            ViewBag.DocumentTypeId = new SelectList(documentTypes, "DocumentTypeId", "DocumentTypeDesc");
            return View();
        }


        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DocumentAddVM document)
        {
            List<DocumentTypeVM> documentTypes = DocumentBLL.GetDocumentTypes();
            ViewBag.DocumentTypeId = new SelectList(documentTypes, "DocumentTypeId", "DocumentTypeDesc", document.DocumentTypeId);
            if (ModelState.IsValid)
            {
                try
                {
                    document.Path = DocumentBLL.UploadFile(document.File, Server.MapPath("~/UploadedFiles"));
                    DocumentBLL.InsertDocument(document);
                    ViewBag.Message = "File upload SUCCESS ";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "File upload faild ";
                    return View(document);
                }
                //return RedirectToAction("Index");
            }
            return View(document);
        }


        public FileResult DownloadFile(string path)
        {           
            
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            string fileName = path.Substring(path.LastIndexOf('\\') + 1);
            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}