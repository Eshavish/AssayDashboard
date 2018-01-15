# AssayDashboard

# Server Deployment Instruction 

Client-Side
  1. Copy the local Client code (except node_module) to the remote server (C:\HologicWebSites\AssayViewerApp\Hologic.AssayDashboard.Client)
  2. On terminal run (npm install)
  3. changes in files
      -> package.json ( "start": "ng serve --proxy-config proxy.conf.json --open --host=10.4.101.93 --disable-host-check")
      -> proxy.conf.json ("target": "http://ussddevengweb01:4200/")
      --> angular-cli.json (change port: 4200 in the defaults section to 80)   

Server-Side
  1. Copy the WebApi project's files to remote server (C:\HologicWebSites\AssayViewerApp\Hologic.AssayDashboard.WebAPI)
  2. Open up Web.config (called "Web") and change connection string to (read it as raw if you cannot view the code under)

    <connectionStrings>
        <add connectionString="Server=localhost\SQLEXPRESS;    
        Database=AssayDatabase; User ID=Assay; Password=assay"
    	  name="AssayDatabase" providerName="System.Data.SqlClient"/>
    </connectionStrings>

  3. restart the server on IIS
