module WrappedTypes

open System
open ValidationCommon

let private defMsg = "Failed to create"


let getValueError typeName error =
    let error = sprintf "%s %s. Reason: %s" defMsg typeName error
    [error]


let private getNullError typeName =
    let error = sprintf "%s %s, the value is null" defMsg typeName
    [error]


let wrapAny x = Success x


module Sha0 =
    type Sha0 = private Sha0 of string

    let private typeName = nameof(Sha0)

    let private validator x =
        let maxValue = int (Math.Pow(2., float (Utils.HashLength)))
        match (x >= 0 && x <= maxValue) with
        | true -> Success (Sha0 (Utils.makeKey x))
        | _ ->
            let reason = sprintf "Must be within range: 0-%i" maxValue
            Failiure (getValueError typeName reason)
        

    let wrap x = validator x
        
    let unwrap (Sha0 x) = x

    let wrapUnchecked x = Sha0 x