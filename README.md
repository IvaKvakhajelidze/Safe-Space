# SafeSpace

SafeSpace is an anonymous mental health support forum built with ASP.NET Core MVC using Clean Architecture principles. The platform provides a safe environment where users can share their thoughts, support others through comments, and find daily inspiration.

## Features

- User registration and secure login
- BCrypt password hashing
- Minimum age validation (13+)
- Email validation
- User profile editing
- Soft delete for users and stories
- Create and delete stories
- Comment on stories
- Like comments
- Profanity filtering using the PurgoMalum API
- Inspirational quotes using the ZenQuotes API
- Hourly quote caching to reduce API requests
- Privacy Policy page
- Responsive and modern user interface

## Technologies Used

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- Clean Architecture
- HTML
- CSS
- Bootstrap

## Project Structure

- **Domain** – Entities, validation attributes, exceptions, interfaces
- **Application** – Business logic and service interfaces
- **Infrastructure** – Database, repositories, external API services
- **MVC** – Controllers, Views, ViewModels, static files

## External APIs

- **PurgoMalum API** – Filters inappropriate language in usernames, stories, and comments.
- **ZenQuotes API** – Displays inspirational quotes on the home page with in-memory caching.

## Security

- Passwords are hashed using BCrypt.
- Users must be at least 13 years old.
- Email addresses are validated.
- Authentication is managed using ASP.NET Core sessions.
- Soft delete prevents permanent removal of user data.

## Running the Project

1. Clone the repository.
2. Configure the SQL Server connection string in `appsettings.json`.
3. Apply the database migrations or create the database.
4. Restore NuGet packages.
5. Run the application.

## Future Improvements

- Story categories
- User profile pictures
- Story search and filtering
- Comment editing
- Email verification
- Password reset functionality
- Notifications

## Author

Developed by **Iva Kvakhajelidze**.
