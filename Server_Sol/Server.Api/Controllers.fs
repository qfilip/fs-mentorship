module ApiEndpoints

open Giraffe.Routing
open Giraffe.Core
open Handlers

let (>=>) = Giraffe.Core.(>=>)

module Cocktail =
    let private cocktailRoute = "/cocktail"

    let private getEndpoints = [
        route "/all" >=> Cocktail.getAll
        routef "/byid/%O" (fun id -> Cocktail.getById id)
    ]


    let gets: HttpHandler list = [subRoute cocktailRoute (choose getEndpoints)]
    // let posts = route "/post-test" >=> bindJson<TestData> (fun x -> Cocktail.postTest x)