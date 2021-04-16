
# **Build a web application for documents archiving with below details**

 

##  **pages**

 

### *page 1* (add a new document):

- Add content file

- Add the following meta data:

    - Subject (Mandatory)  --ok

    - Document type (list of some types like: Incoming, Outgoing …. Etc.) (Mandatory) --ok

    - Document date (Mandatory and should be less than or equal the current date) --ok

    - Serial Number (Mandatory with this format 00/0000 Like: 25/2014) --ok

    - Details (Not mandatory) --ok

 

###  *page 2* (search and dopwnload saved documents):

- Use the mandatory fields as a search criteria 

    - Subject  --ok

    - Document type (1) --ok

    - Serial Number  --ok

- Display the search result with this field:

    - Serial 

    - Subject 

    - Document date

- A facility to sort the search result based on any (2) --ok
    - Subject.	
    - Serial 

- A facility to download the document.  (3)

 ======================================

## Solutions: documents archiving

## **Page Content**

 

### Page1 (create)

 

|fld|typ|q|n| 
|-|-|-|-|
|Subject|txt|req||
|DocumentType|ddl|req||
|DocumentDate|dateTxt|req||
|SerialNumber|txt|req||
|Details|textArea|||
|File|fileUpload|||
|Submit|btn|||

 

### Page2 (index)

#### serach:

|fld|typ|q|n| 
|-|-|-|-|
|Subject|txt|req||
|DocumentType|ddl|req||
|SerialNumber|txt|req||
|Search|btn|||

 

#### grid:

|fld|typ|q|n| 
|-|-|-|-|
|Subject|fld|req||
|DocumentDate|fld|req||
|SerialNumber|fld|req||
|Details|fld|||

 

## **technologies**

- MVC.NET 

- C# 

- html, css, Jquery

## Projects:
- DocumentsArchiving.BI
- DocumentsArchiving.BLL
- DocumentsArchiving.DAL
- DocumentsArchiving.Web
- DocumentsArchiving.Integ
- DocumentsArchiving.Console



## NuGet Libraries
Install-Package PagedList.Mvc -Version 4.5.0 

### classes:

- Document

 

|fld|typ|q|n| 
|-|-|-|-|
|DocumentId|int|req|key|
|Subject|int|req||
|DocumentType|string|req||
|DocumentDate|datetime|req||
|SerialNumber|string|req||
|Details|string|||
 

 


 
 



### BI
- DocumentTypeVM

 

- DocumentVM

|fld|typ|q|n| 
|-|-|-|-|
|DocumentId|int|req|key|
|Subject|int|req||
|DocumentType|string|req||
|DocumentDate|datetime|req||
|SerialNumber|string|req||
|Details|string|||
|File|HttpPostedFileBase|||
|Path|string|||
|DocumentType|DocumentTypeVM|||


 - DocumentAddVM

|fld|typ|q|n| 
|-|-|-|-|
|Subject|int|req||
|DocumentType|string|req||
|DocumentDate|datetime|req|should be less than or equal the current date|
|SerialNumber|string|req|format 00/0000 Like: 25/2014|
|Details|string|||
|File|HttpPostedFileBase|||
|Path|string|||
|DocumentType|DocumentTypeVM|||
### BLL

- file funcs

    - UploadFile(doc)

    - DownloadFile(docId)


- db funcs

    - InsertDocument(doc)

    - GetDocumentsByFilter(filters)

    - GetDocumentTypes()


 

### DAL

- db funcs

    - InsertFile(doc)

    - InsertTrans(doc)

    - GetDocumentTypes()

    - GetDocumentsByFilter(filters)

 ### INTEG

- funcs

    - ListOfCountryNamesByCode(doc)
 http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso?op=ListOfCountryNamesByCode   

### Web functions:

- GET Index
- GET Create
- POST Create(document) 
- GET DownloadFile(path)



## **database**


- MSSQL Server -  DocDB 

#### tables: 

- Document

    - DocumentId (int) (rqg) (key)

    - Subject varchar(30) (req)

    - DocumentType varchar(30) (req)

    - DocumentDate date (req) --should be less than or equal the current date

    - SerialNumber varchar(30) --format 00/0000 Like: 25/2014 (req)

    - Details(max) 

 

- Transaction (suggested)

    - DocumentId (int) (rqg) (key)        

    - TransactionDate date 

    - log varchar(30)

    

 

 

 
 
