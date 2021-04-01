module Dtos
open System
open DomainModels.Common
open SimpleTypes.Strength


module Audit = 
    [<CLIMutable>]
    type AuditDto = {
        Id: string
        EntityId: string
        CreatedOn: DateTime
        EntityStatus: EntityStatus
    }



module Excerpt =
    [<CLIMutable>]
    type ExcerptDto = {
        Id: Guid
        Name: string
        Amount: int
    }



open Excerpt

module Ingredient =
    [<CLIMutable>]
    type IngredientDto = {
        Id: Guid
        Name: string
        Strength: Strength

        Excerpts: ExcerptDto list option
    } 
    and CocktailDto = {
       Id: Guid
       AuditId: Guid

       Name: string
       Description: string option
       
       Excerpts: ExcerptDto list option
    }



module Cocktail =
    [<CLIMutable>]
    type CocktailDto = {
        Id: Guid
        AuditId: Guid

        Name: string
        Description: string option
        
        Excerpts: ExcerptDto list option
    }