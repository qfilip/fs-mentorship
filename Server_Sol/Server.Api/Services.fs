module ApiServices

open System
open System.Collections.Concurrent
open DomainModels

type DataAccess() =
    let mutable data = [
        { Id = Guid.NewGuid(); Name = "Alice" }
        { Id = Guid.NewGuid(); Name = "Bob" }
        { Id = Guid.NewGuid(); Name = "Carl" }
        { Id = Guid.NewGuid(); Name = "Dean" }
        { Id = Guid.NewGuid(); Name = "Earl" }
    ]

    member _.Create name =
        let item = { Id = Guid.NewGuid(); Name = name }
        data <- data @ [item]


    member _.Update x = data <- (data |> List.filter (fun c -> c.Id <> x.Id)) @ [x]


    member _.Delete id = data <- data |> List.filter (fun c -> c.Id <> id)

    
    member _.Get id = data |> List.find (fun c -> c.Id = id)


    member _.GetAll () = data