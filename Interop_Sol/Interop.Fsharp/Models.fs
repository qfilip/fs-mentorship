module Models

open System
open Interop.Csharp

type Vessel = {
    Id: int
    Name: string
    Type: eVesselType
}


let private mapToVessel (x: Interop.Csharp.Vessel) = 
    { Id = x.Id; Name = x.Name; Type = x.Type }


let getVessel id =
    let func = new Functionality()
    let vessel = 
        func.GetVessel(id)
        |> Option.ofObj 
        |> Option.map mapToVessel

    vessel