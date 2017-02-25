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

This README outlines the details of collaborating on this Ember application.
A short introduction of this app could easily go here.

### Prerequisites

You will need the following things properly installed on your computer.

* [Git](https://git-scm.com/)
* [Node.js](https://nodejs.org/) (with NPM)
* [Bower](https://bower.io/)
* [Ember CLI](https://ember-cli.com/)
* [PhantomJS](http://phantomjs.org/)

### Installation

* `git clone <repository-url>` this repository
* `cd webapp`
* `npm install`
* `bower install`

### Running / Development

* `ember serve`
* Visit your app at [http://localhost:4200](http://localhost:4200).

#### Code Generators

Make use of the many generators for code, try `ember help generate` for more details

#### Running Tests

* `ember test`
* `ember test --server`

#### Building

* `ember build` (development)
* `ember build --environment production` (production)

#### Deploying

Specify what it takes to deploy your app.

### Further Reading / Useful Links

* [ember.js](http://emberjs.com/)
* [ember-cli](https://ember-cli.com/)
* Development Browser Extensions
  * [ember inspector for chrome](https://chrome.google.com/webstore/detail/ember-inspector/bmdblncegkenkacieihfhpjfppoconhi)
  * [ember inspector for firefox](https://addons.mozilla.org/en-US/firefox/addon/ember-inspector/)
