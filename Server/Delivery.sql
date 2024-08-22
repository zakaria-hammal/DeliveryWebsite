DROP TABLE IF EXISTS "Categories";
DROP TABLE IF EXISTS "Products";
DROP TABLE IF EXISTS "Orders";
DROP TABLE IF EXISTS "Feedbacks";

CREATE TABLE "Categories" (
	"CategoryID" INTEGER PRIMARY KEY,
	"CategoryName" nvarchar (15) NOT NULL
);

CREATE TABLE "Products" (
	"ProductID" INTEGER PRIMARY KEY ,
	"ProductName" nvarchar (40) NOT NULL ,
    "Description" "ntext" NULL ,
    "Price" REAL NULL CONSTRAINT "DF_Products_Cost" DEFAULT (0),
    "CategoryName" nvarchar (40) NOT NULL ,
    
    CONSTRAINT "CK_Products_Cost" CHECK (Price >= 0)
);

CREATE TABLE "Orders" (
    "OrderID" INTEGER PRIMARY KEY ,
    "ClientFirstName" nvarchar (25) NOT NULL ,
    "ClientLastName" nvarchar (25) NOT NULL ,
    "ClientEmail" nvarchar (25) NOT NULL ,
    "ClientPhoneNumber" nvarchar (10) NOT NULL ,
    "ClientAddress" "ntext" NULL ,
    "ProductId" INTEGER NOT NULL ,
    "ProductName" nvarchar (40) NOT NULL ,
    "Comments" "ntext" NULL ,
    "Completed" "bit" NOT NULL CONSTRAINT "DF_Order_Completed" DEFAULT (0)
);

CREATE TABLE "Feedbacks" (
    "FeedbackID" INTEGER PRIMARY KEY ,
    "ClientFirstName" nvarchar (25) NOT NULL ,
    "ClientLastName" nvarchar (25) NOT NULL ,
    "ClientEmail" nvarchar (25) NOT NULL ,
    "ClientPhoneNumber" nvarchar (10) NOT NULL ,
    "Problem" "ntext" NOT NULL
);