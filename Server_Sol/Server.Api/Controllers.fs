module ApiEndpoints

open Giraffe.Routing
open Giraffe.Core
open Handlers
open Giraffe
open DomainModels.Entities

let (>=>) = Giraffe.Core.(>=>)

module Cocktail =
    let private cocktailRoute = "/cocktail"

    let private getEndpoints = [
        route "/all" >=> Cocktail.getAll
        routef "/byid/%O" (fun id -> Cocktail.getById id)
    ]

    let private postEndpoints = [
        route "/post-test" >=> bindJson<TestData> (fun x -> Cocktail.postTest x)
        route "/post-cocktail" >=> bindJson<Cocktail> (fun x -> Cocktail.postCocktail x)
    ]


    let gets: HttpHandler list = [subRoute cocktailRoute (choose getEndpoints)]
    let posts: HttpHandler list = [subRoute cocktailRoute (choose postEndpoints)]