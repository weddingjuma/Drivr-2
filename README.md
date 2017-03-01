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

> drivr webapp

### Build Setup

``` bash
### install dependencies
npm install

### serve with hot reload at localhost:8080
npm run dev

### build for production with minification
npm run build
```

For detailed explanation on how things work, consult the [docs for vue-loader](http://vuejs.github.io/vue-loader).

