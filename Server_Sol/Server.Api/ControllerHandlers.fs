module Handlers

open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open ApiServices
open System

module Composite =
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