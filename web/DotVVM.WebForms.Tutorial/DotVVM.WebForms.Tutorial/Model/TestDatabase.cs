using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotVVM.WebForms.Tutorial.Model
{
    public class TestDatabase
    {

        public static IQueryable<ProductDTO> GetProducts()
        {
            var data = new List<ProductDTO>()
            {
                new ProductDTO() { Id = 1, Title = "Chai", Supplier = "Exotic Liquids", Category = "Beverages", QuantityPerUnit = "10 boxes x 20 bags", UnitPrice = 18, UnitsInStock = 39 },
                new ProductDTO() { Id = 2, Title = "Chang", Supplier = "Exotic Liquids", Category = "Beverages", QuantityPerUnit = "24 - 12 oz bottles", UnitPrice = 19, UnitsInStock = 17 },
                new ProductDTO() { Id = 3, Title = "Aniseed Syrup", Supplier = "Exotic Liquids", Category = "Condiments", QuantityPerUnit = "12 - 550 ml bottles", UnitPrice = 10, UnitsInStock = 13 },
                new ProductDTO() { Id = 4, Title = "Chef Anton's Cajun Seasoning", Supplier = "New Orleans Cajun Delights", Category = "Condiments", QuantityPerUnit = "48 - 6 oz jars", UnitPrice = 22, UnitsInStock = 53 },
                new ProductDTO() { Id = 5, Title = "Chef Anton's Gumbo Mix", Supplier = "New Orleans Cajun Delights", Category = "Condiments", QuantityPerUnit = "36 boxes", UnitPrice = 21.35, UnitsInStock = 0 },
                new ProductDTO() { Id = 6, Title = "Grandma's Boysenberry Spread", Supplier = "Grandma Kelly's Homestead", Category = "Condiments", QuantityPerUnit = "12 - 8 oz jars", UnitPrice = 25, UnitsInStock = 120 },
                new ProductDTO() { Id = 7, Title = "Uncle Bob's Organic Dried Pears", Supplier = "Grandma Kelly's Homestead", Category = "Produce", QuantityPerUnit = "12 - 1 lb pkgs.", UnitPrice = 30, UnitsInStock = 15 },
                new ProductDTO() { Id = 8, Title = "Northwoods Cranberry Sauce", Supplier = "Grandma Kelly's Homestead", Category = "Condiments", QuantityPerUnit = "12 - 12 oz jars", UnitPrice = 40, UnitsInStock = 6 },
                new ProductDTO() { Id = 9, Title = "Mishi Kobe Niku", Supplier = "Tokyo Traders", Category = "Meat/Poultry", QuantityPerUnit = "18 - 500 g pkgs.", UnitPrice = 97, UnitsInStock = 29 },
                new ProductDTO() { Id = 10, Title = "Ikura", Supplier = "Tokyo Traders", Category = "Seafood", QuantityPerUnit = "12 - 200 ml jars", UnitPrice = 31, UnitsInStock = 31 },
                new ProductDTO() { Id = 11, Title = "Queso Cabrales", Supplier = "Cooperativa de Quesos 'Las Cabras'", Category = "Dairy Products", QuantityPerUnit = "1 kg pkg.", UnitPrice = 21, UnitsInStock = 22 },
                new ProductDTO() { Id = 12, Title = "Queso Manchego La Pastora", Supplier = "Cooperativa de Quesos 'Las Cabras'", Category = "Dairy Products", QuantityPerUnit = "10 - 500 g pkgs.", UnitPrice = 38, UnitsInStock = 86 },
                new ProductDTO() { Id = 13, Title = "Konbu", Supplier = "Mayumi's", Category = "Seafood", QuantityPerUnit = "2 kg box", UnitPrice = 6, UnitsInStock = 24 },
                new ProductDTO() { Id = 14, Title = "Tofu", Supplier = "Mayumi's", Category = "Produce", QuantityPerUnit = "40 - 100 g pkgs.", UnitPrice = 23.25, UnitsInStock = 35 },
                new ProductDTO() { Id = 15, Title = "Genen Shouyu", Supplier = "Mayumi's", Category = "Condiments", QuantityPerUnit = "24 - 250 ml bottles", UnitPrice = 15.5, UnitsInStock = 39 },
                new ProductDTO() { Id = 16, Title = "Pavlova", Supplier = "Pavlova, Ltd.", Category = "Confections", QuantityPerUnit = "32 - 500 g boxes", UnitPrice = 17.45, UnitsInStock = 29 },
                new ProductDTO() { Id = 17, Title = "Alice Mutton", Supplier = "Pavlova, Ltd.", Category = "Meat/Poultry", QuantityPerUnit = "20 - 1 kg tins", UnitPrice = 39, UnitsInStock = 0 },
                new ProductDTO() { Id = 18, Title = "Carnarvon Tigers", Supplier = "Pavlova, Ltd.", Category = "Seafood", QuantityPerUnit = "16 kg pkg.", UnitPrice = 62.5, UnitsInStock = 42 },
                new ProductDTO() { Id = 19, Title = "Teatime Chocolate Biscuits", Supplier = "Specialty Biscuits, Ltd.", Category = "Confections", QuantityPerUnit = "10 boxes x 12 pieces", UnitPrice = 9.2, UnitsInStock = 25 },
                new ProductDTO() { Id = 20, Title = "Sir Rodney's Marmalade", Supplier = "Specialty Biscuits, Ltd.", Category = "Confections", QuantityPerUnit = "30 gift boxes", UnitPrice = 81, UnitsInStock = 40 },
                new ProductDTO() { Id = 21, Title = "Sir Rodney's Scones", Supplier = "Specialty Biscuits, Ltd.", Category = "Confections", QuantityPerUnit = "24 pkgs. x 4 pieces", UnitPrice = 10, UnitsInStock = 3 },
                new ProductDTO() { Id = 22, Title = "Gustaf's Knäckebröd", Supplier = "PB Knäckebröd AB", Category = "Grains/Cereals", QuantityPerUnit = "24 - 500 g pkgs.", UnitPrice = 21, UnitsInStock = 104 },
                new ProductDTO() { Id = 23, Title = "Tunnbröd", Supplier = "PB Knäckebröd AB", Category = "Grains/Cereals", QuantityPerUnit = "12 - 250 g pkgs.", UnitPrice = 9, UnitsInStock = 61 },
                new ProductDTO() { Id = 24, Title = "Guaraná Fantástica", Supplier = "Refrescos Americanas LTDA", Category = "Beverages", QuantityPerUnit = "12 - 355 ml cans", UnitPrice = 4.5, UnitsInStock = 20 },
                new ProductDTO() { Id = 25, Title = "NuNuCa Nuß-Nougat-Creme", Supplier = "Heli Süßwaren GmbH & Co. KG", Category = "Confections", QuantityPerUnit = "20 - 450 g glasses", UnitPrice = 14, UnitsInStock = 76 },
                new ProductDTO() { Id = 26, Title = "Gumbär Gummibärchen", Supplier = "Heli Süßwaren GmbH & Co. KG", Category = "Confections", QuantityPerUnit = "100 - 250 g bags", UnitPrice = 31.23, UnitsInStock = 15 },
                new ProductDTO() { Id = 27, Title = "Schoggi Schokolade", Supplier = "Heli Süßwaren GmbH & Co. KG", Category = "Confections", QuantityPerUnit = "100 - 100 g pieces", UnitPrice = 43.9, UnitsInStock = 49 },
                new ProductDTO() { Id = 28, Title = "Rössle Sauerkraut", Supplier = "Plutzer Lebensmittelgroßmärkte AG", Category = "Produce", QuantityPerUnit = "25 - 825 g cans", UnitPrice = 45.6, UnitsInStock = 26 },
                new ProductDTO() { Id = 29, Title = "Thüringer Rostbratwurst", Supplier = "Plutzer Lebensmittelgroßmärkte AG", Category = "Meat/Poultry", QuantityPerUnit = "50 bags x 30 sausgs.", UnitPrice = 123.79, UnitsInStock = 0 },
                new ProductDTO() { Id = 30, Title = "Nord-Ost Matjeshering", Supplier = "Nord-Ost-Fisch Handelsgesellschaft mbH", Category = "Seafood", QuantityPerUnit = "10 - 200 g glasses", UnitPrice = 25.89, UnitsInStock = 10 },
                new ProductDTO() { Id = 31, Title = "Gorgonzola Telino", Supplier = "Formaggi Fortini s.r.l.", Category = "Dairy Products", QuantityPerUnit = "12 - 100 g pkgs", UnitPrice = 12.5, UnitsInStock = 0 },
                new ProductDTO() { Id = 32, Title = "Mascarpone Fabioli", Supplier = "Formaggi Fortini s.r.l.", Category = "Dairy Products", QuantityPerUnit = "24 - 200 g pkgs.", UnitPrice = 32, UnitsInStock = 9 },
                new ProductDTO() { Id = 33, Title = "Geitost", Supplier = "Norske Meierier", Category = "Dairy Products", QuantityPerUnit = "500 g", UnitPrice = 2.5, UnitsInStock = 112 },
                new ProductDTO() { Id = 34, Title = "Sasquatch Ale", Supplier = "Bigfoot Breweries", Category = "Beverages", QuantityPerUnit = "24 - 12 oz bottles", UnitPrice = 14, UnitsInStock = 111 },
                new ProductDTO() { Id = 35, Title = "Steeleye Stout", Supplier = "Bigfoot Breweries", Category = "Beverages", QuantityPerUnit = "24 - 12 oz bottles", UnitPrice = 18, UnitsInStock = 20 },
                new ProductDTO() { Id = 36, Title = "Inlagd Sill", Supplier = "Svensk Sjöföda AB", Category = "Seafood", QuantityPerUnit = "24 - 250 g  jars", UnitPrice = 19, UnitsInStock = 112 },
                new ProductDTO() { Id = 37, Title = "Gravad lax", Supplier = "Svensk Sjöföda AB", Category = "Seafood", QuantityPerUnit = "12 - 500 g pkgs.", UnitPrice = 26, UnitsInStock = 11 },
                new ProductDTO() { Id = 38, Title = "Côte de Blaye", Supplier = "Aux joyeux ecclésiastiques", Category = "Beverages", QuantityPerUnit = "12 - 75 cl bottles", UnitPrice = 263.5, UnitsInStock = 17 },
                new ProductDTO() { Id = 39, Title = "Chartreuse verte", Supplier = "Aux joyeux ecclésiastiques", Category = "Beverages", QuantityPerUnit = "750 cc per bottle", UnitPrice = 18, UnitsInStock = 69 },
                new ProductDTO() { Id = 40, Title = "Boston Crab Meat", Supplier = "New England Seafood Cannery", Category = "Seafood", QuantityPerUnit = "24 - 4 oz tins", UnitPrice = 18.4, UnitsInStock = 123 },
                new ProductDTO() { Id = 41, Title = "Jack's New England Clam Chowder", Supplier = "New England Seafood Cannery", Category = "Seafood", QuantityPerUnit = "12 - 12 oz cans", UnitPrice = 9.65, UnitsInStock = 85 },
                new ProductDTO() { Id = 42, Title = "Singaporean Hokkien Fried Mee", Supplier = "Leka Trading", Category = "Grains/Cereals", QuantityPerUnit = "32 - 1 kg pkgs.", UnitPrice = 14, UnitsInStock = 26 },
                new ProductDTO() { Id = 43, Title = "Ipoh Coffee", Supplier = "Leka Trading", Category = "Beverages", QuantityPerUnit = "16 - 500 g tins", UnitPrice = 46, UnitsInStock = 17 },
                new ProductDTO() { Id = 44, Title = "Gula Malacca", Supplier = "Leka Trading", Category = "Condiments", QuantityPerUnit = "20 - 2 kg bags", UnitPrice = 19.45, UnitsInStock = 27 },
                new ProductDTO() { Id = 45, Title = "Rogede sild", Supplier = "Lyngbysild", Category = "Seafood", QuantityPerUnit = "1k pkg.", UnitPrice = 9.5, UnitsInStock = 5 },
                new ProductDTO() { Id = 46, Title = "Spegesild", Supplier = "Lyngbysild", Category = "Seafood", QuantityPerUnit = "4 - 450 g glasses", UnitPrice = 12, UnitsInStock = 95 },
                new ProductDTO() { Id = 47, Title = "Zaanse koeken", Supplier = "Zaanse Snoepfabriek", Category = "Confections", QuantityPerUnit = "10 - 4 oz boxes", UnitPrice = 9.5, UnitsInStock = 36 },
                new ProductDTO() { Id = 48, Title = "Chocolade", Supplier = "Zaanse Snoepfabriek", Category = "Confections", QuantityPerUnit = "10 pkgs.", UnitPrice = 12.75, UnitsInStock = 15 },
                new ProductDTO() { Id = 49, Title = "Maxilaku", Supplier = "Karkki Oy", Category = "Confections", QuantityPerUnit = "24 - 50 g pkgs.", UnitPrice = 20, UnitsInStock = 10 },
                new ProductDTO() { Id = 50, Title = "Valkoinen suklaa", Supplier = "Karkki Oy", Category = "Confections", QuantityPerUnit = "12 - 100 g bars", UnitPrice = 16.25, UnitsInStock = 65 },
                new ProductDTO() { Id = 51, Title = "Manjimup Dried Apples", Supplier = "G'day, Mate", Category = "Produce", QuantityPerUnit = "50 - 300 g pkgs.", UnitPrice = 53, UnitsInStock = 20 },
                new ProductDTO() { Id = 52, Title = "Filo Mix", Supplier = "G'day, Mate", Category = "Grains/Cereals", QuantityPerUnit = "16 - 2 kg boxes", UnitPrice = 7, UnitsInStock = 38 },
                new ProductDTO() { Id = 53, Title = "Perth Pasties", Supplier = "G'day, Mate", Category = "Meat/Poultry", QuantityPerUnit = "48 pieces", UnitPrice = 32.8, UnitsInStock = 0 },
                new ProductDTO() { Id = 54, Title = "Tourtiere", Supplier = "Ma Maison", Category = "Meat/Poultry", QuantityPerUnit = "16 pies", UnitPrice = 7.45, UnitsInStock = 21 },
                new ProductDTO() { Id = 55, Title = "Pâté chinois", Supplier = "Ma Maison", Category = "Meat/Poultry", QuantityPerUnit = "24 boxes x 2 pies", UnitPrice = 24, UnitsInStock = 115 },
                new ProductDTO() { Id = 56, Title = "Gnocchi di nonna Alice", Supplier = "Pasta Buttini s.r.l.", Category = "Grains/Cereals", QuantityPerUnit = "24 - 250 g pkgs.", UnitPrice = 38, UnitsInStock = 21 },
                new ProductDTO() { Id = 57, Title = "Ravioli Angelo", Supplier = "Pasta Buttini s.r.l.", Category = "Grains/Cereals", QuantityPerUnit = "24 - 250 g pkgs.", UnitPrice = 19.5, UnitsInStock = 36 },
                new ProductDTO() { Id = 58, Title = "Escargots de Bourgogne", Supplier = "Escargots Nouveaux", Category = "Seafood", QuantityPerUnit = "24 pieces", UnitPrice = 13.25, UnitsInStock = 62 },
                new ProductDTO() { Id = 59, Title = "Raclette Courdavault", Supplier = "Gai pâturage", Category = "Dairy Products", QuantityPerUnit = "5 kg pkg.", UnitPrice = 55, UnitsInStock = 79 },
                new ProductDTO() { Id = 60, Title = "Camembert Pierrot", Supplier = "Gai pâturage", Category = "Dairy Products", QuantityPerUnit = "15 - 300 g rounds", UnitPrice = 34, UnitsInStock = 19 },
                new ProductDTO() { Id = 61, Title = "Sirop d'érable", Supplier = "Forets d'érables", Category = "Condiments", QuantityPerUnit = "24 - 500 ml bottles", UnitPrice = 28.5, UnitsInStock = 113 },
                new ProductDTO() { Id = 62, Title = "Tarte au sucre", Supplier = "Forets d'érables", Category = "Confections", QuantityPerUnit = "48 pies", UnitPrice = 49.3, UnitsInStock = 17 },
                new ProductDTO() { Id = 63, Title = "Vegie-spread", Supplier = "Pavlova, Ltd.", Category = "Condiments", QuantityPerUnit = "15 - 625 g jars", UnitPrice = 43.9, UnitsInStock = 24 },
                new ProductDTO() { Id = 64, Title = "Wimmers gute Semmelknödel", Supplier = "Plutzer Lebensmittelgroßmärkte AG", Category = "Grains/Cereals", QuantityPerUnit = "20 bags x 4 pieces", UnitPrice = 33.25, UnitsInStock = 22 },
                new ProductDTO() { Id = 65, Title = "Louisiana Fiery Hot Pepper Sauce", Supplier = "New Orleans Cajun Delights", Category = "Condiments", QuantityPerUnit = "32 - 8 oz bottles", UnitPrice = 21.05, UnitsInStock = 76 },
                new ProductDTO() { Id = 66, Title = "Louisiana Hot Spiced Okra", Supplier = "New Orleans Cajun Delights", Category = "Condiments", QuantityPerUnit = "24 - 8 oz jars", UnitPrice = 17, UnitsInStock = 4 },
                new ProductDTO() { Id = 67, Title = "Laughing Lumberjack Lager", Supplier = "Bigfoot Breweries", Category = "Beverages", QuantityPerUnit = "24 - 12 oz bottles", UnitPrice = 14, UnitsInStock = 52 },
                new ProductDTO() { Id = 68, Title = "Scottish Longbreads", Supplier = "Specialty Biscuits, Ltd.", Category = "Confections", QuantityPerUnit = "10 boxes x 8 pieces", UnitPrice = 12.5, UnitsInStock = 6 },
                new ProductDTO() { Id = 69, Title = "Gudbrandsdalsost", Supplier = "Norske Meierier", Category = "Dairy Products", QuantityPerUnit = "10 kg pkg.", UnitPrice = 36, UnitsInStock = 26 },
                new ProductDTO() { Id = 70, Title = "Outback Lager", Supplier = "Pavlova, Ltd.", Category = "Beverages", QuantityPerUnit = "24 - 355 ml bottles", UnitPrice = 15, UnitsInStock = 15 },
                new ProductDTO() { Id = 71, Title = "Flotemysost", Supplier = "Norske Meierier", Category = "Dairy Products", QuantityPerUnit = "10 - 500 g pkgs.", UnitPrice = 21.5, UnitsInStock = 26 },
                new ProductDTO() { Id = 72, Title = "Mozzarella di Giovanni", Supplier = "Formaggi Fortini s.r.l.", Category = "Dairy Products", QuantityPerUnit = "24 - 200 g pkgs.", UnitPrice = 34.8, UnitsInStock = 14 },
                new ProductDTO() { Id = 73, Title = "Röd Kaviar", Supplier = "Svensk Sjöföda AB", Category = "Seafood", QuantityPerUnit = "24 - 150 g jars", UnitPrice = 15, UnitsInStock = 101 },
                new ProductDTO() { Id = 74, Title = "Longlife Tofu", Supplier = "Tokyo Traders", Category = "Produce", QuantityPerUnit = "5 kg pkg.", UnitPrice = 10, UnitsInStock = 4 },
                new ProductDTO() { Id = 75, Title = "Rhönbräu Klosterbier", Supplier = "Plutzer Lebensmittelgroßmärkte AG", Category = "Beverages", QuantityPerUnit = "24 - 0.5 l bottles", UnitPrice = 7.75, UnitsInStock = 125 },
                new ProductDTO() { Id = 76, Title = "Lakkalikööri", Supplier = "Karkki Oy", Category = "Beverages", QuantityPerUnit = "500 ml", UnitPrice = 18, UnitsInStock = 57 },
                new ProductDTO() { Id = 77, Title = "Original Frankfurter grüne Soße", Supplier = "Plutzer Lebensmittelgroßmärkte AG", Category = "Condiments", QuantityPerUnit = "12 boxes", UnitPrice = 13, UnitsInStock = 32 }
            };

            return data.AsQueryable();
        }

    }
}