module Dtos
open System
open SimpleTypes.Strength
open DomainModels


module Audit = 
    type AuditDto = {
        Id: string
        EntityId: string
        CreatedOn: DateTime
        EntityStatus: EntityStatus
    }


module Excerpt =
    type ExcerptDto = {
        Id: Guid
        Name: string
        Amount: int
    }


open Excerpt

module Ingredient =
    type IngredientDto = {
        Id: Guid
        Name: string
        Strength: Strength

        Excerpts: ExcerptDto list option
    }


module Cocktail =

    type CocktailDto = {
        Id: Guid
        AuditId: Guid

        Name: string
        Description: string option
        
        Excerpts: ExcerptDto list option
    }