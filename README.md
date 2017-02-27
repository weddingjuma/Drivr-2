# Drivr

## API

### Requirements

* dotnet core
* SQL Server
* Seed.cs file containing seed for database initialisation
* Launchsettings.json file containing IIS port information and connection strings

## Xamarin App

### Prerequisites

If you use an emulator make sure to do the following prerequisites 

* run `netsh http add urlacl url=http://169.254.80.80:55214/ user=everyone` in cmd to open the API port
* Add these settings to `.vs/config/applicationhost.config`:

                    <binding protocol="http" bindingInformation="*:55214:" />
                    <binding protocol="https" bindingInformation="*:44399:" />
                    <binding protocol="http" bindingInformation="*:55214:169.254.80.80" />



## Webapp
