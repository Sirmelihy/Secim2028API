
# Secim2028API

<div align="center" style="text-align: center" >
   <img src="https://github.com/Sirmelihy/Secim2028API/assets/58309701/80d8e986-b625-4de2-b006-cfcc58df4ab7" alt="build" style="width: 250px; height: 250px;">
</div>

**Secim2028API** was developed to replicate and function similarly to Anadolu Agency's API, which provides election data for news channels that are its clients. This project is used to provide real-time election data.

**Publicly available at** : **[secim202820240512205232.azurewebsites.net/swagger/index.html](https://secim202820240512205232.azurewebsites.net/swagger/index.html)**


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

### Candidate

<details>
<summary>Get Candidates</summary>
   
\
Retrieves list of all candidates.

**URL** : `GET /api/Aday`

**Method** : `GET`

**Auth required** : NO

**Permissions required** : None

**Success Response**

**Code** : `200 OK`

```json
[
  {
    "adayId": 14,
    "adayAdi": "Kemal Kılıçdaroğlu",
    "siyasiParti": {
      "siyasiPartiId": 19,
      "siyasiPartiAdi": "Cumhuriyet Halk Partisi",
      "siyasiPartiKisaltma": "CHP",
      "ittifak": {
        "ittifakId": 2,
        "ittifakAdi": "Millet İttifakı"
      }
    }
  }
]
```
</details>

<details>
   <summary>Add Candidate</summary>
   
   \
   Add a new candidate to the 'Candidate' table.

**URL** : `/api/Aday/AddAdays`

**Method** : `POST`

**Auth required** : YES

**Permissions required** : admin

**Data Consraints**

```json
[
  {
    "adayAdi": "Ekrem İMAMOĞLU",
    "partiId": 19
  },
  {
    "adayAdi": "Mansur YAVAŞ",
    "partiId": 19
  }

]


```
</details>

<details>
   <summary>Change Candidate</summary>
   
   \
   Changes the name of the candidate

**URL** : `/api/Aday/ChangeAday?id={cID}&name={name}`

**Method** : `POST`

**Auth required** : YES

**Permissions required** : admin
</details>

### Political Parties

<details>
<summary>Get Political Parties</summary>

\
Retrieves list of all political parties.

**URL** : `/api/SiyasiParti`

**Method** : `GET`

**Auth required** : NO

**Permissions required** : None

**Success Response**

**Code** : `200 OK`

```json
[
  {
    "siyasiPartiId": 10,
    "siyasiPartiAdi": "Adalet Birlik Partisi",
    "siyasiPartiKisaltma": "ABP"
  }
]
```
</details>

<details>
<summary>Add Political Party</summary>

\
Add a new political party to the 'Parties' table.

**URL** : `/api/SiyasiParti`

**Method** : `POST`

**Auth required** : YES

**Permissions required** : admin

**Data constraints**

```json
[
  {
    "siyasiPartiAdi": "İyi Şeyler Partisi",
    "siyasiPartiKisaltma": "ŞEY"
  }
]
```
</details>

<details>
<summary>Delete Political Party</summary>

\
Delete a political party from the 'Parties' table.

**URL** : `/api/SiyasiParti/DeleteSiyasiParti?id={ppID}`

**Method** : `POST`

**Auth required** : YES

**Permissions required** : admin
</details>

### Alliance

<details>
<summary>Get Alliances</summary>

\
Retrieves all alliances along with the political parties that belong to them.

**URL** : `/api/Ittifak`

**Method** : `GET`

**Auth required** : NO

**Permissions required** : None

**Success Response**

**Code** : `200 OK`

```json
[
  {
    "ittifakId": 1,
    "ittifakAdi": "Cumhur İttifakı",
    "siyasiPartis": [
      {
        "siyasiPartiId": 12,
        "siyasiPartiAdi": "Adalet ve Kalkınma Partisi",
        "siyasiPartiKisaltma": "AKP"
      },
      {
        "siyasiPartiId": 34,
        "siyasiPartiAdi": "Milliyetçi Hareket Partisi",
        "siyasiPartiKisaltma": "MHP"
      },
      {
        "siyasiPartiId": 42,
        "siyasiPartiAdi": "Yeniden Refah Partisi",
        "siyasiPartiKisaltma": "YRP"
      }
    ]
  },
  {
    "ittifakId": 31,
    "ittifakAdi": "Liberal İttifak",
    "siyasiPartis": [
      {
        "siyasiPartiId": 54,
        "siyasiPartiAdi": "Liberal Demokrat Parti",
        "siyasiPartiKisaltma": "LDP"
      }
    ]
  }
]
```
</details>

<details>
<summary>Add alliance</summary>

\
Add a new alliance to the 'alliance' table

**URL** : `/api/Ittifak/AddIttifak`

**Method** : `POST`

**Auth required** : YES

**Permissions required** : admin

**Data Constrainst**

```json
{
  "ittifakAdi": "İyi Şeyler İttifakı",
}
```

</details>

<details>
<summary>Delete alliance</summary>

\
Delete a alliance from the 'alliance' table

**URL** : `/api/Ittifak/DeleteIttifak?ittifakid={aID}`

**Method** : `POST`

**Auth required** : YES

**Permissions required** : admin

</details>

### Voting

<details>
<summary>Add Random Vote to Candidate</summary>

\
Add vote to a candidate in a random ballot box.

**URL** : `/api/OyAday/AddRandomSandikOy?ilid={cityID}&adayId={candidateID}&OySayisi={Count}`

**Method** : `POST`

**Auth required** : NO

**Permissions required** : None
</details>

<details>
<summary>Clear Ballot Box Candidate</summary>

\
Clear all candidate votes from a ballot box.

**URL** : `/api/OyAday/ClearSandik?sandikNo={BallotBoxNo}`

**Method** : `POST`

**Auth required** : YES

**Permissions required** : admin
</details>

<details>
<summary>Add Random Vote to Political Party</summary>

\
Add vote to a political party in a random ballot box.

**URL** : `/api/OyParti/AddRandomSandikOy?ilid={cityID}&partiId={partyId}&OySayisi={Count}`

**Method** : `POST`

**Auth required** : NO

**Permissions required** : None
</details>

<details>
<summary>Clear Ballot Box Political Party</summary>

\
Clear all political party votes from a ballot box.

**URL** : `/api/OyAday/ClearSandik?sandikNo={BallotBoxNo}`

**Method** : `POST`

**Auth required** : YES

**Permissions required** : admin
</details>



### Results

<details>
<summary>Get Candidate win times</summary>

\
Retrieves each candidate winning counts of cities

**URL** : `GET /api/Oylar/AdayWinTimesOfIl`

**Method** : `GET`

**Auth required** : NO

**Permissions required** : None

**Success Response**

**Code** : `200 OK`

```json
[
  {
    "adayName": "Recep Tayyip Erdoğan",
    "winCount": 49
  },
  {
    "adayName": "Kemal Kılıçdaroğlu",
    "winCount": 32
  }
]
```
</details>



### 

To see every endpoint please visit the Swagger UI [here](https://secim202820240512205232.azurewebsites.net/swagger/index.html)



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

For any questions or feedback please feel free to reach out me from melihokutanbs@hotmail.com\
**[Linkedin](https://www.linkedin.com/in/melihokutan5/)**\
**[GitHub](https://github.com/Sirmelihy)**


