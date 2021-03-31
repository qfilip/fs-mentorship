module DomainModels

open System
open SimpleTypes.Strength

module Common =
    type EntityStatus = 
        | Active = 0
        | Deleted = 99



open Common
module Entities =
    [<CLIMutable>]
    type Audit = {
        Id: string
        EntityId: string
        CreatedOn: DateTime
        EntityStatus: EntityStatus
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
        Strength: Strength
        EntityStatus: EntityStatus

        Excerpts: Excerpt list
    }


    [<CLIMutable>]
    type Cocktail = {
        Id: string
        Name: string
        Description: string option
        EntityStatus: EntityStatus

        Excerpts: Excerpt list 
    }

    [<CLIMutable>]
    type TestData = {
        Id: Guid
        Name: string
    }