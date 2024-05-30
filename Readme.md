
# Secim2028API

<div align="center" style="text-align: center" >
   <img src="https://github.com/Sirmelihy/Secim2028API/assets/58309701/80d8e986-b625-4de2-b006-cfcc58df4ab7" alt="build" style="width: 30px; height: 30px;">
</div>

**Secim2028API** was developed to replicate and function similarly to Anadolu Agency's API, which provides election data for news channels that are its clients. This project is used to provide real-time election data.

**Publicly available at** : secim202820240512205232.azurewebsites.net

**Swagger UI** : **[secim202820240512205232.azurewebsites.net/swagger/index.html](secim202820240512205232.azurewebsites.net/swagger/index.html)**


## Features

- Tracking real-time election data.
- Providing detailed election results.
- Supplying election data by province and district.
- Adding, removing, or modifying election data.
- Easy access and integration via API.
- Authentication with JWT token.

  
## Technologies Used

- **.NET 7**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **Swagger (UI)**
- **JWT (JSON Web Tokens)**
- **oAuth 2.0**



  
## Installation and setup 

To run the **Secim2028API** on your local machine, make sure you have the following prerequisites installed:

- .NET 7 SDK
- Microsoft SQL Server (recommended)

1. **Clone the repository to your local machine:**

```bash 
  git clone https://github.com/Sirmelihy/Secim2028API.git
```

2. **Navigate to the project directory:**
```bash 
  cd Secim2028
```

3. **Make sure to add the appsettings.json file as this file is not included.**
The content of appsettings.json should be:

```bash 
  {
  "ConnectionStrings": {
    "cnctstring": {Your Connection String HERE}
  },
  "jwt": {
    "Token": {Your Token HERE}
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Remember to add your own connection string and your own token in the appsettings.json file. Otherwise the program will not work properly.

4. **Implement database migrations**

```bash 
  dotnet ef database update
```

If the project does not recognize Entity Framework, please delete the NuGet package for Entity Framework and reinstall it.

5. **Run the Project**

```bash 
  dotnet run
```



    
## Database

To insert data into your local database, please refer to the SQL commands found in 
`/DatabaseData/data.sql`
## API Endpoints

The Secim2028API project offers a range of API endpoints, including vote rates by city, city and district information, political party and candidate details, and more.

Here are some of the examples.

#### Candidates

`GET /api/Aday` (Gives list of all candidates) 

`GET /api/Aday/ChangeAday?id=(cID)&name={name}` (Changes the name of Candidate)\
Authorization: Bearer <Your-jwt-Token>

#### Political Parties

`GET /api/SiyasiParti` (Fetches list of all political parties)

`POST /api/SiyasiParti/DeleteSiyasiParti?id={ppID}` (Deletes the certain political party from db)\
Authorization: Bearer <Your-jwt-Token>

#### Voting

`POST /api/OyParti?sandikNo={bbID}&partiId={ppID}&OySayisi={count}` (Adds vote to certain political party on certain Ballot Box)\
Authorization: Bearer <Your-jwt-Token>

`POST /api/OyParti/ClearSandik?sandikNo={bbID}` (Clears all political party votings from certain Ballot Box)\
Authorization: Bearer <Your-jwt-Token>



#### Results

`/api/Oylar/AdayWinTimesOfIl` (Gives Each aday winning counts of cities)\
Authorization: Bearer <Your-jwt-Token>



### 

To see every endpoint please visit the Swagger UI [here](secim202820240512205232.azurewebsites.net/swagger/index.html)



Please note that most of the post methods need **Authorization**

  
## Authorization

To use this API, which employs OAuth 2.0 for authorization, you must obtain an API key. This API only supports one role (admin), which must be configured directly in the SQL Database. Follow the steps below to get your API key:


### Getting API KEY

1. Send a POST request in the following JSON format to the **/api/Auth/Login** endpoint:\
    ```
    {
      "username": "string",
      "password": "string"
    }
    ```

2. Response is **your jwt token** if credentials are valid.
## Contributing

The Secim2028API is open to all kinds of contributions. Follow these steps in order to contribute : 

1. Fork this repository.
2. Create a new branch (git checkout -b feature/AmazingFeature).
3. Commit your changes (git commit -m 'Add some AmazingFeature').
4. Push to the branch (git push origin feature/AmazingFeature).
5. Open a Pull Request.


## Contact

For any questions or feedback please feel free to reach out me from melihokutanbs@hotmail.com

