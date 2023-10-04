# DotNet-Training-Project-Use-Wheels
Description: To build APIs for second-hand cars selling site that displays various car models from different categories.

**Primary Actors involved**

End-Users [buy vehicles from site]
Admin users [add category and vehicle to site]

**Use-cases:**      
**User Registration API**       
      • There are two types of users – Admin and regular users. This registration portal is only for regular users of the application who wants to book cars on the platform.  
      • Get details of users like first name, last name, date of birth, gender, email address and password.    
      • Registration should be successful only if the user is above 18 years old, return appropriate response when registration request is initiated.    

**Login API**        
      • Allow the user to login when the entered credentials are correct.  
      • If Incorrect credentials are used – show appropriate error response with status code.   
      • Allow the user to logout when appropriate API is triggered.  
      • User should be logged out after 3hrs from login.  

**E-Comm APIs**          
    • Create CRUD APIs for Cars and Categories [Mandatory attributes can be cars pre-owner count, product image, RC number]. Feel free to include other attributes based on creativity/references.  
    • Create some sample test data based on any used-cars website of your choice.  
    • DB can be SQL or No-SQL DB  
    • Build APIs that allow users to add cars to Wishlist. Add to wishlist should increase product view counter in database. If views are not available, show custom messages that attracts the user when hitting the API.   
    • Build APIs to read cars from Wishlist.  
    • Delete the wish list once the user logs out.  
    • Users should not be able to consume the APIs without proper authentication & authorization.  

**Role-Based APIs**  
    • The normal user must only have ability to read the data (cars & categories) (GET)  
    • Normal user can add vehicles to wish list.  
    • The admin user must have ability to add/update/delete both cars and categories.  

**Common Scenarios**  
    • Same data should not be continuously retrieved from database.  
    • Try to minimize the data consumption from the DB.  

**Failure Scenarios:**  
    1. If the Portal is down, display a user-friendly message.  
    2. If there are no products/cars in the hop, display a user-friendly message.  
    3. Handle exceptions wherever necessary  

Note: Custom error messages to be displayed to the user during error flows will be chosen by the developer  

**Additional Use case:**    
As our Government is focusing on reducing the vehicle related crimes.
They had opened a self portal to help the second hand buyers their purchase is legit since most of the crime used/caused cars are been sold under second selling markets.
https://65014f45736d26322f5b7b24.mockapi.io/cosmo/usedcars 
 
The above link gives the cars info: The missing cars from the above portal should not be allowed to added into the store and the seller who tries to add it should be marked as Blacked and not allowed for any further actions in our website, only the admin is allowed to remove the blacked status of a seller.
 
If a car is on trial the user should be warned and the respective disclaimer message need to be displayed.
 
If a user decides to buy a car which is on trial it should have an offer of 18% of its selling price.
