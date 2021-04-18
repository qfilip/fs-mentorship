// Learn more about F# at http://fsharp.org

open System
open Models

[<EntryPoint>]
let main argv =
    let x = nameof Unchecked.defaultof<CrewMember>.Name
    printfn "%s" x
    0 // return an integer exit code
