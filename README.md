
# Personal Blog App

This is a personal blog project coded with ASP.NET Core MVC. This project was created using N Tier Architecture. Used was Repository Design Pattern.

## To Begin

You need change data source in /DataAccess/Concrete/Context path for creating database on your local sql server:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer("Server = [ YOUR LOCAL SERVER NAME ]; Database = personal-blog-app; Integrated Security = True;");
}
```

If you need change database name, you can change it on 'Initial Catalog'.

And you need open the package manager console, then you need write this:

```csharp
add-migration CreateDatabase
```

After these operations, your database will be created on your local sql server.

You need add admin for using the admin panel.

For this you need open your Sql Server Management Studio and you find the created database. Then open the tables section and right click Admin table. Then select the 'Select top 200 rows' option. Then you can add users.

After these operations you will use the admin panel. If need access to admin panel, you need write '/Login' to search bar.


## Support

For support, email aheroglu@outlook.com
