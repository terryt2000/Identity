# Identity
##Identity Tutorial
####This is a very basic enhancement of the project created by the Visual Studio 2013 MVC Template to show a simple way to link application data to the user's identity
1. Created BlogPost entity and added to the ApplicationDbContext class
  1. BlogPost entity has foreign key to AspNetUsers table
  2. In a production application, I recommend refactoring to put the application ef models in a separate dll project, typically a separate project for each context
  3. Additionally recommend factoring out the dbcontext into a "repository" or business service so the data access logic is encapsulated and does not "pollute" the controller methods
2. Created BlogPosts controller for CRUD on BlogPosts
  1. Uses [Authorize] attribute to require authentication
  1. Uses BlogPostViewModel rather than expose EF Entity model
  2. Controller filters the blogpost list using User.Identity.UserId so user can only see posts that he/she created
  3. Create/Edit augment the model with User.Identity.UserId to ensure the authenticated user's id is stored with the data.
  4. Added BlogPost link to toolbar in _Layout.cshtml
3. Added Hometown property to the ApplicationUser entity (just to show how to add custom properties to the user identity)
  1. Added Hometown field to RegisterViewModel 
  2. Updated Register method in Account controller to save the property 
