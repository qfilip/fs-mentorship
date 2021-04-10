module DomainModels

open System
open SimpleTypes.Strength

module Common =
    type eEntityStatus = 
        | Active = 0
        | Deleted = 99



open Common
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

        Excerpts: Excerpt list
    }


    [<CLIMutable>]
    type Cocktail = {
        Id: string
        Name: string
        EntityStatus: eEntityStatus

        Excerpts: Excerpt list
    }



    [<CLIMutable>]
    type TestData = {
        Id: Guid
        Name: string
    }