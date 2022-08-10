# Gangster Geek Example Local &amp; External Auth For Piranha CMS
This project implements Asp.Net default identity login procedure modified to work with piranha
## Things to note!
-Manager Logout link still uses the default piranha route. Although this does still seem to logout the user even when logged in with an external google account.
-This is just an example implementation and comes with no warranties or guarantees
-The delete user link in the manager seems to have a problem deleting accounts made with external login.

## Run the demo
Set your google client Id and secret in `appsettings.json`

```json
"Google": {
    "ClientId": "{CLIENT-ID}",
    "ClientSecret": "{CLIENT-SECRET}"
}
```

Add Authorized redirect URIs to your google credentials. URI Format `https://{URL}/signin-google`

![Google Credentials Redirect Uri](/images/RedirectUri.png) 

-Run Project
-Seed Data
- Navigate to `/Login`


### Other OAuth External Logins
In theory this workflow should allow the use of any of the oauth providers found [Here]("https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-6.0") which include Google, Microsoft, Facebook &amp; Twitter.

The `ExternalLogin` page callback method would need to have the relevant routes added

``` c#
[Route("/signin-microsoft")] // This Line would be added if you wanted to use microsoft accounts for login
[Route("/signin-google")] // Only this route exists in the project
public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null){
    // Code removed
}
```

You would also need to register the relevant services. For Example adding the microsoft service would look like this

```c#
public static PiranhaServiceBuilder UseGangsterGeekAuthSystem(this PiranhaServiceBuilder serviceBuilder, IConfiguration configuration)
{
    serviceBuilder.Services.AddAuthentication().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = configuration["Google:ClientId"];
        googleOptions.ClientSecret = configuration["Google:ClientSecret"];
    })
    .AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
        microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
    });

    return serviceBuilder;
}
```