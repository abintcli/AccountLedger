# AccountLedger
## Clone Repository
Run the following to clone
```bash 
git clone https://github.com/abintcli/AccountLedger.git
```
## Run API
The API needs to be running in order for the unit tests or web to run. 
To start the API run the following in a terminal inside the API project folder
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
If the database has already been created just run 
```bash
dotnet run
```
To view the API swagger documentation page go to https://localhost:7170/swagger/index.html
## Run Tests
Assuming the API is running, open anoher terminal inside the test project folder and run the following
```bash
dotnet test
```
## Run Web
Assuming the API is running, open anoher terminal inside the web project folder and run the following
```bash
dotnet run
```
To view got to https://localhost:7173/
