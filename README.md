Azure CORS rule manager
=======================

(ACRM is in development ATM)

ACRM is a simple web application to help you manage Azure Cross Origin Resource Sharing rules (CORS rule management is not possible through the Azure web interface at the moment). Since CORS rule setup is in most cases a one-time thing, there is no need to keep the rule management code within your project. This is where ACRM steps in as it gives you the ability to list/add/edit/remove CORS rules for Azure services.

## Requirements

Since ACRM is made as an ASP.NET MVC web app which uses Azure SDK v3.x, you will need VisualStudio to build and run it. I assume this will most probably be used by developers anyhow so this shouldn't be a problem. For those of you who develop stuff for Azure from platforms other than .net, you will need to manage CORS rules through the Azure REST API.

## Credits

This app was inspired by [Gaurav Mantri's post](http://gauravmantri.com/2013/12/01/windows-azure-storage-and-cors-lets-have-some-fun/).

======================

Made at: [Mono Software Ltd.](http://www.mono-software.com/)
