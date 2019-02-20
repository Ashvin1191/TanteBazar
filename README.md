# TanteBazar
An Ficticious Restfull Web API built with Dotnet Core 2.2.
The API Allow a user/customer to the following: 

1.Get Item
2.Save Item to a Basket
3.Remove Item from a Basket
4.Checkout Selected Item

# The Project Main Solution Breakdown:
 1. ### Src : The source folder:
 The following is define in the Source folder:
 
 1.DataServices
The different method that allows a user to carried out some operation is defined in the dataservice.
For example: 
###### QueryBasket()
Allow a user to get available item from the basket
###### QueryBasketItem()
Allow a user to view a list of item added to his basket (specific to a customer)
###### UpdateBasketItem
Update fields in basket such as the Quantity/Total Price/Date Modified
###### RemoveBasketItem
Allow a user to remove an item by using the item id from a basket
###### CheckoutBasket
Allow a user to checkout item added to the basket

2. ### Dtos
The Dtos includes properties of different models(Entity)in the project which define how data be sent over the network.
Example: ###### Class Customer
There are two properties : 
1._CustomerID_
2._CustName_

3.###Services
Define all the interfaces in the project, use in the controller module.



# Postman Collection
To Test test the above functionality of the WEB API, Please use this postman collection: https://www.getpostman.com/collections/a39c73dd527aa131aa38
