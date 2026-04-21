# <center>Cloud Resource Management System</center>

It is the core of an IaaS (Infrastructure as a Service) system that manages the provisioning of different resources in the cloud, calculating their operating costs based on their technical specifications.

--- 
### Summary
---

* [**TECHNOLOGIES**](#technologies) 
    > Defines the technologies used

* [**RUN APP**](#run-app)
    > Define how to run

* [**MODEL**](#model) 
    > Defines model fields

* [**FILE STRUCTURE**](#file-structure)
    > Define the structure
    
* [**ENDPOINTS**](#endpoints) 
    > Define Endpoints
    
* [**HOST**](#host) 
    > Define host
    
* [**CREATE VIRTUAL MACHINE**](#vm) 
    > Create a virtual machine

* [**CREATE MANAGED DATABASE**](#mdb) 
    > Create a managed database

* [**GET RESOURCES**](#get) 
    > Returns list of resources
---

<a id="technologies"></a> 

## <center> Technologies </center>

       
<br>

**BACKEND**
* **ASP.NET Core Web API**
* **Entity Framework**
* **SQLite**
<br>

---

<a id="run-app"></a>
## <center> Run app </center>
* Install the packages

    ```
    dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.0
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0

* Make Migration

    ```
    dotnet ef migrations add InicialDb
    dotnet ef database update
    
* Run

    ```
    dotnet run
    

<br>


---

## <center> Model </center>
<a id="model"></a>
Use an abstract superclass called CloudResource and two subclasses called ManagedDatabase and VirtualMachine to define Entities.
 
<a id="file-structure"></a>
## <center> File structure </center>

* We find the following folders
    * Properties: to launch settings
    * controllers: to separate controller of services
    * Data: to define App database context
    * DTOs: to separate models to services and controllers
    * Interfaces: to define methods and be a bridge between services and controllers
    * Migratios: where are database migrations
    * Models: classes to define entities
    * Services: to implement methods

---
<a id="endpoints"></a>
## <center> Endpoints </center>

<a id="host"></a>
## Host

  * **Host**: https://localhost:60101/
  * **Swagger**: https://localhost:60101/swagger/index.html
 
<a id="vm"></a>

## **Create Virtual Machine**
  * **Method:** POST
  * **Note**
    * > Create virtual machine
  * **route:** {host}/virtual-machines
  * **Request body:**
  
    ```json
    {
      "resourceName": "string",
      "region": "string",
      "baseHourlyRate": decimal,
      "name": "string",
      "cpuCores": int,
      "ramMemoryGb": int
    }

    * **Response**:
      * **HTTP STATUS**: 200
<br>
<br>
<br>

<a id="mdb"></a>

## **Create Managed Database**
  * **Method:** POST
  * **Note**
    * > Create managed database
  * **route:** {host}/managed-databases
  * **Request body:**
  
    ```json
    {
      "resourceName": "string",
      "region": "string",
      "baseHourlyRate": decimal,
      "name": "string",
      "databaseEngine": "string",
      "storageCapacityGb": int
    }

    * **Response**:
      * **HTTP STATUS**: 200
<br>
<br>
<br>

<a id="get"></a>

## **Get Resources**
  * **Method:** GET
  * **Note**
    * > Returns list of resources.
  * **Route:** {base_url}api/resources
  * **Response body:**
  
  	```
    [
      {
        "id": guid,
        "resourceName": "string",
        "region": "string",
        "baseHourlyRate": decimal,
        "premium": boolean,
        "name": "string",
        "cpuCores": int,
        "ramMemoryGb": int,
        "databaseEngine": "string",
        "storageCapacityGb": int,
        "monthlyCost": decimal
      }
    ]
<br>
<br>
<br>

<br>

---

