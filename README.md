# Recipe planner API
## Make it easier for yourself to store your favorite recipes, plan what you will cook and what you need to buy. with Recipe planner you can create and manage recipes. Browse for new recipes. Add recipes in your planner and populate the ingredients from a recipe immigetly to your shopping list.

Built with dotnet web api. C#, Entity Framework, SQL, Azure.

### Base Url https://foodplannerapi20240204190546.azurewebsites.net/

## Authetication

### Register Account.
```
POST url/register
```
JSON Body
```
{
    "email": "",
    "password": ""
}
```
### Login, you'll receive a bearer token and refresh token in the header and expire date.
```
POST url/login
```
JSON Body
```
{
    "email": "",
    "password": ""
}
```
### Refresh token, You recieve the same as when you login in the header
```
POST url/refresh
```
JSON BODY
```JSON
{
"refreshToken": ""
}
```
## Recipes

### Get all recipes
```
GET url/api/recipes
```
### Get recipes by Id
```
GET url/api/recipes/id
```
### Post a recipe
```
POST url/api/recipes
```
``` 
HEADER
key: Bearer
value: Access Token
```
JSON BODY
```JSON
{
  "id": 0,
  "name": "string",
  "description": "string",
  "cookTime": 0,
  "ingredients": [
    {
      "ingredient": "string",
      "amount": "string"
    }
  ],
  "steps": [
    "string"
  ]
}
```

### Update a recipe
```
PUT url/api/recipes
```
```
HEADER
key: Bearer
value: Access Token
```
JSON BODY
``` JSON
{
  "id": 0,
  "name": "string",
  "description": "string",
  "cookTime": 0,
  "ingredients": [
    {
      "ingredient": "string",
      "amount": "string"
    }
  ],
  "steps": [
    "string"
  ],
  "userID": "string"
}
```
### Delete a recipe
```
DELETE url/api/recipes/id
```
```
HEADER
key: Bearer
value: Access Token
```
### Get recipes by user
```
GET url/api/recipes/get-recipes-by-user
```
```
HEADER
key: Bearer
value: Access Token
```
## Recipes Schedules, all actions require Authentication with Bearer token in the header

### Get recipe schedules
```
GET url/api/recipe-schedules
```
### Get recipe schedule by id
```
GET url/api/recipe-schedules/id
```
### Create recipe schedules, query with id to schedule and date.
```
POST url/api/recipe-schedules?recipeid=id&date=date-time
```
### Delete recipe schedule
```
DELETE url/api/recipe-schedules/id
```
## Shopping list, all actions require Authentication with Bearer token in the header

### Get all shopping items.
```
GET url/api/shopping-items
```
### Get shopping item by Id
```
GET url/api/shopping-items/id
```
### Post a shopping item
```
POST url/api/shopping-items?item=eggs&quantaty=2
```
### Update a recipe
```
PUT url/api/shoppings-items
```
JSON BODY
``` JSON
{
  "id": 0,
  "item": "string",
  "quantity": "string",
  "userID": "string"
}
```
### Delete a recipe
```
DELETE url/api/shopping-items/id
```
### Populate all ingredients and quantaty from recipe directly to shopping list.
```
POST url/api/add-ingredients-from-recipe-to-shoppinglist?recipeId=id
```





