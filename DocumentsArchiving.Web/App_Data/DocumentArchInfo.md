
# **Build a web application for documents archiving with below details**

 

##  **pages**

 

### *page 1* (add a new document):

- Add content file

- Add the following meta data:

    - Subject (Mandatory)  --ok

    - Document type (list of some types like: Incoming, Outgoing …. Etc.) (Mandatory) --ok

    - Document date (Mandatory and should be less than or equal the current date) --ok

    - Serial Number (Mandatory with this format 00/0000 Like: 25/2014)

    - Details (Not mandatory)

 

###  *page 2* (search saved documents):

- Use the mandatory fields as a search criteria 

    - Subject

    - Document type

    - Serial Number

- Display the search result with this field:

    - Serial 

    - Subject 

    - Document date

- A facility to sort the search result based on any Serial or Subject.

- A facility to download the document.

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

 

 

 

|fld|typ|q|n| 

|-|-|-|-|

|Subject|int|req||

|DocumentType|string|req||

|DocumentDate|datetime|req|should be less than or equal the current date|

|SerialNumber|string|req|format 00/0000 Like: 25/2014|

|Details|string|||

 
 



### BI
- DocumentTypeVM

- DocumentAddVM

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


### BLL

- file funcs

    - UploadFile(doc)

    - DownloadFile(docId)

- db funcs

    - InsertFile(doc)

    - GetFileBySubject(subject)

    - GetFileByDocumentType(documentType)

    - GetFileBySerialNumber(serialNumber)

 

### DAL

- file funcs

    - UploadFile(doc)

    - DownloadFile(docId)

- db funcs

    - InsertFile(doc)

    - InsertTrans(doc)

    - GetDocumentTypes()

    - GetFileBySubject(subject)

    - GetFileByDocumentType(documentType)

    - GetFileBySerialNumber(serialNumber)

 

### Web functions:

    - GET AddDoc

    - POST AddDoc(doc)

    - GET SearchDoc

    - GET SearchDoc(subject,documentType,serialNumber)



## **database**

 C:\Users\MSH\AppData\Local\Microsoft\VisualStudio\SSDT
Data Source=(localdb)\ProjectsV13;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

    <add name="DocArchDBEntities" connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\DocArchDB.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

Data Source=(localdb)\ProjectsV13;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False


Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DocDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False


- MSSQL Server -  DocArchDB 

#### tables: 

- Document

    - DocumentId (int) (rqg) (key)

    - Subject varchar(30) (req)

    - DocumentType varchar(30) (req)

    - DocumentDate date (req) --should be less than or equal the current date

    - SerialNumber varchar(30) --format 00/0000 Like: 25/2014 (req)

    - Details(max) 

 

- Transaction

    - DocumentId (int) (rqg) (key)        

    - TransactionDate date 

    - log varchar(30)

    

 

 

 
 