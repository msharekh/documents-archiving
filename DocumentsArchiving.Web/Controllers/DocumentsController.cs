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
            var documents = DocumentBLL.GetDocuments();
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            int pageIndex = 1;

            /*filtering*/
            if (!string.IsNullOrEmpty(Subject))
            {
                documents = documents
                    .Where(x => x.Subject.Contains(Subject)).ToList();
            }
            if (!string.IsNullOrEmpty(SerialNumber))
            {
                documents = documents
                    .Where(x => x.SerialNumber.Contains(SerialNumber)).ToList();
            }
            if (!string.IsNullOrEmpty(DocumentTypeId))
            {
                documents = documents
                    .Where(x => x.DocumentTypeId.Equals(int.Parse(DocumentTypeId))).ToList();
            }

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
            //documentTypes.Add(new DocumentTypeVM() { DocumentTypeId = 0, DocumentTypeDesc = "--select type--" });
            ViewBag.Subject = Subject;
            ViewBag.SerialNumber = SerialNumber;
            ViewBag.DocumentTypeId = new SelectList(documentTypes, "DocumentTypeId", "DocumentTypeDesc", 0);
            ViewBag.CurrentSort = sortOrder;

            //paging;
            return View(documents.ToList().ToPagedList(page ?? pageIndex, pageSize));
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