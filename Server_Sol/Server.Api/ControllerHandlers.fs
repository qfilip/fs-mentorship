module Handlers

open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open ApiServices
open System
open DomainModels.Entities

module Cocktail =
    let service (ctx : HttpContext) = ctx.GetService<DataAccess>()
    
     
    let getAll =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let db = service ctx
                let result = db.GetAll()
                
                return! json result next ctx
            }


    let getById (id: Guid) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let db = service ctx
                let result = db.Get(id)
                
                return! json result next ctx
            }


    let postCocktail (x: Cocktail) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let db = service ctx
                
                return! json x next ctx
            }


    let postTest (x: TestData) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let db = service ctx
                let result = db.PostTest(x)
                
                return! json result next ctx
            }