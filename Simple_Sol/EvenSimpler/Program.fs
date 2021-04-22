// Learn more about F# at http://fsharp.org

open System
open Models.Common
open Models.Entities
open Builders
open WrappedTypes.Sha0

[<EntryPoint>]
let main argv =
    let result = ResultBuilder()

    let key = result {
            let! key = WrappedTypes.Sha0.wrap 127
            let user = {
                Key = key
                Nick = "Nick"
                Wallet = (Bitcoin, 2.)
            }
            return user
        }

    
    0 // return an integer exit code
