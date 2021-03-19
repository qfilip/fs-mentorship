module ApiEndpoints

open Giraffe.Routing
open Giraffe.Core
open Giraffe.ResponseWriters
open Handlers

let (>=>) = Giraffe.Core.(>=>)

module Composite =
    let gets: HttpHandler list = [
        route "/api/composite/all" >=> Composite.getAll
    ]

    let posts: HttpHandler list = [
        routef "api/composite/byid/%O" (fun id -> Composite.getById id)
    ]

    let getUnit = ()