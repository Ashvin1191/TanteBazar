# TanteBazar
An Ficticious Restfull Web API built with Dotnet Core 2.2.
The API Allow a user/customer to the following: 
1. Get Item
2. Save Item to a Basket
3. Remove Item from a Basket
4. Checkout Selected Item

###### A Customer is able to view a list of item in the store.The customer can select an item base on the item id and add it to a basket.Only one basket is available per customer. After the customer has added a list of selected item, he/she can check out the basket (all item in the basket is checkout). In addition a customer can remove items form the basket, removing items will clear the customer's basket.In short the customer is allow to fully manage her/his basket using the availablt list of items.  



# The Project Main Solution Breakdown:
 1. ### Src : The source folder:
 The following is define in the Source folder:
 ## TanteBazar.Core
 1.DataServices
The different method that allows a user to carried out some operation is defined in the dataservice.
For example: 
###### QueryBasket()
Allow a user to get available item from the basket
###### QueryBasketItem()
Allow a user to view a list of item added to his basket (specific to a customer)
###### UpdateBasketItem()
Update fields in basket such as the Quantity/Total Price/Date Modified
###### RemoveBasketItem()
Allow a user to remove an item by using the item id from a basket
###### CheckoutBasket()
Allow a user to checkout item added to the basket

2. ### Dtos
The Dtos includes properties of different models(Entity)in the project which define how data be sent over the network.
Example:
 ###### Customer Class
There are two properties: 
1. _CustomerID_
2. _CustName_

3. ### Services
Define all the interfaces in the project, use in the controller module.

## TanteBazar.WebApi 
The Web Api main components where the follows are defined:
1. Controller:
_Define route for different http request made by the user_
2. Mapper:
_Mapping of different entities define in the DTOs_
3. Middleware:
_Define an authorization class. i.e The customer key and value verification_
4. Models:
_Maintain data of the application_  

## TanteBazar.Tests
1.Define xunit test

## Client side
A fully asynchronous application exposes the below method which allows a user/client to perform task like view list of item,
added item to a basket and checkout item from the basket;
1. GetBasketAsync()
Allow the user to a a list of item 
2. AddItemToBasket()
Allow the user to add item to a basket
3. CheckoutBasket()
Allow the user to checkout item added to the basket
4. RemoveFromBasket()
Allow the user to remove item from the basket

The web api can be integrated with other application by using http request or by using the available the method available in the client app.

### Note: 
#### Connection string for APIConnectionString will be send via mail.
#### The Data is stored in a database (SQl Server) on Azure Platform.

# Postman Collection
To Test test the above functionality of the WEB API.
###### A list of customerId is provided via email to user in the below postman collection.

Please use this postman collection: 
https://www.getpostman.com/collections/a39c73dd527aa131aa38
