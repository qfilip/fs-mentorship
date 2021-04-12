module Models

module Common =
    type eEntityStatus = 
        | Active = 0
        | Deleted = 99



open Common
open System

module Entities =
    [<CLIMutable>]
    type Audit = {
        Id: string
        EntityId: string
        CreatedOn: DateTime
        EntityStatus: eEntityStatus
    }


    [<CLIMutable>]
    type Excerpt = {
        Id: string
        CocktailId: string
        IngredientId: string
        Amount: int
    }


    [<CLIMutable>]
    type Ingredient = {
        Id: string
        Name: string
        Strength: int
        EntityStatus: eEntityStatus
    }


    [<CLIMutable>]
    type Cocktail = {
        Id: string
        Name: string
        EntityStatus: eEntityStatus
    }



module Utility =
    open Entities

    type SimpleDatabase = {
        Cocktails: Cocktail list
        Ingredients: Ingredient list
        Excerpts: Excerpt list
        Audits: Audit list
    }