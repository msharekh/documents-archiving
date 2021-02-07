using DocumentsArchiving.BI;
using DocumentsArchiving.BLL;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace DocumentsArchiving.Web.Controllers
{
    public class DocumentsController : Controller
    {
        // GET: Documents
        public ActionResult Index(string searchBy,string search,int? page)
        {
            var documents = DocumentBLL.GetDocuments();
            switch (searchBy)
            {
                case "Subject":
                    return View(documents.Where(x => x.Subject.Contains(search)).ToList().ToPagedList(page??1,3));
                
                case "SerialNumber":
                    return View(documents.Where(x => x.SerialNumber.Contains(search)).ToList().ToPagedList(page??1,3));
                
                default:
                    break;
            }

            List<DocumentTypeVM> documentTypes = DocumentBLL.GetDocumentTypes();
            ViewBag.DocumentTypeId = new SelectList(documentTypes, "DocumentTypeId", "DocumentTypeDesc");

            return View(documents.ToList().ToPagedList(page ?? 1, 3));
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
        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    }
}