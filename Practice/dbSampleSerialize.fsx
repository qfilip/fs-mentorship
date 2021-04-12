#r "nuget: Thoth.Json.Net, 5.0.0"
#load "../Simple_Sol/Simple/SimpleTypes.fs"
#load "../Simple_Sol/Simple/Dtos.fs"
#load "../Simple_Sol/Simple/Models.fs"

open Models.Entities
open System
open Models.Common
open Thoth.Json.Net
open System.IO

let mguid = Guid.NewGuid().ToString()

let filepath = "D:\\Riznice\\fs-mentorship\\Practice\\simpledb.json"

let writeToFile content =
    File.WriteAllText (filepath, content)

let readFromFile = File.ReadAllText filepath

type SimpleDatabase = {
    Cocktails: Cocktail list
    Ingredients: Ingredient list
    Excerpts: Excerpt list
    Audits: Audit list
}

let cocktails: Cocktail list = [
    { Id = mguid; Name = "Moscow Mule"; EntityStatus = eEntityStatus.Active }
    { Id = mguid; Name = "Dark 'n' Stormy"; EntityStatus = eEntityStatus.Active }
]

let ingredients: Ingredient list = [
    { Id = mguid; Name = "Vodka"; EntityStatus = eEntityStatus.Active; Strength = 40 }
    { Id = mguid; Name = "Rum"; EntityStatus = eEntityStatus.Active; Strength = 40 }
    { Id = mguid; Name = "Mint"; EntityStatus = eEntityStatus.Active; Strength = 0 }
    { Id = mguid; Name = "Ginger Beer"; EntityStatus = eEntityStatus.Active; Strength = 2 }
]

let excerpts: Excerpt list = [
    { Id = mguid; Amount = 5; CocktailId = cocktails.[0].Id; IngredientId = ingredients.[0].Id }
    { Id = mguid; Amount = 3; CocktailId = cocktails.[0].Id; IngredientId = ingredients.[2].Id }
    { Id = mguid; Amount = 3; CocktailId = cocktails.[0].Id; IngredientId = ingredients.[3].Id }

    { Id = mguid; Amount = 5; CocktailId = cocktails.[1].Id; IngredientId = ingredients.[1].Id }
    { Id = mguid; Amount = 3; CocktailId = cocktails.[1].Id; IngredientId = ingredients.[2].Id }
    { Id = mguid; Amount = 3; CocktailId = cocktails.[1].Id; IngredientId = ingredients.[3].Id }
]

let db: SimpleDatabase = {
    Cocktails = cocktails
    Ingredients = ingredients
    Excerpts = excerpts
    Audits = []
}

let json = Encode.Auto.toString(4, db)
writeToFile json |> ignore