module Dtos

open System
open DomainModels.Common
open SimpleTypes.Strength


[<CLIMutable>]
type AuditDto = {
    Id: Guid
    EntityId: string
    CreatedOn: DateTime
    EntityStatus: eEntityStatus
}


[<CLIMutable>]
type ExcerptDto = {
    Id: Guid
    CocktailId: Guid
    IngredientId: Guid
    Amount: int
}


[<CLIMutable>]
type IngredientDto = {
    Id: Guid
    Name: string
    Strength: Strength
    EntityStatus: eEntityStatus

    Excerpts: ExcerptDto list option
} 


and CocktailDto = {
    Id: Guid
    Name: string
    Description: string option
    EntityStatus: eEntityStatus
       
    Excerpts: ExcerptDto list option
}