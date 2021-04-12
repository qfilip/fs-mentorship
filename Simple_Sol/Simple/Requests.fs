module Requests

open System

type ApiModel = CocktailDto | IngredientDto | ExcerptDto

type Command =
    | Insert of ApiModel
    | Modify of ApiModel
    | Delete of ApiModel

type Query =
    | Single of ApiModel
    | All of ApiModel