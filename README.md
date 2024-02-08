# Recipe planner API
## Make it easier for yourself to store your favorite recipes, plan what you will cook and what you need to buy. with Recipe planner you can create and manage recipes. Browse for new recipes. Add recipes in your planner and populate the ingredients from a recipe immigetly to your shopping list.

### Domain name https://foodplannerapi20240204190546.azurewebsites.net/

### Authetication
Register Account.
```
url/register
```
JSON Body
```
{
    "email": "",
    "password": ""
}
```
Login, you recive a bearer token in the header.
```
url/login
```
JSON Body
```
{
    "email": "",
    "password": ""
}
```
```
get all recipes
```
/api/recipes
```
