module DomainModels
open System

[<CLIMutable>]
type Excerpt = {
    CompositeId: Guid
    IngredientId: Guid
    Name: string
    Amount: int
}


[<CLIMutable>]
type Ingredient = {
    Id: Guid
    Name: string
}


[<CLIMutable>]
type Composite = {
    Id: Guid
    Name: string
    Excerpts: Excerpt list
}


[<CLIMutable>]
type TestData = {
    Id: Guid
    Name: string
}