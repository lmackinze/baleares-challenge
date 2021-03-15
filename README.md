# Baleares challenge

Challenge for the Baleares group

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Installing

Clone the repository locally and build the solution using Visual Studio. Remember to check for the `appsettings.json` in case you need to update your database settings.

After compiling, open the menu `Tools -> NuGet Package Manager -> Package Manager Console`

When the console is open, select the default project to be `BalearesChallengeApp.Data` and execute the following command:

```
Update-Database
```

This will create the database and populate it with the required data (cities and provinces).

Pre-loaded provinces are:

    Id	Name
    1	Buenos Aires
    2	Cordoba
    3	Neuquen

Pre-loaded cities are:

    Id	Name                    ProvinceId
    1	Villa Luzuriaga	        1
    2	La Plata                1
    3	Tigre	                1
    4	Merlo	                1
    5	Cosquin	                2
    6	Villa General Belgrano	2
    7	Mina Clavero	        2
    8	San Martin De Los Andes	3
    9	Villa La Angostura      3
    10	Centenario              3

## Examples

All the examples are described within Swagger when the application starts.
