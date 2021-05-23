This project is a demo of using Identity package without the default UI.
It also creates a default admin user. The default admin name and email is set in AppSettings. The password is a default one, so don't forget to change it!
Default password is Admin123!
/admin folder is set up to require Admin role.
Both the role and admin User is created if the identity database doesn't exist when the app starts.

The identity db is setup to use Sqlite by default. If you change it to another provider, don't forget to delete and recreate the migration.
