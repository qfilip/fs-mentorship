module Mapper

open System
open Dtos
open DomainModels.Entities

let multimap sources mapFn = sources |> List.map mapFn

// this Fn may fail... is it ok to have Result/Error in mapping fns
let toOption x = Some x

// Any built in function like this?
let toListOption mapFn = function
    | [] -> None
    | xs -> Some (xs |> List.map mapFn)

let toListFromOptionList mapFn = function
    | None -> []
    | Some xs -> xs |> List.map mapFn


let toGuid (x: string) = Guid.Parse(x)

module Entities =

    let toExcerpt (x: ExcerptDto) =
        let entity: Excerpt = {
            Id = x.Id.ToString()
            CocktailId = x.CocktailId.ToString()
            IngredientId = x.IngredientId.ToString()
            Amount = x.Amount
        }
        entity

    let toIngredient (x: IngredientDto) =
        let entity: Ingredient = {
            Id = x.Id.ToString()
            Name = x.Name
            Strength = SimpleTypes.Strength.unwrap (x.Strength)
            EntityStatus = x.EntityStatus

            Excerpts = x.Excerpts |> toListFromOptionList toExcerpt
        }
        entity

    let toCocktail (x: CocktailDto) =
        let entity: Cocktail = {
            Id = x.Id.ToString()
            Name = x.Name
            Description = x.Description
            EntityStatus = x.EntityStatus

            Excerpts = x.Excerpts |> toListFromOptionList toExcerpt
        }
        entity


module Dtos =

    let toExcerptDto (x: Excerpt) =
        let dto: ExcerptDto = {
             Id = x.Id |> toGuid
             CocktailId = x.CocktailId |> toGuid
             IngredientId = x.IngredientId |> toGuid
             Amount = x.Amount
        }
        dto


    let toIngredientDto (x: Ingredient) =
        let dto: IngredientDto = {
            Id = x.Id |> toGuid
            Name = x.Name
            Strength = SimpleTypes.Strength.wrapUnchecked (x.Strength)
            EntityStatus = x.EntityStatus

            Excerpts = x.Excerpts |> (toListOption toExcerptDto)
        }
        dto


    let toCocktailDto (x: Cocktail) =
        let (dto: CocktailDto) = {
            Id = x.Id |> toGuid
            Name = x.Name
            Description = x.Description
            EntityStatus = x.EntityStatus
            
            Excerpts = x.Excerpts |> (toListOption toExcerptDto)
        }
        dto
