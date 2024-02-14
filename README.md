# Recipe planner API
## Make it easier for yourself to store your favorite recipes, plan what you will cook and what you need to buy. With this Recipe planner you can create and manage recipes. Browse for new recipes. Add recipes in your planner and populate the ingredients from a recipe immediately to your shopping list.

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
### Login, you'll receive a bearer token, refresh token and expire date.
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
### Refresh token, You recieve the same response as when you login.
```
POST url/refresh
```
JSON BODY
```JSON
{
"refreshToken": ""
}
```
## How to use the refreshtoken 
In the header add the key Authorisation together with the value; Bearer your-token.
``` 
HEADER
key: Authorization
value: Bearer your-token
```

## Recipes

### Get all recipes, and you can add a query of max cook time.
```
GET url/api/recipes
Get url/api/recipes?maxCookTime=30
```
### Get recipes by Id
```
GET url/api/recipes/id
```
### Post a recipe (Authorization required)
```
POST /api/recipes
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

### Update a recipe (Authorization required)
```
PUT /api/recipes
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
  ]
}
```
### Delete a recipe
```
DELETE /api/recipes/id (Authorization required)
```
### Get recipes by user
```
GET url/api/recipes/get-recipes-by-user (Authorization required)
```
## Recipes Schedules, all actions require Authorization.

### Get recipe schedules
```
GET /api/recipeschedules
```
### Get recipe schedule by id
```
GET /api/recipeschedules/id
```
### Create recipe schedules, query with id to schedule and date.
```
POST /api/recipeschedules?recipeid=id&date=date-time
```
### Delete recipe schedule
```
DELETE /api/recipe-schedules/id
```
## Shopping list, all actions require Authorization.

### Get all shopping items.
```
GET /api/shoppingitems
```
### Get shopping item by Id
```
GET /api/shoppingitems/id
```
### Post a shopping item
```
POST /api/shoppingitems?item=eggs&quantaty=2
```
### Update a recipe
```
PUT /api/shoppingsitems
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
DELETE /api/shoppingitems/id
```
### Populate all ingredients and quantaty from recipe directly to shopping list.
```
POST /api/addingredients-from-recipe-to-shoppinglist?recipeId=id
```





