module DbMokka

open Models.Entities
open System
open Models.Common
open Thoth.Json.Net
open System.IO
open Models.Utility

let private mguid = Guid.NewGuid().ToString()

let private filepath = Path.Combine(__SOURCE_DIRECTORY__, "simpledb.json")

let private writeToFile content =
    File.WriteAllText (filepath, content)

let private readFromFile = File.ReadAllText filepath

let private cocktails: Cocktail list = [
    { Id = mguid; Name = "Moscow Mule"; EntityStatus = eEntityStatus.Active }
    { Id = mguid; Name = "Dark 'n' Stormy"; EntityStatus = eEntityStatus.Active }
]

let private ingredients: Ingredient list = [
    { Id = mguid; Name = "Vodka"; EntityStatus = eEntityStatus.Active; Strength = 40 }
    { Id = mguid; Name = "Rum"; EntityStatus = eEntityStatus.Active; Strength = 40 }
    { Id = mguid; Name = "Mint"; EntityStatus = eEntityStatus.Active; Strength = 0 }
    { Id = mguid; Name = "Ginger Beer"; EntityStatus = eEntityStatus.Active; Strength = 2 }
]

let private excerpts: Excerpt list = [
    { Id = mguid; Amount = 5; CocktailId = cocktails.[0].Id; IngredientId = ingredients.[0].Id }
    { Id = mguid; Amount = 3; CocktailId = cocktails.[0].Id; IngredientId = ingredients.[2].Id }
    { Id = mguid; Amount = 3; CocktailId = cocktails.[0].Id; IngredientId = ingredients.[3].Id }

    { Id = mguid; Amount = 5; CocktailId = cocktails.[1].Id; IngredientId = ingredients.[1].Id }
    { Id = mguid; Amount = 3; CocktailId = cocktails.[1].Id; IngredientId = ingredients.[2].Id }
    { Id = mguid; Amount = 3; CocktailId = cocktails.[1].Id; IngredientId = ingredients.[3].Id }
]

let mockDb = 
    let db: SimpleDatabase = {
        Cocktails = cocktails
        Ingredients = ingredients
        Excerpts = excerpts
        Audits = []
    }

    let json = Encode.Auto.toString(4, db)
    writeToFile json |> ignore